using Application.UseCases.Publicaciones.Queries.ObtenerTodasPublicaciones;
using Domain.Entities;

namespace Application.UseCases.Queries.Publicaciones
{
    public sealed record ObtenerTodasPublicacionesResult(IEnumerable<PublicacionFeedDto> Publicaciones);
}