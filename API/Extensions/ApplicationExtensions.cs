using Application.Behaviors;
using Application.UseCases.Publicaciones.PublicarMascotaPerdida;
using FluentValidation;
using MediatR;

namespace encuentra_mascotas.Extensions
{
   public static class ApplicationExtensions
   {
      public static IServiceCollection AddApplication(this IServiceCollection services)
      {
         // ✅ Assembly CORRECTO: Application (donde están los handlers)
         var assembly = typeof(PublicarMascotaPerdidaCommand).Assembly;

         // MediatR
         services.AddMediatR(cfg =>
         {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
         });

         // Validadores de Application (Commands)
         services.AddValidatorsFromAssembly(assembly);

         return services;
      }
   }
}
