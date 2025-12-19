using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Services;

namespace Infraestructure.Services.Geocoding
{
   public class NominatimGeocodingService : IGeocodingService
   {
      private readonly HttpClient _http;

      public NominatimGeocodingService(HttpClient http)
      {
         _http = http;
      }

      public async Task<GeocodingResult> GeocodificarAsync(string direccion)
      {
         var url =
             $"https://nominatim.openstreetmap.org/search" +
             $"?q={Uri.EscapeDataString(direccion)}" +
             $"&format=json&addressdetails=1&limit=1";

         var response = await _http.GetFromJsonAsync<List<NominatimResponse>>(url);

         var item = response?.FirstOrDefault()
             ?? throw new Exception("No se pudo geocodificar la dirección");

         var address = item.address;

         return new GeocodingResult(
             Provincia: address.state,
             Distrito: address.state_district,
             Barrio: address.city ?? address.town ?? address.suburb,
             Latitud: double.Parse(item.lat, CultureInfo.InvariantCulture),
             Longitud: double.Parse(item.lon, CultureInfo.InvariantCulture)
         );
      }
   }

}
