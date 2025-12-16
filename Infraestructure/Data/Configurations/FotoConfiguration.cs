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
   public class FotoConfiguration : IEntityTypeConfiguration<Foto>
   {
      public void Configure(EntityTypeBuilder<Foto> builder)
      {
         builder.HasKey(x => x.Id);

         builder.Property(x => x.ImagenUrl)
            .IsRequired();

         builder.HasOne(x=> x.Publicacion)
            .WithMany(p=> p.Fotos)
            .HasForeignKey(x=>x.PublicacionId)
            .OnDelete(DeleteBehavior.Cascade);

         builder.HasOne(x=> x.ImgEmbedding)
            .WithOne(e=> e.Foto)
            .HasForeignKey<ImagenEmbedding>(e=>e.FotoId)
            .OnDelete(DeleteBehavior.Cascade);
      }
   }
}
