using System.Reflection;
using encuentra_mascotas.Extensions;
using encuentra_mascotas.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===============================
// FluentValidation – API Layer
// ===============================
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
   options.SuppressModelStateInvalidFilter = true;
});


// ===============================
// Application Layer
// ===============================
builder.Services.AddApplication();

// ===============================
// Infrastructure
// ===============================
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddExternalServices(builder.Configuration);

var app = builder.Build();

// ===============================
// Global Exception Handler
// ===============================
app.UseExceptionHandler(appBuilder =>
{
   appBuilder.Run(async context =>
   {
      var exception = context.Features
         .Get<IExceptionHandlerFeature>()?
         .Error;

      var logger = context.RequestServices
         .GetRequiredService<ILogger<Program>>();

      logger.LogError(exception, "Unhandled exception");

      // Validation (Application layer)
      if (exception is ValidationException validationEx)
      {
         var problem = new ValidationProblemDetails
         {
            Title = "Error de validación",
            Status = StatusCodes.Status400BadRequest
         };

         foreach (var error in validationEx.Errors)
         {
            problem.Errors
               .TryAdd(error.PropertyName, new[] { error.ErrorMessage });
         }

         context.Response.StatusCode = 400;
         await context.Response.WriteAsJsonAsync(problem);
         return;
      }

      // Otros errores
      context.Response.StatusCode = 500;
      await context.Response.WriteAsJsonAsync(new ProblemDetails
      {
         Title = "Ocurrió un error inesperado",
         Status = 500,
         Detail = app.Environment.IsDevelopment()
            ? exception?.Message
            : null
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
