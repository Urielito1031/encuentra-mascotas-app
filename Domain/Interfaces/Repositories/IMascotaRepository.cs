using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Pgvector;

namespace Domain.Interfaces.Repositories
{
   public interface IMascotaRepository
   {
      Task AgregarAsync(Mascota mascota);
      Task<List<Mascota>> BuscarSimilaresAsync(Vector vectorBusqueda, int limite =20);
      Task<Mascota?>ObtenerPorIdAsync(Guid id);

   }
}
