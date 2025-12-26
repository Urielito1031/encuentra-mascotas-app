using Domain.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Infraestructure.Services.Storage;

namespace Infraestructure.Services
{
   public class ProcesadorDeFotosService : IProcesadorDeFotos
   {
      private readonly IFileStorageService _storage;
      private readonly IImagenEmbeddingService _embedding;

      public ProcesadorDeFotosService(
          IFileStorageService storage,
          IImagenEmbeddingService embedding)
      {
         _storage = storage;
         _embedding = embedding;
      }

      public async Task<Foto> ProcesarAsync(
          IFormFile archivo,
          Guid publicacionId,
          int orden,
          CancellationToken cancellationToken)
      {
         using var stream = new MemoryStream();
         await archivo.CopyToAsync(stream, cancellationToken);

         stream.Position = 0;
         var url = await _storage.SubirArchivoAsync(
             stream,
             archivo.FileName,
             archivo.ContentType,
             ContenedoresBlob.Publicaciones);

         stream.Position = 0;
         var vector = _embedding.GenerarEmbedding(stream);

         return Foto.Crear(url, publicacionId, vector, orden);
      }

      public async Task CompensarAsync(string url)
      {
         await _storage.EliminarArchivoAsync(
             url,
             ContenedoresBlob.Publicaciones);
      }
   }
}
