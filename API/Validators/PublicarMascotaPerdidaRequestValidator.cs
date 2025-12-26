using encuentra_mascotas.Contracts.Requests;
using FluentValidation;

namespace encuentra_mascotas.Validators
{
   public class PublicarMascotaPerdidaRequestValidator : AbstractValidator<PublicarMascotaPerdidaRequest>
   {
      public PublicarMascotaPerdidaRequestValidator()
      {
         //Mascota
         RuleFor(x => x.NombreMascota)
               .NotEmpty().WithMessage("El nombre de la mascota es obligatorio.")
               .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

         RuleFor(x => x.RazaId)
            .NotEmpty().WithMessage("La raza es obligatoria.");

         RuleFor(x => x.ColorPrincipal)
            .NotEmpty().WithMessage("El color principal es obligatorio.")
            .MaximumLength(50).WithMessage("El color no puede exceder 50 caracteres.");

         RuleFor(x => x.DescripcionMascota)
            .NotEmpty().WithMessage("La descripción de la mascota es obligatoria.")
            .MaximumLength(200).WithMessage("La descripción no puede exceder 200 caracteres.");

         RuleFor(x => x.TamanioMascota)
            .IsInEnum().WithMessage("El tamaño de la mascota es inválido.");

         RuleFor(x => x.Sexo)
            .IsInEnum().WithMessage("El sexo de la mascota es inválido.");

         // Ubicación
         RuleFor(x => x.Provincia)
            .NotEmpty().WithMessage("La provincia es obligatoria.")
            .MaximumLength(100).WithMessage("La provincia no puede exceder 100 caracteres.");

         RuleFor(x => x.Distrito)
            .NotEmpty().WithMessage("El distrito es obligatorio.")
            .MaximumLength(100).WithMessage("El distrito no puede exceder 100 caracteres.");

         RuleFor(x => x.Barrio)
            .NotEmpty().WithMessage("El barrio es obligatorio.")
            .MaximumLength(100).WithMessage("El barrio no puede exceder 100 caracteres.");

         // Publicación
         RuleFor(x => x.DescripcionPublicacion)
            .NotEmpty().WithMessage("La descripción de la publicación es obligatoria.")
            .MaximumLength(1000).WithMessage("La descripción no puede exceder 1000 caracteres.");

         RuleFor(x => x.FechaPerdido)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("La fecha de pérdida no puede ser futura.")
            .GreaterThan(DateTime.UtcNow.AddYears(-10)).WithMessage("La fecha de pérdida no puede ser anterior a 10 años.");

         RuleFor(x => x.EstadoMascota)
            .IsInEnum().WithMessage("El estado de la mascota es inválido.");

         // Fotos
         RuleFor(x => x.Fotos)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Debe incluir al menos una foto.")
            .Must(f => f.Count > 0).WithMessage("Debe incluir al menos una foto.")
            .Must(f => f.Count <= 10).WithMessage("No puede incluir más de 10 fotos.");


         //Validación individual de cada foto
         RuleForEach(x => x.Fotos)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("La foto no puede estar vacía.")
            .Must(f => f.Length > 0).WithMessage("La foto no puede estar vacía.")
            .Must(f => f.Length <= 5 * 1024 * 1024)
            .WithMessage("La foto no puede exceder 5MB.")
            .Must(IsValidImage)
         .WithMessage("Solo se permiten archivos de imagen (JPEG, PNG, etc.).")
         .When(x => x.Fotos != null && x.Fotos.Count > 0);

      }



      private bool IsValidImage(IFormFile file)
      {
         var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
         var extension = Path.GetExtension(file.FileName).ToLower();
         return allowedExtensions.Contains(extension);
      }
   
   }
}

