using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Pgvector;

namespace Domain.Entities
{
   public class Foto
   {
      public Guid Id { get; private set; }
      public string ImagenUrl { get; private set; }
      public Guid PublicacionId { get; private set; }
      public Publicacion Publicacion { get; private set; }
      public ImagenEmbedding ImgEmbedding { get; private set; }

      private Foto() { }
      public static Foto Crear(string url,
         Guid publicacionId, 
         Vector embeddingVector,
         int orden)
      {
         if (string.IsNullOrWhiteSpace(url))
            throw new DomainException("URL inválida.");
         if (publicacionId == Guid.Empty)
            throw new DomainException("Publicación requerida.");
        
         var foto = new Foto
         {
            Id = Guid.NewGuid(),
            ImagenUrl = url,
            PublicacionId = publicacionId,
         };
         //no existe foto sin embedding
         foto.ImgEmbedding = ImagenEmbedding.Crear(foto.Id, embeddingVector,orden);



         return foto;
      }
     
   }
}
