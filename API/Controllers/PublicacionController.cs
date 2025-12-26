using encuentra_mascotas.Contracts.Requests;
using MediatR;
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

      [HttpPost("mascota-perdida")]
      [Consumes("multipart/form-data")]
      public async Task<IActionResult> PublicarMascotaPerdida([FromForm] PublicarMascotaPerdidaRequest request)
      {
         // por ahora simulamos el id
         // En el futuro esto vendrá de: User.Claims.FirstOrDefault(c => c.Type == "id")?.Value
         var usuarioId = Guid.Parse("d3f82a92-1234-4567-89ab-cdef01234567");
        
         var command = request.ToCommand(usuarioId);

         var result = await _mediator.Send(command);
         return CreatedAtAction(
            nameof(PublicarMascotaPerdida), 
            new { id = result.PublicacionId }, 
            result
            );
      }
      

   }
}
