using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
   public class Usuario
   {
      public Guid Id { get; private set; }

      public string Nombre { get; private set; }

      public string ContraseniaHash { get; private set; }

      public string? FotoPerfilUrl { get;private set; }
      public string Email { get; private set; }

      public string Telefono { get; private set; }
      public DateTime FechaRegistro { get; private set; }

      private Usuario() { }

      private readonly List<Favorito> _favoritos = new();
      public IReadOnlyCollection<Favorito> Favoritos => _favoritos.AsReadOnly();

      private readonly List<Comentario> _comentarios = new();
      public IReadOnlyCollection<Comentario> Comentarios => _comentarios.AsReadOnly();

      private readonly List<Publicacion> _publicaciones = new();
      public IReadOnlyCollection<Publicacion> Publicaciones => _publicaciones.AsReadOnly();

      public static Usuario Crear(string nombre,
         string contraseniaHash,
         string email,
         string telefono,
         string? fotoPerfil = null
         )
      {
         if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("Nombre inválido.");

         if (string.IsNullOrWhiteSpace(contraseniaHash))
            throw new DomainException("Hash inválido.");

         if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email inválido.");

         return new Usuario
         {
            Id = Guid.NewGuid(),
            Nombre = nombre,
            ContraseniaHash = contraseniaHash,
            Email = email,
            Telefono = telefono,
            FotoPerfilUrl = fotoPerfil,
            FechaRegistro = DateTime.UtcNow

         };
      }

      public void AgregarPublicacion(Publicacion p)
      {
         if (p == null) throw new DomainException("Publicación inválida.");
         _publicaciones.Add(p);
      }

      public void AgregarFavorito(Favorito f)
      {
         if (f == null) throw new DomainException("Favorito inválido.");
         _favoritos.Add(f);
      }

      public void AgregarComentario(Comentario c)
      {
         if (c == null) throw new DomainException("Comentario inválido.");
         _comentarios.Add(c);
      }

      public void CambiarFoto(string nuevaFoto) =>
             FotoPerfilUrl = nuevaFoto;

      public void CambiarTelefono(string nuevoTel) =>
          Telefono = nuevoTel;

      public void CambiarNombre( string nombre) => 
         Nombre = nombre;

   }
}
