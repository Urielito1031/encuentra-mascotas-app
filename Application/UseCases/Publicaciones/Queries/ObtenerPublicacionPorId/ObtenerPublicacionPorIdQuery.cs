using MediatR;

namespace Application.UseCases.Publicaciones.Queries.ObtenerPublicacionPorId
{
   public record ObtenerPublicacionPorIdQuery(
      Guid publicacionId,
      Guid? UsuarioActualId
      ): IRequest<ObtenerPublicacionPorIdResult>;
}
