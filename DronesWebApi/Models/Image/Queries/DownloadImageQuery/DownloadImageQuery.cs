using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DronesWebApi.Core;
using MediatR;

namespace DronesWebApi.Models.Image.Queries.DownloadImageQuery
{
    public class DownloadImageQuery: IRequest<ImageDto>
    {
        public string MedicationCode { get; set; }
    }

    public class DownloadImageQueryHandler : IRequestHandler<DownloadImageQuery, ImageDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DownloadImageQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<ImageDto> Handle(DownloadImageQuery request, CancellationToken cancellationToken)
        {
            var image = _unitOfWork.Images.GetImageOfMedication(request.MedicationCode);

            return Task.FromResult(_mapper.Map<ImageDto>(image));
        }
    }
}
