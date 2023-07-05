using DronesWebApi.Core;
using FluentValidation;

namespace DronesWebApi.UnitTests.Validators.Medication.Queries.GetLoadedMedicationsQuery
{
    public class GetLoadedMedicationsQueryValidatorTester
    {
        public GetLoadedMedicationsQueryValidatorTester(IUnitOfWork unitOfWork)
        {
            //RuleFor(getLoadedMedicationsQuery => getLoadedMedicationsQuery.DroneId)
            //    .Must(droneId =>
            //    {
            //        var drone = unitOfWork.Drones.Get(droneId);

            //        return drone != null;

            //    }).WithMessage(getLoadedMedicationsQuery => $"Drone with id: '{getLoadedMedicationsQuery.DroneId}' was not found");
        }
    }
}
