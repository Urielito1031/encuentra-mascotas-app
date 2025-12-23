using Application.Common.Images;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Publicaciones.PublicarMascotaPerdida
{
   public class PublicarMascotaPerdidaUseCase
       : IRequestHandler<PublicarMascotaPerdidaCommand, PublicarMascotaPerdidaResult>
   {
      private readonly IMascotaRepository _mascotaRepo;
      private readonly IUbicacionRepository _ubicacionRepo;
      private readonly IPublicacionRepository _publicacionRepo;
      private readonly IGeocodingService _geocodingService;
      private readonly IProcesadorDeFotos _procesadorDeFotos;

      public PublicarMascotaPerdidaUseCase(
          IMascotaRepository mascotaRepo,
          IUbicacionRepository ubicacionRepo,
          IPublicacionRepository publicacionRepo,
          IGeocodingService geocodingService,
          IProcesadorDeFotos procesadorDeFotos)
      {
         _mascotaRepo = mascotaRepo;
         _ubicacionRepo = ubicacionRepo;
         _publicacionRepo = publicacionRepo;
         _geocodingService = geocodingService;
         _procesadorDeFotos = procesadorDeFotos;
      }

      public async Task<PublicarMascotaPerdidaResult> Handle(
          PublicarMascotaPerdidaCommand request,
          CancellationToken cancellationToken)
      {
         var ubicacion = await CrearUbicacionAsync(request);
         var mascota = CrearMascota(request);

         var publicacion = Publicacion.Crear(
             request.UsuarioId,
             mascota,
             ubicacion,
             request.DescripcionPublicacion,
             request.FechaPerdido,
             request.EstadoMascota
         );

         var fotosSubidas = new List<string>();

         try
         {
            int orden = 0;
            foreach (var archivo in request.Fotos)
            {
               var foto = await _procesadorDeFotos.ProcesarAsync(
                   archivo,
                   publicacion.Id,
                   orden++,
                   cancellationToken);

               fotosSubidas.Add(foto.ImagenUrl);
               publicacion.AgregarFoto(foto);
            }

            await _ubicacionRepo.AgregarAsync(ubicacion);
            await _mascotaRepo.AgregarAsync(mascota);
            await _publicacionRepo.AgregarAsync(publicacion);

            return new PublicarMascotaPerdidaResult(publicacion.Id);
         }
         catch
         {
            foreach (var url in fotosSubidas)
            {
               await _procesadorDeFotos.CompensarAsync(url);
            }
            throw;
         }
      }

      private async Task<Ubicacion> CrearUbicacionAsync(PublicarMascotaPerdidaCommand request)
      {
         var geo = await _geocodingService.GeocodificarAsync(
             $"{request.Barrio},{request.Distrito},{request.Provincia}");

         return Ubicacion.Crear(
             geo.Provincia,
             geo.Barrio,
             geo.Distrito,
             geo.Latitud,
             geo.Longitud);
      }

      private Mascota CrearMascota(PublicarMascotaPerdidaCommand request)
      {
         return Mascota.Crear(
             request.NombreMascota,
             request.RazaId,
             request.ColorPrincipal,
             request.DescripcionMascota,
             request.TamanioMascota,
             request.Sexo);
      }
   }
}
