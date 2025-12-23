using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Domain.Interfaces.Services;
using Infraestructure.Services.Storage;
using Microsoft.Extensions.Options;

namespace EncuentraMascotas.Infrastructure.Services
{
   public class AzureBlobStorageService : IFileStorageService
   {
      private readonly BlobServiceClient _clientBlob;

      public AzureBlobStorageService(IOptions<AzureBlobOptions> options)
      {
         _clientBlob = new BlobServiceClient(options.Value.ConnectionString);
      }

      public async Task<string> SubirArchivoAsync(Stream archivoStream, string nombreArchivo, string contentType, string nombreContenedor)
      {
         var contenedor = _clientBlob.GetBlobContainerClient(nombreContenedor);
         await contenedor.CreateIfNotExistsAsync();
         await contenedor.SetAccessPolicyAsync(PublicAccessType.Blob);

         string extension = Path.GetExtension(nombreArchivo);
         string nombreFinal = $"{Guid.NewGuid()}{extension}";

         BlobClient clienteBlob = contenedor.GetBlobClient(nombreFinal);

         var opciones = new BlobUploadOptions
         {
            HttpHeaders = new BlobHttpHeaders { ContentType = contentType }
         };

         // aseguro que el stram esté al principio antes de leerlo
         if (archivoStream.CanSeek) archivoStream.Position = 0;

         await clienteBlob.UploadAsync(archivoStream, opciones);

         return clienteBlob.Uri.ToString();
      }

      public async Task EliminarArchivoAsync(string ruta, string nombreContenedor)
      {
         if (string.IsNullOrEmpty(ruta)) return;
         
         BlobContainerClient contenedor = _clientBlob.GetBlobContainerClient(nombreContenedor);
         string nombreArchivo = Path.GetFileName(new Uri(ruta).LocalPath);
         BlobClient clienteBlob = contenedor.GetBlobClient(nombreArchivo);
         await clienteBlob.DeleteIfExistsAsync();
      }
   }
}