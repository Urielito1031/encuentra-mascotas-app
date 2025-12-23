namespace Domain.Interfaces.Services
{
   public interface IFileStorageService
   {
      Task<string> SubirArchivoAsync(Stream archivoStream, string nombreArchivo, string contentType, string nombreContenedor);

      Task EliminarArchivoAsync(string ruta, string nombreContenedor);

   }
}
