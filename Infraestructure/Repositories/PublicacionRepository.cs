using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infraestructure.Data.Contexts;

namespace Infraestructure.Repositories
{
   public class PublicacionRepository : IPublicacionRepository
   {


      private readonly AppDbContext _context;

      public PublicacionRepository(AppDbContext context)
      {
         _context = context;
      }

      public async Task AgregarAsync(Publicacion publicacion)
      {
         await _context.Publicaciones.AddAsync(publicacion);
         await _context.SaveChangesAsync();
      }

      public IQueryable<Publicacion> ObtenerQueryable()
      {
         return _context.Publicaciones;
      }
   }
}
