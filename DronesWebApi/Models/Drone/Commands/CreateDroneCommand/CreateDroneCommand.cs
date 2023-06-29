using DronesWebApi.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using DronesWebApi.Core.Domain.Enums;

namespace DronesWebApi.Models.Drone.Commands.CreateDroneCommand
{
    public class CreateDroneCommand : IRequest<CreateCommandResponse>
    {
        public string SerialNumber { get; set; }

        public int ModelId { get; set; }

        public int WeightLimitInGrams { get; set; }

        public int BatteryCapacityInPercentage { get; set; }
    }

    public class CreateDroneCommandHandler : IRequestHandler<CreateDroneCommand, CreateCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDroneCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<CreateCommandResponse> Handle(CreateDroneCommand request, CancellationToken cancellationToken)
        {
            var entity = new Core.Domain.Drone()
            {
                SerialNumber = request.SerialNumber,
                ModelId = request.ModelId,
                BatteryCapacityInPercentage = request.BatteryCapacityInPercentage,
                WeightLimitInGrams = request.WeightLimitInGrams,
                State = EDroneState.Idle
            };

            _unitOfWork.Drones.Add(entity);

            _unitOfWork.Complete();

            return Task.FromResult(new CreateCommandResponse(){ Id = entity.Id });
        }
    }
}
