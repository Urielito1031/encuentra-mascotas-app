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
   public class ImagenEmbeddingConfiguration : IEntityTypeConfiguration<ImagenEmbedding>
   {
      public void Configure(EntityTypeBuilder<ImagenEmbedding> builder)
      {
         builder.HasKey(x => x.Id);

         builder.Property(x => x.Vector)
            .HasColumnType("vector(512)")
            .IsRequired();

         builder.Property(x => x.Orden)
            .IsRequired();

         builder.Property(x => x.FechaCreacion)
            .IsRequired();

         builder.HasOne(x=>x.Foto)
            .WithOne(f=> f.ImgEmbedding)
            .HasForeignKey<ImagenEmbedding>(x=>x.FotoId)
            .OnDelete(DeleteBehavior.Cascade);

         // Índice para búsqueda vectorial eficiente
         // Nota: Este índice debe crearse manualmente en PostgreSQL después de la migración
         // CREATE INDEX idx_imagen_embedding_vector ON "ImagenEmbeddings" 
         // USING ivfflat (Vector vector_cosine_ops) WITH (lists = 100);
      }
   }
}
