
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Publicaciones.CrearPublicacion
{
   public sealed record CrearPublicacionCommand(
     Guid UsuarioId,
     Guid MascotaId,
     Guid UbicacionId,
     string Descripcion,
     DateTime FechaPerdido,
     EstadoMascota EstadoMascota,
     IReadOnlyList<IFormFile> Fotos
 ) : IRequest<CrearPublicacionResult>;

}
