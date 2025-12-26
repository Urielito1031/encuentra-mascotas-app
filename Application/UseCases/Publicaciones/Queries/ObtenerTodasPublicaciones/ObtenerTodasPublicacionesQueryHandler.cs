using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.UseCases.Queries.Publicaciones
{
    public class ObtenerTodasPublicacionesQueryHandler : IRequestHandler<ObtenerTodasPublicacionesQuery, ObtenerTodasPublicacionesResult>
    {
        private readonly IPublicacionRepository _publicacionRepository;

        public ObtenerTodasPublicacionesQueryHandler(IPublicacionRepository publicacionRepository)
        {
            _publicacionRepository = publicacionRepository;
        }

        public async Task<ObtenerTodasPublicacionesResult> Handle(ObtenerTodasPublicacionesQuery request, CancellationToken cancellationToken)
        {
            var publicaciones = await _publicacionRepository.ObtenerTodasAsync();
            return new ObtenerTodasPublicacionesResult(publicaciones);
        }
    }
}