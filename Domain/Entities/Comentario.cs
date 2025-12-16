using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
   public class Comentario
   {
      public Guid Id { get; private set; }
      public Guid UsuarioId { get; private set; }
      public Usuario Usuario { get; private set; }
      public Guid PublicacionId { get; private set; }
      public Publicacion Publicacion { get; private set; }
      public string Texto { get; private set; }
      public DateTime Fecha { get; private set; }
      private Comentario() { }

      public static Comentario Crear(Guid usuarioId, Guid publicacionId, string texto)
      {
         if (usuarioId == Guid.Empty)
            throw new DomainException("Usuario requerido.");
         if (publicacionId == Guid.Empty)
            throw new DomainException("Publicación requerida.");
         if (string.IsNullOrWhiteSpace(texto))
            throw new DomainException("Comentario vacío.");

         return new Comentario
         {
            Id = Guid.NewGuid(),
            UsuarioId = usuarioId,
            PublicacionId = publicacionId,
            Texto = texto.Trim(),
            Fecha = DateTime.UtcNow
         };
      }
   }
}