using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
   public class Favorito
   {
      public Guid Id { get; private set; }
      public Guid UsuarioId { get; private set; }
      public Usuario Usuario { get; private set; }

      public Guid PublicacionId { get; private set; }
      public Publicacion Publicacion { get; private set; }
      public DateTime Fecha { get; private set; }

      private Favorito() { }

      public static Favorito Crear(Guid usuarioId, Guid publicacionId)
      {
         if (usuarioId == Guid.Empty)
            throw new DomainException("Usuario requerido.");
         if (publicacionId == Guid.Empty)
            throw new DomainException("Publicación requerida.");

         return new Favorito
         {
            Id = Guid.NewGuid(),
            UsuarioId = usuarioId,
            PublicacionId = publicacionId,
            Fecha = DateTime.UtcNow
         };
      }
   }

}
