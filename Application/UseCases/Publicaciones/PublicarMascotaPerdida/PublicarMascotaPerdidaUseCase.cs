using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Publicaciones.PublicarMascotaPerdida
{
   public class PublicarMascotaPerdidaUseCase : IRequestHandler<PublicarMascotaPerdidaCommand, PublicarMascotaPerdidaResult>
   {

      private readonly IMascotaRepository       _mascotaRepository;
      private readonly IUbicacionRepository     _ubicacionRepository;
      private readonly IPublicacionRepository   _publicacionRepository;
      private readonly IFileStorageService      _fileStorage;
      private readonly IImagenEmbeddingService  _embeddingService;
      private readonly IGeocodingService        _geocodingService;

      public PublicarMascotaPerdidaUseCase(IMascotaRepository mascotaRepository,
         IUbicacionRepository ubicacionRepository, 
         IPublicacionRepository publicacionRepository,
         IFileStorageService fileStorage,
         IImagenEmbeddingService embeddingService,
         IGeocodingService geocodingService)
      {
         _mascotaRepository = mascotaRepository;
         _ubicacionRepository = ubicacionRepository;
         _publicacionRepository = publicacionRepository;
         _fileStorage = fileStorage;
         _embeddingService = embeddingService;
         _geocodingService = geocodingService;
      }

      //para una publicacion completa necesitamos la ubicacion, mascota y al menos una foto
      public async Task<PublicarMascotaPerdidaResult> Handle(PublicarMascotaPerdidaCommand request, CancellationToken cancellationToken)
      {

         var geocoding = await _geocodingService.GeocodificarAsync(
            $"{request.Barrio},{request.Distrito},{request.Provincia}"
            );

         var ubicacion = Ubicacion.Crear(
            geocoding.Provincia,
            geocoding.Barrio,
            geocoding.Distrito,
            geocoding.Latitud,
            geocoding.Longitud
            );
         await _ubicacionRepository.AgregarAsync( ubicacion );

         var mascota = Mascota.Crear(
            request.NombreMascota,
            request.RazaId,
            request.colorPrincipal,
            request.DescripcionMascota,
            request.TamanioMascota,
            request.Sexo
            );

         await _mascotaRepository.AgregarAsync( mascota );

         var publicacion = Publicacion.Crear(
            request.UsuarioId,
            mascota,
            ubicacion,
            request.DescripcionPublicacion,
            request.FechaPerdido,
            request.EstadoMascota
            );

         int orden = 0;
         foreach(var foto in request.Fotos)
         {
            var url = await _fileStorage.SubirArchivoAsync(foto, "publicaciones");
            using var stream = foto.OpenReadStream();
            var embedding = _embeddingService.GenerarEmbedding( stream );

            var fotoEntidad = Foto.Crear(
               url,
               publicacion.Id,
               embedding,
               orden++
               );
            
            publicacion.AgregarFoto(fotoEntidad);
         }
         await _publicacionRepository.AgregarAsync( publicacion );

         return new PublicarMascotaPerdidaResult(publicacion.Id);




      }
   }
}
