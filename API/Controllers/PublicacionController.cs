using Application.UseCases.Publicaciones.CrearPublicacion;
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
      public async Task<IActionResult> Crear(CrearPublicacionCommand command)
      {
         var result = await _mediator.Send(command);
         return CreatedAtAction(nameof(Crear), new { id = result.PublicacionId }, result);
      }
      

   }
}
