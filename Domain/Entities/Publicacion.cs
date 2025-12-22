using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities
{
   public class Publicacion
   {
      public Guid Id { get; private set; }
      public string Descripcion { get; private set; }
      public DateTime FechaPublicacion { get; private set; }
      public DateTime UltimaActualizacion { get; private set; }
      public DateTime FechaPerdido { get; private set; }
      public EstadoPublicacion Estado { get; private set; }

      public Guid UsuarioId { get; private set; }
      public Usuario Usuario { get; private set; }
      
      public Guid MascotaId { get; private set; }
      public Mascota Mascota { get; private set; }
      public Guid UbicacionId { get; private set; }
      public Ubicacion Ubicacion { get; private set; }
      public EstadoMascota EstadoMascota { get; private set; }



      private readonly List<Foto> _fotos = new();
      public IReadOnlyCollection<Foto> Fotos => _fotos.AsReadOnly();



      private readonly List<Comentario> _comentarios = new();
      public IReadOnlyCollection<Comentario> Comentarios => _comentarios.AsReadOnly();



      private readonly List<Favorito> _favoritos = new();
      public IReadOnlyCollection<Favorito> Favoritos => _favoritos.AsReadOnly();


      private Publicacion() { }
      public static Publicacion Crear(
         Guid usuarioId,
         Mascota mascota, 
         Ubicacion ubicacion, 
         string descripcion,
         DateTime fechaPerdido,
         EstadoMascota estadoMascota)
      {
         if (usuarioId == Guid.Empty) throw new DomainException("El autor (Usuario) es requerido.");
         if (mascota is null) throw new DomainException("Mascota es requerida.");
         if (ubicacion is null) throw new DomainException("Ubicación requerida.");
         if (string.IsNullOrWhiteSpace(descripcion)) throw new DomainException("Descripción requerida.");
         if (fechaPerdido > DateTime.UtcNow) throw new DomainException("La fecha de pérdida no puede ser futura.");

         return new Publicacion
         {
            Id = Guid.NewGuid(),
            UsuarioId = usuarioId,
            Mascota = mascota,
            MascotaId = mascota.Id,
            Ubicacion = ubicacion,
            UbicacionId = ubicacion.Id,
            Descripcion = descripcion.Trim(),
            FechaPublicacion = DateTime.UtcNow,
            UltimaActualizacion = DateTime.UtcNow,
            FechaPerdido = fechaPerdido,
            Estado = EstadoPublicacion.Activa,
            EstadoMascota = estadoMascota
         };
      }

      public void AgregarFoto(Foto foto)
      {
         if (foto is null) throw new DomainException("Foto inválida.");
         _fotos.Add(foto);
      }
      public void AgregarComentario(Comentario comentario)
      {
         if (comentario is null) throw new DomainException("Comentario inválido.");
         _comentarios.Add(comentario);
      }
      public void AgregarFavorito(Favorito favorito)
      {
         if (favorito is null) throw new DomainException("Favorito inválido.");
         _favoritos.Add(favorito);
      }

      public void CambiarEstado(EstadoPublicacion nuevoEstado)
      {
         if (Estado == EstadoPublicacion.Finalizada) 
            throw new DomainException("No se puede cambiar estado de una publicacion finalizada.");
         if (Estado == EstadoPublicacion.Eliminada)
            throw new DomainException("No se puede cambiar estado de una publicacion eliminada.");
         
         Estado = nuevoEstado;
         UltimaActualizacion = DateTime.UtcNow;
      }

      public void Actualizar(string descripcion, DateTime fechaPerdido)
      {
         if (Estado == EstadoPublicacion.Finalizada)
            throw new DomainException("No se puede actualizar una publicación finalizada.");
         if (Estado == EstadoPublicacion.Eliminada)
            throw new DomainException("No se puede actualizar una publicación eliminada.");
         if (string.IsNullOrWhiteSpace(descripcion))
            throw new DomainException("Descripción requerida.");
         if (fechaPerdido > DateTime.UtcNow)
            throw new DomainException("La fecha de pérdida no puede ser futura.");

         Descripcion = descripcion.Trim();
         FechaPerdido = fechaPerdido;
         UltimaActualizacion = DateTime.UtcNow;
      }
   }
}