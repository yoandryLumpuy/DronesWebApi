using DronesWebApi.Core;
using DronesWebApi.Infrastructure.FileManager;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DronesWebApi.Models.Image.Commands.UploadImageCommand
{
    public class UploadImageCommand : IRequest<UploadImageCommandResponse>
    {
        public IFormFile File { get; set; }

        public string MedicationCode { get; set; }
    }

    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, UploadImageCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;

        public UploadImageCommandHandler(IUnitOfWork unitOfWork, IFileManager fileManager)
        {
            _fileManager = fileManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<UploadImageCommandResponse> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var fileInfo = await _fileManager.ConvertToBytesArray(request.File);

            var image = new Core.Domain.Image()
            {
                FileName = fileInfo.FileName,
                ContentType = fileInfo.ContentType,
                Data = fileInfo.Data,
                MedicationCode = request.MedicationCode
            };

            _unitOfWork.Images.Add(image);

            _unitOfWork.Complete();

            return new UploadImageCommandResponse() { ImageId = image.Id, MedicationCode = request.MedicationCode };
        }
    }
}
