using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
   public class Ubicacion
   {
      public Guid Id { get; private set; }
      public string Provincia { get; private set; }  //"state"
      public string Distrito {  get; private set; }  // "state_district"
      public string Barrio { get; private set; } // "city"
      public double Latitud {  get; private set; }
      public double Longitud { get; private set; }

      private Ubicacion() { }

      public static Ubicacion Crear(string provincia,string ciudad,string barrio,double lat, double lon)
      {
         if (lat is < -90 or > 90)
            throw new DomainException("Latitud inválida.");
         if (lon is < -180 or > 180)
            throw new DomainException("Longitud inválida.");
         if (string.IsNullOrWhiteSpace(provincia))
            throw new DomainException("Provincia inválida.");
         if (string.IsNullOrWhiteSpace(ciudad))
            throw new DomainException("Ciudad inválida.");
         if (string.IsNullOrWhiteSpace(barrio))
            throw new DomainException("Barrio inválido.");

         return new Ubicacion
         {
            Id = Guid.NewGuid(),
            Provincia = provincia.Trim(),
            Distrito = ciudad.Trim(),
            Barrio = barrio.Trim(),
            Latitud = lat,
            Longitud = lon
         };
      }

   }
}
