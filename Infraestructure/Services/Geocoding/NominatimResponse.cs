using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Services.Geocoding
{

   //JSON DESERIALIZADO
   internal sealed class NominatimResponse
   {
      public string lat {  get; set; }
      public string lon { get; set; }
      public Address address { get; set; }
      
      internal sealed class Address
      {
         public string state { get; set; }
         public string state_district { get; set; }
         public string city { get; set; }
         public string town { get; set; }
         public string suburb { get; set; }
      }
   }
}
