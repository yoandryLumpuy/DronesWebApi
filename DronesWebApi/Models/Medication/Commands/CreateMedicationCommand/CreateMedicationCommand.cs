using MediatR;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using DronesWebApi.Commons.Constants;
using DronesWebApi.Commons.Exceptions;
using DronesWebApi.Core;
using DronesWebApi.Core.Domain.Enums;

namespace DronesWebApi.Models.Medication.Commands.CreateMedicationCommand
{
    public class CreateMedicationCommand : IRequest<CreateMedicationCommandResponse>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int WeightInGrams { get; set; }

        /// <summary>
        /// This field can be NULL if user just want to upload medication to repository and load in drone at a later time. 
        /// </summary>
        public int? DroneId { get; set; }
    }

    public class CreateMedicationCommandHandler : IRequestHandler<CreateMedicationCommand, CreateMedicationCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMedicationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<CreateMedicationCommandResponse> Handle(CreateMedicationCommand request, CancellationToken cancellationToken)
        {
            Core.Domain.Drone drone = null;

            var medication = new Core.Domain.Medication()
            {
                Code = request.Code,
                Name = request.Name,
                WeightInGrams = request.WeightInGrams,
                Drone = request.DroneId.HasValue ? drone = _unitOfWork.Drones.Get(request.DroneId.Value) : null
            };

            if (drone != null)
                drone.State = EDroneState.Loading;

            _unitOfWork.Medications.Add(medication);

            _unitOfWork.Complete();

            return Task.FromResult(new CreateMedicationCommandResponse()
            {
                Code = request.Code,
                DroneId = drone?.Id
            });
        }
    }
}
