using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
   public sealed class ServicioExternoException : ApplicationException
   {
      public ServicioExternoException(string message) : base(message, 503)
      {
      }
   }
}
