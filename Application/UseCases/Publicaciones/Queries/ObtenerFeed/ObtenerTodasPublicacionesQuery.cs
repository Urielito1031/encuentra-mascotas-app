using Application.UseCases.Publicaciones.Queries.ObtenerTodasPublicaciones;
using MediatR;

namespace Application.UseCases.Queries.Publicaciones
{

   //para agregar filtros a futuro con ObtenerFeedQuery
   public record ObtenerFeedQuery : IRequest<ObtenerFeedResult>;

   public record ObtenerFeedResult(IEnumerable<PublicacionFeedDto> Publicaciones);
}