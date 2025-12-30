using Application.UseCases.Publicaciones.Queries.ObtenerTodasPublicaciones;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore; 

namespace Application.UseCases.Queries.Publicaciones
{
   public class ObtenerFeedUseCase : IRequestHandler<ObtenerFeedQuery, ObtenerFeedResult>
   {
      private readonly IPublicacionRepository _publicacionRepository;

      public ObtenerFeedUseCase(IPublicacionRepository publicacionRepository)
      {
         _publicacionRepository = publicacionRepository;
      }

      public async Task<ObtenerFeedResult> Handle(ObtenerFeedQuery request, CancellationToken cancellationToken)
      {
         var query = _publicacionRepository.ObtenerQueryable();

         query = ValidarFiltros(request, query);

         query = query.OrderByDescending(p => p.FechaPerdido);

         var resultado = await query.Select(p => new PublicacionFeedDto(
             p.Id,
             p.Descripcion,
             p.FechaPerdido,
             p.Estado.ToString(),
             p.EstadoMascota.ToString(),

             new AutorDto(
                 p.Usuario.Nombre,
                 p.Usuario.FotoPerfilUrl
             ),

             new UbicacionDto(
                 p.Ubicacion.Provincia,
                 p.Ubicacion.Distrito,
                 p.Ubicacion.Barrio
             ),

             new MascotaResumenDto(
                 p.Mascota.Nombre,
                 p.Mascota.Raza.Nombre,
                 p.Mascota.ColorPrincipal,
                 p.Mascota.Sexo.ToString(),
                 p.Mascota.TamanioAproximado.ToString()
             ),

             p.Fotos.Select(f => f.ImagenUrl).ToList(),

             p.Comentarios.Count,
             p.Favoritos.Count
         ))
         .ToListAsync(cancellationToken);

         return new ObtenerFeedResult(resultado);
      }

      private static IQueryable<Publicacion> ValidarFiltros(ObtenerFeedQuery request, IQueryable<Domain.Entities.Publicacion> query)
      {
         if (!string.IsNullOrEmpty(request.Provincia))
         {
            query = query.Where(p => p.Ubicacion.Provincia == request.Provincia);
         }
         if (!string.IsNullOrEmpty(request.Distrito))
         {
            query = query.Where(p => p.Ubicacion.Distrito == request.Distrito);
         }
         if (request.EstadoMascota.HasValue)
         {
            query = query.Where(p => (int)p.EstadoMascota == request.EstadoMascota.Value);
         }

         return query;
      }
   }
}