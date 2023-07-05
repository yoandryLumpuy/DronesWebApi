using DronesWebApi.Core;
using DronesWebApi.Models.Medication.Commands.CreateMedicationCommand;
using FluentValidation;
using System.Linq;
using DronesWebApi.Models.Medication.Commands.LoadMedicationCommand;
using DronesWebApi.UnitTests.Validators.Medication.Commands.LoadMedicationCommand;

namespace DronesWebApi.Models.Medication.Commands.LoadMedicationListCommand
{
    public class LoadMedicationListCommandValidatorTester
    {
        public LoadMedicationListCommandValidatorTester(IUnitOfWork unitOfWork)
        {
            //RuleForEach(x => x.Items)
            //    .SetValidator(new LoadMedicationCommandValidatorTester(unitOfWork));

            //RuleFor(command => command)
            //    .Must(command =>
            //    {
            //        var itemsGroupedByDroneId = command.Items.GroupBy(i => i.DroneId);

            //        foreach (var groupItem in itemsGroupedByDroneId)
            //        {
            //            var drone = unitOfWork.Drones.Get(groupItem.Key);

            //            var medicationsToLoad = groupItem.Select(i => unitOfWork.Medications.Get(i.Code));

            //            if (unitOfWork.Drones.TotalLoadInGrams(groupItem.Key) + medicationsToLoad.Sum(i => i.WeightInGrams) > drone.WeightLimitInGrams)
            //                return false;
            //        }

            //        return true;

            //    }).WithMessage("Sum of weight of medications exceeds Drone weight limit");
        }
    }
}
