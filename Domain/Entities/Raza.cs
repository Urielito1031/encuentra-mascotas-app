using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class Raza
   {
      public Guid Id { get;private  set; }
      public string Nombre { get; private set; }
      private Raza() {  }

      public static Raza Crear(string nombre)
      {
         if (string.IsNullOrWhiteSpace(nombre)) throw new Exception("Nombre inválido.");
         return new Raza { Id = Guid.NewGuid(), Nombre = nombre.Trim() };
      }


   }
}
