using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Publicaciones.PublicarMascotaPerdida
{
  public sealed record PublicarMascotaPerdidaCommand(
     Guid UsuarioId,
     //mascota
     string NombreMascota,
     Guid RazaId,
     string ColorPrincipal,
     string DescripcionMascota,
     TamanioMascota TamanioMascota,
     Sexo Sexo,

     //Ubicacion
     string Provincia,
     string Distrito,
     string Barrio,


     //Publicacion
     string DescripcionPublicacion,
     DateTime FechaPerdido,
     EstadoMascota EstadoMascota,

     //Fotos
     IReadOnlyList<IFormFile> Fotos

     ): IRequest<PublicarMascotaPerdidaResult>;
 
}
