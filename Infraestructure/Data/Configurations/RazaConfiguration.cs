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
   public class RazaConfiguration : IEntityTypeConfiguration<Raza>

   {
      public void Configure(EntityTypeBuilder<Raza> builder)
      {
         builder.HasKey(x => x.Id);

         builder.Property(x => x.Nombre)
             .IsRequired()
             .HasMaxLength(100);
      }
   }
}
