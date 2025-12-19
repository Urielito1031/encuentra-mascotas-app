using Infraestructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace encuentra_mascotas.Extensions
{
   public static class PersistenceExtensions
   {
      public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
      {
         var connectionString = configuration.GetConnectionString("DefaultConnection");

         services.AddDbContext<AppDbContext>(options =>
             options.UseNpgsql(connectionString, o =>
             {
                o.UseVector();
                o.MigrationsAssembly("Infraestructure");
             }));

         return services;
      }
   }
}
