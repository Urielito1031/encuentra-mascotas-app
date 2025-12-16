using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
   public class RegistrarMascotaRequest
   {
      public required string Nombre { get; set; }
      public Guid RazaId { get; set; }
      public required string ColorPrincipal { get; set; }
      public required string Descripcion { get; set; }
      public int TamanioAproximado { get; set; }
      public int Sexo { get; set; }
   }
}
