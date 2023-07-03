using DronesWebApi.Core;
using FluentValidation;

namespace DronesWebApi.Models.Drone.Queries.GetDroneBatteryLevelQuery
{
    public class GetDroneBatteryLevelQueryValidator: AbstractValidator<GetDroneBatteryLevelQuery>
    {
        public GetDroneBatteryLevelQueryValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(command => command.DroneId)
                .Must(droneId =>
                {
                    var drone = unitOfWork.Drones.Get(droneId);

                    return drone != null;

                }).WithMessage(command => $"Drone with id: '{command.DroneId}' does not exists");
        }
    }
}
