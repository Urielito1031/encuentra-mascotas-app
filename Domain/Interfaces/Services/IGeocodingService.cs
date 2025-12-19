using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
   public interface IGeocodingService
   {
      Task<GeocodingResult> GeocodificarAsync(string direccion);
   }

   //Pasa del JSON deserializado NominatimResponse a GeocodingResult
   //es el resultado limpio del sistema.
   public sealed record GeocodingResult(
       string Provincia,
       string Distrito,
       string Barrio,
       double Latitud,
       double Longitud
   );
}
