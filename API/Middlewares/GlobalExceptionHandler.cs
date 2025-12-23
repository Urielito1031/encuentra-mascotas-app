using Application.Exceptions;
using Domain.Exceptions;
using Infraestructure.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace API.Middlewares
{
   public static class GlobalExceptionHandler
   {
      public static (int StatusCode, string Message) MapExceptionToResponse(Exception exception)
      {
         return exception switch
         {
            RecursoNoEncontradoException => (404, exception.Message),
            ServicioExternoException => (503, exception.Message),
            DomainException => (409, exception.Message),
            InfraestructureException => (500, exception.Message),
            _ => (500, "Ocurrió un error inesperado")
         };
      }
   }
}