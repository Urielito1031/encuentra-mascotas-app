using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Interfaces;
using EncuentraMascotas.Infrastructure.Services;
using Infraestructure;
using Infraestructure.Repositories;
using Infraestructure.Services;
using Infraestructure.Services.Storage;

namespace encuentra_mascotas.Extensions
{
   public static class InfraestructureExtensions
   {
      public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)

      {
         services.AddScoped<IImagenEmbeddingService, ImagenEmbeddingService>();
         services.AddScoped<IProcesadorDeFotos, ProcesadorDeFotosService>();

         services.Configure<AzureBlobOptions>(
            configuration.GetSection(AzureBlobOptions.SectionName));
        
         services.AddScoped<IFileStorageService, AzureBlobStorageService>();

         services.AddScoped<IUnitOfWork, UnitOfWork>();

         services.AddScoped<IPublicacionRepository, PublicacionRepository>();
         services.AddScoped<IUbicacionRepository, UbicacionRepository>();
         services.AddScoped<IMascotaRepository, MascotaRepository>();

         return services;
      }
   }
}
