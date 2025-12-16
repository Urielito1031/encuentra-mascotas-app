using System;
using Application.Services;
using Domain.Interfaces.Services;
using Infraestructure.Data.Contexts;
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string modelPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "clip-model.onnx");


builder.Services.AddSingleton<IClipApiService>(sp => new ClipApiService(modelPath));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();