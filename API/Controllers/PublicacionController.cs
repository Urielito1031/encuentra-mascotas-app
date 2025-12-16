using Application.UseCases.Publicaciones.CrearPublicacion;
using encuentra_mascotas.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace encuentra_mascotas.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PublicacionController : ControllerBase
   {
      private readonly IMediator _mediator;

      public PublicacionController(IMediator mediator)
      {
         _mediator = mediator;
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Crear([FromForm] CrearPublicacionRequest request)
      {
         // por ahora simulamos el id
         // En el futuro esto vendrá de: User.Claims.FirstOrDefault(c => c.Type == "id")?.Value
         var usuarioId = Guid.Parse("d3f82a92-1234-4567-89ab-cdef01234567");
        
         var command = new CrearPublicacionCommand(
            UsuarioId: usuarioId,
            MascotaId: request.MascotaId,
            UbicacionId: request.UbicacionId,
            Descripcion: request.Descripcion,
            FechaPerdido: request.FechaPerdido,
            EstadoMascota: request.EstadoMascota,
            Fotos: request.Fotos
            );

         var result = await _mediator.Send(command);
         return CreatedAtAction(
            nameof(Crear), 
            new { id = result.PublicacionId }, 
            result
            );
      }
      

   }
}
