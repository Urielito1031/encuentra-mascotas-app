using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
     string colorPrincipal,
     string DescripcionMasctoa,
     TamanioMascota TamanioMascota,
     Sexo Sexo,

     //Ubicacion
     string Provincia,
     string Distrito,
     string Barrio,
     double Latitud,
     double Longitud,

     //Publicacion
     string DescripcionPublicacion,
     DateTime FechaPerdido,
     EstadoMascota EstadoMascota,

     //Fotos
     IReadOnlyList<IFormFile> Fotos

     ): IRequest<PublicarMascotaPerdidaResult>;
 
}
