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
   public class ImagenBusquedaTemporalConfiguration : IEntityTypeConfiguration<ImagenBusquedaTemporal>
   {
      public void Configure(EntityTypeBuilder<ImagenBusquedaTemporal> builder)
      {
         builder.HasKey(x => x.Id);

         builder.Property(x => x.VectorEmbedding)
            .HasColumnType("vector(512)")
            .IsRequired();

         builder.Property(x => x.ImagenUrl)
            .IsRequired()
            .HasMaxLength(500);

         builder.Property(x => x.OrigenIP)
            .IsRequired()
            .HasMaxLength(45); // IPv6 max length

         builder.Property(x => x.FechaCarga)
            .IsRequired();

         builder.Property(x => x.FechaExpiracion)
            .IsRequired();

         builder.Property(x => x.EliminadoAutomatico)
            .IsRequired()
            .HasDefaultValue(false);

         // Índice para limpieza automática de registros expirados
         builder.HasIndex(x => new { x.FechaExpiracion, x.EliminadoAutomatico });
      }
   }
}
