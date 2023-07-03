using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DronesWebApi.Commons.Exceptions;
using DronesWebApi.Core;
using MediatR;

namespace DronesWebApi.Models.DroneModel.Queries.GetDroneModelQuery
{
    public class GetDroneModelQuery : IRequest<DroneModelDto>
    {
        public int ModelId { get; set; }
    }

    public class GetDroneModelQueryHandler : IRequestHandler<GetDroneModelQuery, DroneModelDto>
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public GetDroneModelQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<DroneModelDto> Handle(GetDroneModelQuery request, CancellationToken cancellationToken)
        {
            var droneModel = _unitOfWork.DroneModels.Get(request.ModelId);

            return Task.FromResult(_mapper.Map<DroneModelDto>(droneModel));
        }
    }
}
