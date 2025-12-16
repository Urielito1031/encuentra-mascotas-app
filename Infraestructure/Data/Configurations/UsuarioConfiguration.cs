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
   public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
   {
      public void Configure(EntityTypeBuilder<Usuario> builder)
      {
         builder.HasKey(x => x.Id);

         builder.Property(x => x.Nombre)
             .IsRequired()
             .HasMaxLength(200);

         builder.Property(x => x.ContraseniaHash)
             .IsRequired();

         builder.Property(x => x.Email)
             .IsRequired()
             .HasMaxLength(200);

         builder.Property(x => x.Telefono)
             .HasMaxLength(30);

         builder.Property(x => x.FotoPerfilUrl)
             .HasMaxLength(500);

         builder.Property(x => x.FechaRegistro)
             .IsRequired();

         // Índice único para email
         builder.HasIndex(x => x.Email)
            .IsUnique();

         // Las relaciones se configuran en las entidades que contienen la FK
      }
   }
}
