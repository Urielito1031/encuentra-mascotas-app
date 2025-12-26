
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
   public interface IPublicacionRepository
   {
      Task AgregarAsync(Publicacion publicacion);
      Task<IReadOnlyList<Publicacion>> ObtenerTodasAsync();

   }
}
