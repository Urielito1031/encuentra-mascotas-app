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
   public class FavoritoConfiguration : IEntityTypeConfiguration<Favorito>
   {
      public void Configure(EntityTypeBuilder<Favorito> builder)
      {
         builder.HasKey(x => x.Id);

         builder.Property(x => x.Fecha)
            .IsRequired();

         // Índice único para evitar duplicados
         builder.HasIndex(x => new { x.UsuarioId, x.PublicacionId })
            .IsUnique();

         builder.HasOne(x => x.Usuario)
         .WithMany(u => u.Favoritos)
         .HasForeignKey(x => x.UsuarioId)
         .OnDelete(DeleteBehavior.Cascade);

         builder.HasOne(x => x.Publicacion)
             .WithMany(p => p.Favoritos)
             .HasForeignKey(x => x.PublicacionId)
             .OnDelete(DeleteBehavior.Cascade);
      }
   }
}
