using DronesWebApi.Core;
using FluentValidation;

namespace DronesWebApi.UnitTests.Validators.Drone.Queries.GetDroneBatteryLevelQuery
{
    public class GetDroneBatteryLevelQueryValidatorTester
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDroneBatteryLevelQueryValidatorTester()
        {

            //RuleFor(command => command.DroneId)
            //    .Must(droneId =>
            //    {
            //        var drone = unitOfWork.Drones.Get(droneId);

            //        return drone != null;

            //    }).WithMessage(command => $"Drone with id: '{command.DroneId}' does not exists");
        }
    }
}
