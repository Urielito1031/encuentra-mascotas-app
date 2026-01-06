
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
   public interface IPublicacionRepository
   {
      Task AgregarAsync(Publicacion publicacion);
      IQueryable<Publicacion> ObtenerQueryable();
      Task<Publicacion?> ObtenerPorIdAsync(Guid idPublicacion);
   }
}
