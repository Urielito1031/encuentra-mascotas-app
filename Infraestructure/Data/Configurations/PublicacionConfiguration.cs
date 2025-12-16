using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configurations
{
   public class PublicacionConfiguration : IEntityTypeConfiguration<Publicacion>
   {
      public void Configure(EntityTypeBuilder<Publicacion> builder)
      {
         builder.HasKey(x => x.Id);

         builder.Property(x => x.Descripcion)
            .IsRequired()
            .HasMaxLength(1000);

         builder.Property(x => x.FechaPublicacion)
            .IsRequired();

         builder.Property(x => x.UltimaActualizacion)
            .IsRequired();
         
         builder.Property(x=> x.Estado)
            .IsRequired();

         builder.Property(x => x.EstadoMascota)
            .IsRequired();

         builder.Property(x => x.FechaPerdido)
            .IsRequired();

         // Relación con Usuario
         builder.HasOne(x => x.Usuario)
            .WithMany(u => u.Publicaciones)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

         // Relación con Mascota
         builder.HasOne(x=> x.Mascota)
            .WithMany()
            .HasForeignKey(x=> x.MascotaId)
            .OnDelete(DeleteBehavior.Restrict);

         // Relación con Ubicacion (1:1 - FK en Publicacion)
         builder.HasOne(x => x.Ubicacion)
            .WithMany()
            .HasForeignKey(x => x.UbicacionId)
            .OnDelete(DeleteBehavior.Restrict);

         // Relación con Fotos
         builder.HasMany(x => x.Fotos)
            .WithOne(f => f.Publicacion)
            .HasForeignKey(f => f.PublicacionId)
            .OnDelete(DeleteBehavior.Cascade);

         // Relación con Comentarios
         builder.HasMany(x => x.Comentarios)
            .WithOne(c => c.Publicacion)
            .HasForeignKey(c => c.PublicacionId)
            .OnDelete(DeleteBehavior.Cascade);

         // Relación con Favoritos
         builder.HasMany(x => x.Favoritos)
            .WithOne(f=> f.Publicacion)
            .HasForeignKey(f=> f.PublicacionId)
            .OnDelete(DeleteBehavior.Cascade);

         // Índices para mejorar performance
         builder.HasIndex(x => x.Estado);
         builder.HasIndex(x => x.FechaPublicacion);
         builder.HasIndex(x => x.UsuarioId);
         builder.HasIndex(x => x.MascotaId);

         // Query filter para soft delete
         builder.HasQueryFilter(x => x.Estado != Domain.Enums.EstadoPublicacion.Eliminada);
      }
   }
}
