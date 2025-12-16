using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infraestructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Pgvector;
using Pgvector.EntityFrameworkCore;

namespace Infraestructure
{
   public class MascotaRepository : IMascotaRepository
   {
      private readonly AppDbContext _context;
      public MascotaRepository(AppDbContext context)
      {
         _context = context;
      }

      public async Task AgregarAsync(Mascota mascota)
      {
         await _context.Mascotas.AddAsync(mascota);
         await _context.SaveChangesAsync();
      }

      public async Task<List<Mascota>> BuscarSimilaresAsync(Vector vectorBusqueda, int limite = 20)
      {
         return await _context.Mascotas
            .Take(limite)
            .Include(m => m.Raza)
            .ToListAsync();
      }

      public async Task<Mascota?> ObtenerPorIdAsync(int id)
      {
         return await _context.Mascotas.FindAsync(id);
      }
   }
}
