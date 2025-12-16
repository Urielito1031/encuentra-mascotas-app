using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;

namespace Application.UseCases.Publicaciones.CrearPublicacion
{
   public class CrearPublicacionUseCase
   {
      private readonly IPublicacionRepository _repository;

      public CrearPublicacionUseCase(IPublicacionRepository repository)
      {
         _repository = repository;
      }
      public async Task<Guid> EjecutarAsync(
         Guid usuarioId,
         Mascota mascota,
         Ubicacion ubicacion,
         string descripcion,
         DateTime fechaPerdido,
         EstadoMascota estadoMascota) 
      {
         var publicacion = Publicacion.Crear(
            usuarioId,
            mascota,
            ubicacion,
            descripcion, 
            fechaPerdido, 
            estadoMascota
            );
        
         await _repository.AgregarAsync( publicacion );

         return publicacion.Id; 
      }

   }
}
