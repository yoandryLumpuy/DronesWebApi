using System.Linq;
using DronesWebApi.Core;
using DronesWebApi.Models.Medication.Commands.CreateMedicationCommand;
using FluentValidation;

namespace DronesWebApi.Models.Medication.Commands.CreateMedicationsListCommand
{
    public class CreateMedicationsListCommandValidator: AbstractValidator<CreateMedicationsListCommand>
    {
        public CreateMedicationsListCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleForEach(x => x.Items)
                .SetValidator(new CreateMedicationCommandValidator(unitOfWork));

            RuleFor(command => command)
                .Must(command =>
                {
                    var itemsGroupedByDroneId = command.Items.GroupBy(i => i.DroneId);

                    foreach (var groupItem in itemsGroupedByDroneId.Where(groupItem => groupItem.Key.HasValue))
                    {
                        var drone = unitOfWork.Drones.Get(groupItem.Key);

                        if (unitOfWork.Drones.TotalLoadInGrams(groupItem.Key.Value) + groupItem.Sum(i => i.WeightInGrams) > drone.WeightLimitInGrams)
                            return false;
                    }

                    return true;                    
                })
                .WithMessage("Sum of weight of medications exceeds Drone weight limit");
        }
    }
}
