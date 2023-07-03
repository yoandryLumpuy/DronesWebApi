using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DronesWebApi.Core;
using DronesWebApi.Models.Drone.Queries.GetDroneQuery;
using MediatR;

namespace DronesWebApi.Models.Drone.Queries.GetAllAvailableDronesForLoadingQuery
{
    public class GetAllAvailableDronesForLoadingQuery: IRequest<IEnumerable<DroneDto>>
    { }

    public class GetAllAvailableDronesForLoadingQueryHandler : IRequestHandler<GetAllAvailableDronesForLoadingQuery, IEnumerable<DroneDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAvailableDronesForLoadingQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<DroneDto>> Handle(GetAllAvailableDronesForLoadingQuery request, CancellationToken cancellationToken)
        {
            var elements = _unitOfWork.Drones.GetAvailableForLoading();

            return Task.FromResult(_mapper.Map<IEnumerable<DroneDto>>(elements));
        }
    }
}
