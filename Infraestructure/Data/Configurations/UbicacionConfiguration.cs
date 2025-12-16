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
   public class UbicacionConfiguration : IEntityTypeConfiguration<Ubicacion>
   {
      public void Configure(EntityTypeBuilder<Ubicacion> builder)
      {
         builder.HasKey(x => x.Id);

         builder.Property(x => x.Provincia)
            .IsRequired()
            .HasMaxLength(100);

         builder.Property(x => x.Distrito)
            .IsRequired()
            .HasMaxLength(100);

         builder.Property(x => x.Barrio)
            .IsRequired()
            .HasMaxLength(100);

         builder.Property(x => x.Latitud)
             .IsRequired();

         builder.Property(x => x.Longitud)
             .IsRequired();

         // Índice para búsquedas geográficas
         builder.HasIndex(x => new { x.Latitud, x.Longitud });
      }
   }
}
