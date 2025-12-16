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
   public class MascotaConfiguration : IEntityTypeConfiguration<Mascota>
   {
      public void Configure(EntityTypeBuilder<Mascota> builder)
      {
         builder.HasKey(x => x.Id);

         builder.Property(x => x.Nombre)
              .HasMaxLength(200);

         builder.Property(x => x.ColorPrincipal)
             .IsRequired()
             .HasMaxLength(100);

         builder.Property(x => x.Descripcion)
             .IsRequired()
             .HasMaxLength(1000);

         builder.Property(x => x.TamanioAproximado)
             .IsRequired();

         builder.Property(x => x.Sexo)
             .IsRequired();

         builder.HasOne(x => x.Raza)
             .WithMany()
             .HasForeignKey(x => x.RazaId)
             .OnDelete(DeleteBehavior.Restrict);

      }
   }
}