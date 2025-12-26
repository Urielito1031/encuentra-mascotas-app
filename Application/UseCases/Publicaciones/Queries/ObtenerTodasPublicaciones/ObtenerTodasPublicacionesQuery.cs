using MediatR;

namespace Application.UseCases.Queries.Publicaciones
{
    public sealed record ObtenerTodasPublicacionesQuery : IRequest<ObtenerTodasPublicacionesResult>;
}