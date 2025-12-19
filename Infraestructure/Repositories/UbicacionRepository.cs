using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infraestructure.Data.Contexts;

namespace Infraestructure.Repositories
{
   public class UbicacionRepository : IUbicacionRepository
   {
      private readonly AppDbContext _context;

      public UbicacionRepository(AppDbContext context)
      {
         _context = context;
      }

      public async Task AgregarAsync(Ubicacion ubicacion)
      {
         await _context.Ubicaciones.AddAsync(ubicacion);
         _context.SaveChanges();
      }

      public async Task<Ubicacion?> ObtenerPorIdAsync(Guid id)
      {
         return await _context.Ubicaciones.FindAsync(id);

      }
   }
}
