using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities
{
   public class Mascota
   {
      public Guid Id { get; private set; }
      public string? Nombre { get; private set; }
      public Guid RazaId { get; private set; }
      public Raza Raza { get; private set; }
      public string ColorPrincipal { get; private set; }
      public string Descripcion { get; private set; }
      public TamanioMascota TamanioAproximado { get; private set;}
      public Sexo Sexo{ get; private set; }



      private Mascota() { }

      public static Mascota Crear(string nombre, Guid razaId,string colorPrincipal,string descripcion, TamanioMascota tamanio, Sexo sexo)
      {
         if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre es obligatorio");
         if (razaId == Guid.Empty)
            throw new DomainException("Raza inválida");
         if(string.IsNullOrWhiteSpace(colorPrincipal))
            throw new DomainException("El color es obligatorio");
         if (string.IsNullOrWhiteSpace(descripcion))
            throw new DomainException("La descripcion es obligatoria");
         return new Mascota
         {
            Id = Guid.NewGuid(),
            Nombre = nombre.Trim(),
            RazaId = razaId,
            ColorPrincipal = colorPrincipal,
            Descripcion = descripcion,
            TamanioAproximado = tamanio,
            Sexo = sexo

         };

      }


   }
}
