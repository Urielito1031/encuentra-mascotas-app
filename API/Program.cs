using encuentra_mascotas.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using ApplicationException = Application.Exceptions.ApplicationException;
using InfraestructureException = Infraestructure.Exceptions.InfraestructureException;
using DomainException = Domain.Exceptions.DomainException;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application
builder.Services.AddApplication();

// Infrastructure
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddExternalServices(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(appBuilder =>
{
   appBuilder.Run(async context =>
   {
      var exception = context.Features
          .Get<IExceptionHandlerFeature>()?
          .Error;

      var logger = context.RequestServices
          .GetRequiredService<ILogger<Program>>();
      
      //para ver error completo.
      logger.LogError(exception, "Unhandled exception");

      if (exception is ApplicationException appEx)
      {
         int statusCode = appEx switch
         {
            Application.Exceptions.RecursoNoEncontradoException => 404,
            Application.Exceptions.ServicioExternoException => 503,
            _ => 500
         };
         context.Response.StatusCode = statusCode;
         await context.Response.WriteAsJsonAsync(new
         {
            error = appEx.Message
         });
         return;
      }

      if (exception is DomainException domainEx)
      {
         context.Response.StatusCode = 409; // Conflicto de reglas de negocio
         await context.Response.WriteAsJsonAsync(new
         {
            error = domainEx.Message
         });
         return;
      }

      if (exception is InfraestructureException infraEx)
      {
         context.Response.StatusCode = 500; // Fallos de infraestructura
         await context.Response.WriteAsJsonAsync(new
         {
            error = "Ocurrió un error interno"
         });
         return;
      }

      context.Response.StatusCode = 500;
      await context.Response.WriteAsJsonAsync(new
      {
         error = "Ocurrió un error inesperado"
      });
   });
});

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
