using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace encuentra_mascotas.Contracts.Requests
{
   public class CrearPublicacionRequest
   {

      [Required]
      public Guid MascotaId { get; set; }
      [Required]
      public Guid UbicacionId { get; set; }
      [Required(ErrorMessage = "La descripción es obligatoria")]
      public string Descripcion { get; set; }
      [Required]
      public DateTime FechaPerdido { get; set; }
      [Required]
      public EstadoMascota EstadoMascota { get; set; }
      
      [Required(ErrorMessage = "Sube al menos una imagen de una mascota.")]
      public List<IFormFile> Fotos { get; set; }

   }
}
