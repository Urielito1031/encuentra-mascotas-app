using encuentra_mascotas.Extensions;
using Microsoft.AspNetCore.Diagnostics;


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

app.UseExceptionHandler(builder =>
{
   builder.Run(async context =>
   {
      var error = context.Features
          .Get<IExceptionHandlerFeature>()?
          .Error;

      if (error is Application.Exceptions.ApplicationException appEx)
      {
         context.Response.StatusCode = appEx.StatusCode;
         await context.Response.WriteAsJsonAsync(new
         {
            error = appEx.Message
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
