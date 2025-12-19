using Application.UseCases.Publicaciones.PublicarMascotaPerdida;
using Azure.Core;
using Domain.Enums;

namespace encuentra_mascotas.Contracts.Requests
{
   public class PublicarMascotaPerdidaRequest
   {
      // Mascota
      public string NombreMascota { get; set; }
      public Guid RazaId { get; set; }
      public string ColorPrincipal { get; set; }
      public string DescripcionMascota { get; set; }
      public TamanioMascota TamanioMascota { get; set; }
      public Sexo Sexo { get; set; }

      // Ubicación
      public string Provincia { get; set; }
      public string Distrito { get; set; }
      public string Barrio { get; set; }
      public double Latitud { get; set; }
      public double Longitud { get; set; }

      // Publicación
      public string DescripcionPublicacion { get; set; }
      public DateTime FechaPerdido { get; set; }
      public EstadoMascota EstadoMascota { get; set; }

      // Fotos
      public List<IFormFile> Fotos { get; set; } 

      public PublicarMascotaPerdidaCommand ToCommand(Guid usuarioId)
      {
         return new PublicarMascotaPerdidaCommand(
                   UsuarioId: usuarioId,

            //mascota
            NombreMascota: NombreMascota,
            RazaId: RazaId,
            ColorPrincipal: ColorPrincipal,
            DescripcionMascota: DescripcionMascota,
            TamanioMascota: TamanioMascota,
            Sexo: Sexo,

            //ubicacion
            Provincia: Provincia,
            Distrito: Distrito,
            Barrio: Barrio,
            Latitud: Latitud,
            Longitud: Longitud,

            //publicacion
            DescripcionPublicacion: DescripcionPublicacion,
            FechaPerdido: FechaPerdido,
            EstadoMascota: EstadoMascota,
            Fotos: Fotos
            );
      }
   }
}
