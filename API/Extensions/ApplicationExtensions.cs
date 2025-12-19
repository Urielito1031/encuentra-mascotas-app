using Application.UseCases.Publicaciones.CrearPublicacion;

namespace encuentra_mascotas.Extensions
{
   public static class ApplicationExtensions
   {
      public static IServiceCollection AddApplication(this IServiceCollection services)
      {
         services.AddMediatR(cfg =>
             cfg.RegisterServicesFromAssembly(typeof(CrearPublicacionCommand).Assembly));

         return services;
      }
   }
}
