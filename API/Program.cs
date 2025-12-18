using Application.Services;
using Application.UseCases.Publicaciones.CrearPublicacion;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using EncuentraMascotas.Infrastructure.Services;
using Infraestructure;
using Infraestructure.Data.Contexts;
using Infraestructure.Repositories;
using Infraestructure.Services;
using Infraestructure.Services.Geocoding;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, o =>
       {
          o.UseVector();
          o.MigrationsAssembly("Infraestructure");
       })
    );

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CrearPublicacionCommand).Assembly));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string modelPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "clip-model.onnx");


builder.Services.AddSingleton<IClipApiService>(sp => new ClipVectorizacionService(modelPath));
builder.Services.AddScoped<IImagenEmbeddingService, ImagenEmbeddingService>();
builder.Services.AddScoped<IFileStorageService, AzureBlobStorageService>();
builder.Services.AddScoped<IGeocodingService, NominatimGeocodingService>();
builder.Services.AddScoped<IPublicacionRepository, PublicacionRepository>();
builder.Services.AddScoped<IUbicacionRepository, UbicacionRepository>();
builder.Services.AddScoped<IMascotaRepository, MascotaRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();