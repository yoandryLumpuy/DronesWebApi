using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DronesWebApi.Commons.Exceptions;
using DronesWebApi.Core;
using DronesWebApi.Models.DroneModel.Queries.GetDroneModelQuery;
using MediatR;

namespace DronesWebApi.Models.Drone.Queries.GetDroneQuery
{
    public class GetDroneQuery: IRequest<DroneDto>
    {
        public int Id { get; set; }
    }

    public class GetDroneQueryHandler : IRequestHandler<GetDroneQuery, DroneDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDroneQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<DroneDto> Handle(GetDroneQuery request, CancellationToken cancellationToken)
        {
            var entity = _unitOfWork.Drones.GetWithLoadedMedications(request.Id);

            if (entity == null)
                throw new NotFoundException(message: $"Drone with id '{request.Id}' was not found");

            return Task.FromResult(_mapper.Map<DroneDto>(entity));
        }
    }
}
