namespace Application.UseCases.Publicaciones.Queries.ObtenerPublicacionPorId
{
   public sealed record ObtenerPublicacionPorIdResult(
     Guid Id,
     string Descripcion,
     DateTime FechaPublicacion,
     DateTime FechaPerdido,
     string EstadoPublicacion,
     string EstadoMascota,

     AutorDetalleDto Autor,
     UbicacionDetalleDto Ubicacion,
     MascotaDetalleDto Mascota,

     IReadOnlyList<FotoDto> Fotos,
     IReadOnlyList<ComentarioDto> Comentarios,

     bool EsFavorita,
     int CantidadFavoritos
 );
   public sealed record AutorDetalleDto(
    Guid Id,
    string Nombre,
    string? FotoPerfilUrl,
    string Telefono
);
   public sealed record MascotaDetalleDto(
    string Nombre,
    string Raza,
    string ColorPrincipal,
    string Descripcion,
    string Sexo,
    string Tamanio
);
   public sealed record UbicacionDetalleDto(
    string Provincia,
    string Distrito,
    string Barrio,
    double Latitud,
    double Longitud
);
   public sealed record FotoDto(
      string Url,
      int Orden
      );
   public sealed record ComentarioDto(
      Guid Id,
      string Texto,
      DateTime Fecha,
      AutorComentarioDto Autor
      );

   public sealed record AutorComentarioDto(
       Guid Id,
       string Nombre,
       string? FotoPerfilUrl
   );



}
