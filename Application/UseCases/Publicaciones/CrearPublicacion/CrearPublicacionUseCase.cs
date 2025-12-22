using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Storage;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using MediatR;
using Pgvector;

namespace Application.UseCases.Publicaciones.CrearPublicacion
{
   public class CrearPublicacionUseCase : IRequestHandler<CrearPublicacionCommand,CrearPublicacionResult>
   {
      private readonly IPublicacionRepository  _repository;
      private readonly IMascotaRepository      _mascotaRepository;
      private readonly IUbicacionRepository    _ubicacionRepository;
      private readonly IImagenEmbeddingService _embeddingService;
      private readonly IFileStorageService     _fileStorageService;

      public CrearPublicacionUseCase(

         IPublicacionRepository repository,
         IMascotaRepository mascotaRepository,
         IUbicacionRepository ubicacionRepository,
         IImagenEmbeddingService embeddingService,
         IFileStorageService fileStorageService)
      {
         _repository = repository;
         _mascotaRepository = mascotaRepository;
         _ubicacionRepository = ubicacionRepository;
         _embeddingService = embeddingService;
         _fileStorageService = fileStorageService;
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
         int orden = 0;
         foreach(var fotoFile in request.Fotos)
         {
            string url = await _fileStorageService.SubirArchivoAsync(fotoFile, ContenedoresBlob.Publicaciones);
            using var stream = fotoFile.OpenReadStream();
            Vector vector = _embeddingService.GenerarEmbedding(stream);


            var foto = Foto.Crear(
               url: url,  
               publicacionId: publicacion.Id,
               embeddingVector: vector,
               orden: orden++
               );
         
            publicacion.AgregarFoto(foto);
         };

         await _repository.AgregarAsync(publicacion);
         return new CrearPublicacionResult(publicacion.Id);

      }

   }
}
