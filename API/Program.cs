using encuentra_mascotas.Extensions;
using API.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();

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

      var (statusCode, message) = GlobalExceptionHandler.MapExceptionToResponse(exception);
      context.Response.StatusCode = statusCode;
      await context.Response.WriteAsJsonAsync(new { error = message });
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
