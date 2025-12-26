using encuentra_mascotas.Extensions;
using API.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// FluentValidation
builder.Services
   .AddFluentValidationAutoValidation()
   .AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

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

      if (exception is ValidationException validationEx)
      {
         var problemDetails = new ValidationProblemDetails
         {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Title = "Uno o más errores han ocurrido.",
            Status = 400
         };
         problemDetails.Extensions["traceId"] = context.TraceIdentifier;

         // Mapear ValidationFailure a Errors
         foreach (var error in validationEx.Errors)
         {
            if (!problemDetails.Errors.ContainsKey(error.PropertyName))
            {
               problemDetails.Errors[error.PropertyName] = new string[0];
            }
            problemDetails.Errors[error.PropertyName] = problemDetails.Errors[error.PropertyName]
               .Append(error.ErrorMessage).ToArray();
         }

         context.Response.StatusCode = 400;
         context.Response.ContentType = "application/problem+json";
         await context.Response.WriteAsJsonAsync(problemDetails);
      }
      else
      {
         var (statusCode, message) = exception != null ? GlobalExceptionHandler.MapExceptionToResponse(exception) : (500, "Ocurrió un error inesperado");
         var problemDetails = new ProblemDetails
         {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Title = message,
            Status = statusCode
         };
         problemDetails.Extensions["traceId"] = context.TraceIdentifier;

         context.Response.StatusCode = statusCode;
         context.Response.ContentType = "application/problem+json";
         await context.Response.WriteAsJsonAsync(problemDetails);
      }
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
