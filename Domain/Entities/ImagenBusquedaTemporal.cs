using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pgvector;

namespace Domain.Entities
{
   public class ImagenBusquedaTemporal
   {  
      public Guid Id { get; private set; }
      public DateTime FechaCarga { get; private set; }
      public Vector VectorEmbedding { get; private set; }
      public string OrigenIP { get; private set; }
      public string ImagenUrl { get; private set; }
      public bool EliminadoAutomatico { get; private set; }
      public DateTime FechaExpiracion { get; private set; }

      private ImagenBusquedaTemporal() { }

      public ImagenBusquedaTemporal(
      Vector vectorEmbedding,
      string origenIP,
      string imagenUrl,
      TimeSpan tiempoValidez)
      {
         Id = Guid.NewGuid();
         FechaCarga = DateTime.UtcNow;
         VectorEmbedding = vectorEmbedding ?? throw new ArgumentNullException(nameof(vectorEmbedding));
         OrigenIP = origenIP ?? throw new ArgumentNullException(nameof(origenIP));
         ImagenUrl = imagenUrl ?? throw new ArgumentNullException(nameof(imagenUrl));

         FechaExpiracion = FechaCarga.Add(tiempoValidez);
         EliminadoAutomatico = false;
      }

      public void MarcarComoEliminado()
      {
         EliminadoAutomatico = true;
      }

   }
}
