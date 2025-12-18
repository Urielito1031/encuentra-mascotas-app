using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Publicaciones.PublicarMascotaPerdida
{
   public sealed record PublicarMascotaPerdidaResult(
   
      Guid PublicacionId
      );
}
