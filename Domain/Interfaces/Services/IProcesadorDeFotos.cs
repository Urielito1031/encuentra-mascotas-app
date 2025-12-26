using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces.Services
{
   public interface IProcesadorDeFotos
   {
      Task<Foto> ProcesarAsync(
           IFormFile archivo,
           Guid publicacionId,
           int orden,
           CancellationToken cancellationToken);

      Task CompensarAsync(string url);
   }
}