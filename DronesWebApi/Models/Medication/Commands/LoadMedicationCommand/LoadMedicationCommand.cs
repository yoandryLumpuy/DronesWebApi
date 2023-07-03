using System.Threading;
using System.Threading.Tasks;
using DronesWebApi.Core;
using DronesWebApi.Core.Domain.Enums;
using DronesWebApi.Models.Drone.Commands.CreateDroneCommand;
using DronesWebApi.Models.Medication.Commands.CreateMedicationCommand;
using MediatR;

namespace DronesWebApi.Models.Medication.Commands.LoadMedicationCommand
{
    public class LoadMedicationCommand : IRequest<CreateMedicationCommandResponse>
    {
        public string Code { get; set; }

        public int DroneId { get; set; }
    }

    public class LoadMedicationCommandHandler : IRequestHandler<LoadMedicationCommand, CreateMedicationCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoadMedicationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<CreateMedicationCommandResponse> Handle(LoadMedicationCommand request, CancellationToken cancellationToken)
        {
            var medication = _unitOfWork.Medications.GetWithDronesReferences(request.Code);

            var drone = _unitOfWork.Drones.Get(request.DroneId);

            medication.Drone = drone;

            drone.State = EDroneState.Loading;

            _unitOfWork.Complete();

            return Task.FromResult(new CreateMedicationCommandResponse()
            {
                Code = request.Code,
                DroneId = request.DroneId
            });
        }
    }
}
