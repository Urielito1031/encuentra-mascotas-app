using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pgvector;

namespace Domain.Interfaces.Services
{
   public interface IImagenEmbeddingService
   {
     public Vector GenerarEmbedding(Stream imagen);
   }
}
