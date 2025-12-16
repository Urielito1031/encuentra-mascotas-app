using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace encuentra_mascotas.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class VectorizarController : ControllerBase
   {
      private readonly ClipApiService _clipService;

      public VectorizarController(ClipApiService clipService)
      {
         _clipService = clipService;
      }

      [HttpPost]
      public IActionResult GenerarVector(IFormFile imagen)
      {
         if (imagen == null || imagen.Length == 0)
         {
            return BadRequest("Por favor, suba una imagen válida");
         }
         try
         {
            using var stream = imagen.OpenReadStream();
            float[] vector = _clipService.ObtenerImagenEnbedding(stream);
            return Ok(new
            {
               Mensaje = "Vector generado correctamente",
               Dimensiones = vector.Length,   //de 512
               Vector = vector
            });
         }
         catch (Exception ex)
         {
            return StatusCode(500, $"Error interno procesando la imagen: {ex.Message}");
         }
      }
   }
}
