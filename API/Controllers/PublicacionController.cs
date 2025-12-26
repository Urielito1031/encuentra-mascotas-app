using encuentra_mascotas.Contracts.Requests;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace encuentra_mascotas.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PublicacionController : ControllerBase
   {
      private readonly IMediator _mediator;
      private readonly IValidator<PublicarMascotaPerdidaRequest> _validator;

      public PublicacionController(
         IMediator mediator,
         IValidator<PublicarMascotaPerdidaRequest> validator)
      {
         _mediator = mediator;
         _validator = validator;
      }

      [HttpPost("mascota-perdida")]
      [Consumes("multipart/form-data")]
      public async Task<IActionResult> PublicarMascotaPerdida([FromForm] PublicarMascotaPerdidaRequest request)
      {
         // ✅ VALIDACIÓN MANUAL (necesaria para [FromForm])
         var validationResult = await _validator.ValidateAsync(request);
         if (!validationResult.IsValid)
         {
            return BadRequest(new ValidationProblemDetails(validationResult.ToDictionary()));
         }

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
