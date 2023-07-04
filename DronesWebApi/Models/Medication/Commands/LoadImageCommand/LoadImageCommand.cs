using System.Threading;
using System.Threading.Tasks;
using DronesWebApi.Core;
using DronesWebApi.Core.Domain;
using DronesWebApi.Infrastructure.FileManager;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DronesWebApi.Models.Medication.Commands.LoadImageCommand
{
    public class LoadImageCommand: IRequest<LoadImageCommandResponse>
    {
        public IFormFile File { get; set; }

        public string MedicationCode { get; set; }
    }

    public class LoadImageCommandHandler : IRequestHandler<LoadImageCommand, LoadImageCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;

        public LoadImageCommandHandler(IUnitOfWork unitOfWork, IFileManager fileManager)
        {
            _fileManager = fileManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoadImageCommandResponse> Handle(LoadImageCommand request, CancellationToken cancellationToken)
        {
            var fileInfo = await _fileManager.ConvertToBytesArray(request.File);

            var image = new Image()
            {
                FileName = fileInfo.FileName,
                ContentType = fileInfo.ContentType,
                Data = fileInfo.Data,
                MedicationCode = request.MedicationCode
            };

            _unitOfWork.Images.Add(image);

            _unitOfWork.Complete();

            return new LoadImageCommandResponse() { ImageId = image.Id, MedicationCode = request.MedicationCode };
        }
    }
}
