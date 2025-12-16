using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
      public async Task<Ubicacion?> ObtenerPorIdAsync(Guid id)
      {
         return await _context.Ubicaciones.FindAsync(id);

      }
   }
}
