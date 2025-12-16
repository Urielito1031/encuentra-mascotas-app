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
   public class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
   {
      public void Configure(EntityTypeBuilder<Comentario> builder)
      {
         builder.HasKey(x => x.Id);

         builder.Property(x => x.Texto)
         .IsRequired()
         .HasMaxLength(500);

         builder.Property(x => x.Fecha)
             .IsRequired();

         builder.HasOne(x => x.Usuario)
             .WithMany(u => u.Comentarios)
             .HasForeignKey(x => x.UsuarioId)
             .OnDelete(DeleteBehavior.Cascade);

         builder.HasOne(x => x.Publicacion)
             .WithMany(p => p.Comentarios)
             .HasForeignKey(x => x.PublicacionId)
             .OnDelete(DeleteBehavior.Cascade);

         // Índices para mejorar performance
         builder.HasIndex(x => x.PublicacionId);
         builder.HasIndex(x => x.UsuarioId);
         builder.HasIndex(x => x.Fecha);
      }
   }
}
