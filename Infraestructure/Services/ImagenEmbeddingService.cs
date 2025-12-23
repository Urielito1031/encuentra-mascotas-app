
using Domain.Interfaces.Services;
using Infraestructure.Exceptions;
using Pgvector;
using SixLabors.ImageSharp;

namespace Infraestructure.Services
{
   public class ImagenEmbeddingService : IImagenEmbeddingService
   {
      private readonly IClipApiService _clipService;
      public ImagenEmbeddingService(IClipApiService clipService)
      {
         _clipService = clipService;
      }


      public Vector GenerarEmbedding(Stream imagen)
      {
         try
         {
            //obtener vector con CLIP
            float[] floatArray = _clipService.ObtenerImagenEmbedding(imagen);
            if (floatArray.Length != 512)
            {
               throw new EmbeddingException($"Vector debe tener 512 dimensiones, tiene {floatArray.Length}");
            }

            
            var vector = new Vector(floatArray);
            return vector;
         }
         catch (UnknownImageFormatException)
         {
            throw new EmbeddingException("Formato de imagen no soportado. Solo se permiten formatos como JPEG, PNG, BMP, GIF, WebP, etc.");
         }
      }

   
   }
}
