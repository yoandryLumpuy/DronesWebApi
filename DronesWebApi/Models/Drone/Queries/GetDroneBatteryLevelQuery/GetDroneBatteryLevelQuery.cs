using System.Threading;
using System.Threading.Tasks;
using DronesWebApi.Core;
using MediatR;

namespace DronesWebApi.Models.Drone.Queries.GetDroneBatteryLevelQuery
{
    public class GetDroneBatteryLevelQuery: IRequest<GetDroneBatteryLevelQueryResponse>
    {
        public int DroneId { get; set; }
    }

    public class GetDroneBatteryLevelQueryHandler : IRequestHandler<GetDroneBatteryLevelQuery, GetDroneBatteryLevelQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDroneBatteryLevelQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<GetDroneBatteryLevelQueryResponse> Handle(GetDroneBatteryLevelQuery request, CancellationToken cancellationToken)
        {
            var drone = _unitOfWork.Drones.Get(request.DroneId);

            return Task.FromResult(new GetDroneBatteryLevelQueryResponse()
            {
                DroneId = request.DroneId, BatteryLevel = drone.BatteryCapacityInPercentage
            });
        }
    }
}
