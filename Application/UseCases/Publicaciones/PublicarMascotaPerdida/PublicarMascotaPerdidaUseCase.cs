using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.Publicaciones.PublicarMascotaPerdida
{
   public class PublicarMascotaPerdidaUseCase : IRequestHandler<PublicarMascotaPerdidaCommand, PublicarMascotaPerdidaResult>
   {

      private readonly IMascotaRepository _mascotaRepository;
      private readonly IUbicacionRepository _ubicacionRepository;
      private readonly IPublicacionRepository _publicacionRepository;
      private readonly IFileStorageService _fileStorage;
      private readonly IImagenEmbeddingService _embeddingService;
      private readonly IGeocodingService _geocodingService;

      public PublicarMascotaPerdidaUseCase(IMascotaRepository mascotaRepository,
         IUbicacionRepository ubicacionRepository, 
         IPublicacionRepository publicacionRepository,
         IFileStorageService fileStorage,
         IImagenEmbeddingService embeddingService,
         IGeocodingService geocodingService)
      {
         _mascotaRepository = mascotaRepository;
         _ubicacionRepository = ubicacionRepository;
         _publicacionRepository = publicacionRepository;
         _fileStorage = fileStorage;
         _embeddingService = embeddingService;
         _geocodingService = geocodingService;
      }

      public Task<PublicarMascotaPerdidaResult> Handle(PublicarMascotaPerdidaCommand request, CancellationToken cancellationToken)
      {


      }
   }
}
