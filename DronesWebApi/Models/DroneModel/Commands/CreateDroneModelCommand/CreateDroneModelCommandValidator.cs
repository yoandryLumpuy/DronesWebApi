using DronesWebApi.Models.Drone.Commands.CreateDroneCommand;
using FluentValidation;

namespace DronesWebApi.Models.DroneModel.Commands.CreateDroneModelCommand
{
    public class CreateDroneModelCommandValidator: AbstractValidator<CreateDroneModelCommand>
    {
        public CreateDroneModelCommandValidator()
        {
            RuleFor(command => command.Name).NotNull().WithMessage($"Name can't be null")
                .NotEmpty().WithMessage($"Name can't be empty")
                .MaximumLength(100).WithMessage($"Maximum length of Name is 100");

            RuleFor(m => m.CruiserweightInGrams)
                .GreaterThan(0)
                .WithMessage($"Field {nameof(CreateDroneModelCommand.CruiserweightInGrams)} must be greater than zero");

            RuleFor(m => m.MiddleweightInGrams)
                .GreaterThan(0)
                .WithMessage($"Field {nameof(CreateDroneModelCommand.MiddleweightInGrams)} must be greater than zero");

            RuleFor(m => m.LightweightInGrams)
                .GreaterThan(0)
                .WithMessage($"Field {nameof(CreateDroneModelCommand.LightweightInGrams)} must be greater than zero");

            RuleFor(m => m.HeavyweightInGrams)
                .GreaterThan(0)
                .WithMessage($"Field {nameof(CreateDroneModelCommand.HeavyweightInGrams)} must be greater than zero");
        }
    }
}
