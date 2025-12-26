using Domain.Entities;

namespace Application.UseCases.Queries.Publicaciones
{
    public sealed record ObtenerTodasPublicacionesResult(IReadOnlyList<Publicacion> Publicaciones);
}