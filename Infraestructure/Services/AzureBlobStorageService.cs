using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models; 
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EncuentraMascotas.Infrastructure.Services
{
   public class AzureBlobStorageService : IFileStorageService
   {
      private readonly BlobServiceClient _clientBlob;

      public AzureBlobStorageService(IConfiguration configuration)
      {
         // TODO: ver cadena de conexion a blob storage
         string keys = configuration.GetConnectionString("AzureStorage");
         _clientBlob = new BlobServiceClient(keys);
      }

      public async Task<string> SubirArchivoAsync(IFormFile archivo, string nombreContenedor)
      {
         BlobContainerClient contenedor = _clientBlob.GetBlobContainerClient(nombreContenedor);

         await contenedor.CreateIfNotExistsAsync();
         await contenedor.SetAccessPolicyAsync(PublicAccessType.Blob);

         string extension = Path.GetExtension(archivo.FileName);
         string nombreArchivo = $"{Guid.NewGuid()}{extension}";

         //referencia al archivo (Blob) aunque no exista todavia
         BlobClient clienteBlob = contenedor.GetBlobClient(nombreArchivo);

         
         var opciones = new BlobUploadOptions
         {
            HttpHeaders = new BlobHttpHeaders { ContentType = archivo.ContentType }
         };

         using (var stream = archivo.OpenReadStream())
         {
            await clienteBlob.UploadAsync(stream, opciones);
         }

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