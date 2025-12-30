namespace Application.UseCases.Publicaciones.Queries.ObtenerTodasPublicaciones
{
   public record PublicacionFeedDto(
        Guid Id,
        string Descripcion,
        DateTime FechaPerdido,
        string EstadoPublicacion, 
        string EstadoMascota,   

        AutorDto Autor,
        UbicacionDto Ubicacion,
        MascotaResumenDto Mascota,

        List<string> FotosUrls,
        int CantidadComentarios,
        int CantidadFavoritos
    );

   public record AutorDto(
       string Nombre,
       string? FotoPerfilUrl
   );

   public record UbicacionDto(
       string Provincia,
       string Distrito,
       string Barrio
   );

   public record MascotaResumenDto(
       string Nombre,
       string Raza,
       string Color,
       string Sexo,   
       string Tamanio  
   );
}
