using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.UseCases.Publicaciones.Queries.ObtenerPublicacionPorId
{
   public class ObtenerPublicacionPorIdUseCase : IRequestHandler<ObtenerPublicacionPorIdQuery, ObtenerPublicacionPorIdResult>
   {
      private readonly IPublicacionRepository _repository;

      public ObtenerPublicacionPorIdUseCase(IPublicacionRepository repository)
      {
         _repository = repository;
      }

      public async Task<ObtenerPublicacionPorIdResult> Handle(ObtenerPublicacionPorIdQuery request, CancellationToken cancellationToken)
      {
         Publicacion? publicacionEntidad = await _repository.ObtenerPorIdAsync(request.publicacionId);
         return new ObtenerPublicacionPorIdResult(
            publicacionEntidad.Id,
            publicacionEntidad.Descripcion,
            publicacionEntidad.FechaPublicacion,
            publicacionEntidad.FechaPerdido,
            publicacionEntidad.Estado.ToString(),
            publicacionEntidad.EstadoMascota.ToString(),
            new AutorDetalleDto(
               publicacionEntidad.Usuario.Id,
               publicacionEntidad.Usuario.Nombre,
               publicacionEntidad.Usuario.FotoPerfilUrl,
               publicacionEntidad.Usuario.Telefono
               ),
            new UbicacionDetalleDto(
               publicacionEntidad.Ubicacion.Provincia,
               publicacionEntidad.Ubicacion.Distrito,
               publicacionEntidad.Ubicacion.Barrio,
               publicacionEntidad.Ubicacion.Latitud,
               publicacionEntidad.Ubicacion.Longitud
               ),
            new MascotaDetalleDto(
               publicacionEntidad.Mascota.Nombre!,
               publicacionEntidad.Mascota.Raza.Nombre,
               publicacionEntidad.Mascota.ColorPrincipal,
               publicacionEntidad.Mascota.Descripcion,
               publicacionEntidad.Mascota.Sexo.ToString(),
               publicacionEntidad.Mascota.TamanioAproximado.ToString()
               ),

             publicacionEntidad.Fotos
             .OrderBy(f => f.ImgEmbedding.Orden)
             .Select(f => new FotoDto(f.ImagenUrl, f.ImgEmbedding.Orden)).ToList(),

             publicacionEntidad.Comentarios
             .OrderByDescending(c => c.Fecha)
             .Select(c => new ComentarioDto(
                c.Id,
                c.Texto,
                c.Fecha,
                new AutorComentarioDto(
                   c.Usuario.Id,
                   c.Usuario.Nombre,
                   c.Usuario.FotoPerfilUrl
                   )
                ))
             .ToList(),
             request.UsuarioActualId is not null &&
             publicacionEntidad.Favoritos.Any(f => f.UsuarioId == request.UsuarioActualId),
             publicacionEntidad.Favoritos.Count
             );
      }
   }
}
