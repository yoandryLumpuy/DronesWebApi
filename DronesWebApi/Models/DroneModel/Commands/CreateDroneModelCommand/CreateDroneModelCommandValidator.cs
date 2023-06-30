using DronesWebApi.Models.Drone.Commands.CreateDroneCommand;
using FluentValidation;

namespace DronesWebApi.Models.DroneModel.Commands.CreateDroneModelCommand
{
    public class CreateDroneModelCommandValidator: AbstractValidator<CreateDroneModelCommand>
    {
        public CreateDroneModelCommandValidator()
        { }
    }
}
