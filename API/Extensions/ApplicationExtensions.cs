using Application.UseCases.Publicaciones.CrearPublicacion;
using FluentValidation;
using Application.Behaviors;
using MediatR;

namespace encuentra_mascotas.Extensions
{
   public static class ApplicationExtensions
   {
      public static IServiceCollection AddApplication(this IServiceCollection services)
      {
         services.AddMediatR(cfg =>
             cfg.RegisterServicesFromAssembly(typeof(CrearPublicacionCommand).Assembly));

         services.AddValidatorsFromAssembly(typeof(CrearPublicacionCommand).Assembly);

         return services;
      }
   }
}
