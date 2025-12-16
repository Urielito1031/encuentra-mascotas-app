using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Publicaciones.CrearPublicacion
{
   public class CrearPublicacionUseCase : IRequestHandler<CrearPublicacionCommand,CrearPublicacionResult>
   {
      private readonly IPublicacionRepository  _repository;
      private readonly IMascotaRepository      _mascotaRepository;
      private readonly IUbicacionRepository    _ubicacionRepository;
      private readonly IImagenEmbeddingService _embeddingService;

      public CrearPublicacionUseCase(

         IPublicacionRepository repository,
         IMascotaRepository mascotaRepository,
         IUbicacionRepository ubicacionRepository,
         IImagenEmbeddingService embeddingService)
      {
         _repository = repository;
         _mascotaRepository = mascotaRepository;
         _ubicacionRepository = ubicacionRepository;
         _embeddingService = embeddingService;
      }

      public async Task<CrearPublicacionResult> Handle(
         CrearPublicacionCommand request,
         CancellationToken cancellationToken
         )
      {
         var mascota = await _mascotaRepository.ObtenerPorIdAsync(request.MascotaId);
         var ubicacion = await _ubicacionRepository.ObtenerPorIdAsync(request.UbicacionId);
         
         var publicacion = Publicacion.Crear(
          request.UsuarioId,
          mascota,
          ubicacion,
          request.Descripcion,
          request.FechaPerdido,
          request.EstadoMascota
      );

         //ver logica agregar foto!

         await _repository.AgregarAsync(publicacion);

         return new CrearPublicacionResult(publicacion.Id);

      }

   }
}
