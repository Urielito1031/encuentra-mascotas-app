using Application.Services;
using Domain.Interfaces.Services;
using Infraestructure.Services.Geocoding;
using Microsoft.Extensions.Options;

namespace encuentra_mascotas.Extensions
{
   public static class ExternalServicesExtensions
   {
      public static IServiceCollection AddExternalServices(
        this IServiceCollection services,
        IConfiguration configuration)
      {
         // CLIP
         var modelPath = Path.Combine(
             Directory.GetCurrentDirectory(),
             "Assets",
             "clip-model.onnx");

         services.AddSingleton<IClipApiService>(
             _ => new ClipVectorizacionService(modelPath));

         // Geocoding
         //toma la seccion Geocodign de appsetings y la carga en el objeto 
         services.Configure<GeocodingOptions>(
            configuration.GetSection(GeocodingOptions.SectionName));
         
         services.AddHttpClient<IGeocodingService, NominatimGeocodingService>((sp,client) =>
         {
            var options = sp
            .GetRequiredService<IOptions<GeocodingOptions>>()
            .Value;

            client.BaseAddress = new Uri(options.BaseUrl);
            client.DefaultRequestHeaders.UserAgent.ParseAdd(options.UserAgent);
          
         });

         return services;
      }
   }
}
