using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DronesWebApi.Core;
using DronesWebApi.Core.Repositories;
using DronesWebApi.Models.Drone.Queries.GetDroneQuery;
using DronesWebApi.Persistence.Repositories;
using MediatR;

namespace DronesWebApi.Models.Drone.Queries.GetDronesWithPaginationQuery
{
    public class GetDronesWithPaginationQuery: PaginatedListRequest, IRequest<IPaginatedList<DroneDto>> 
    { }

    public class GetDronesWithPaginationQueryHandler: IRequestHandler<GetDronesWithPaginationQuery, IPaginatedList<DroneDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetDronesWithPaginationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<IPaginatedList<DroneDto>> Handle(GetDronesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var elements = _unitOfWork.Drones.GetPaginatedWithLoadedMedications(request.PageNumber, request.PageSize);

            return Task.FromResult(elements.Map<Core.Domain.Drone, DroneDto>(_mapper));
        }
    }
}
