using DronesWebApi.Core;
using DronesWebApi.Models.Medication.Commands.CreateMedicationCommand;
using FluentValidation;
using System.Linq;
using DronesWebApi.Models.Medication.Commands.LoadMedicationCommand;

namespace DronesWebApi.Models.Medication.Commands.LoadMedicationListCommand
{
    public class LoadMedicationListCommandValidator: AbstractValidator<LoadMedicationListCommand>
    {
        public LoadMedicationListCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleForEach(x => x.Commands)
                .SetValidator(new LoadMedicationCommandValidator(unitOfWork));

            RuleFor(command => command)
                .Must(command =>
                {
                    var itemsGroupedByDroneId = command.Commands.GroupBy(i => i.DroneId);

                    foreach (var groupItem in itemsGroupedByDroneId)
                    {
                        var drone = unitOfWork.Drones.Get(groupItem.Key);

                        var medicationsToLoad = groupItem.Select(i => unitOfWork.Medications.Get(i.Code));

                        if (unitOfWork.Drones.TotalLoadInGrams(groupItem.Key) + medicationsToLoad.Sum(i => i.WeightInGrams) > drone.WeightLimitInGrams)
                            return false;
                    }

                    return true;

                }).WithMessage("Sum of weight of medications exceeds Drone weight limit");
        }
    }
}
