using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data.Contexts
{
   public class AppDbContext : DbContext
   {
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

      public DbSet<Usuario> Usuarios { get; set; }
      public DbSet<Publicacion> Publicaciones { get; set; }
      public DbSet<Mascota> Mascotas { get; set; }
      public DbSet<Ubicacion> Ubicaciones { get; set; }
      public DbSet<Foto> Fotos { get; set; }
      public DbSet<ImagenEmbedding> ImagenEmbeddings { get; set; }
      public DbSet<Comentario> Comentarios { get; set; }
      public DbSet<Favorito> Favoritos { get; set; }
      public DbSet<Raza> Razas { get; set; }
      public DbSet<ImagenBusquedaTemporal> ImagenesBusquedaTemporal { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);
         modelBuilder.HasPostgresExtension("vector");

         modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
      }
   }
}
