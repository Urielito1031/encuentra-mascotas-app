using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Pgvector;

namespace Domain.Entities
{
   public class ImagenEmbedding
   {
      public Guid Id { get; private set; }
      public Vector Vector {  get; private set; }
      public int Orden { get; private set; }
      public DateTime FechaCreacion { get; private set; }
      public Guid FotoId { get; private set; }
      public Foto Foto { get; private set; }

      private ImagenEmbedding() { }

      public static ImagenEmbedding Crear(Guid fotoId, Vector vector,int orden)
      {
         if (fotoId == Guid.Empty)
            throw new DomainException("FotoId requerido.");
         if (vector is null)
            throw new DomainException("Vector embedding inválido.");
         return new ImagenEmbedding
         {
            Id = Guid.NewGuid(),
            Vector = vector,
            Orden = orden,
            FotoId = fotoId,
            FechaCreacion = DateTime.UtcNow
         };
      }



   }
}
