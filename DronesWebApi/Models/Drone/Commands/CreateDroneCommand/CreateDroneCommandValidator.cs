using FluentValidation;

namespace DronesWebApi.Models.Drone.Commands.CreateDroneCommand
{
    public class CreateDroneCommandValidator: AbstractValidator<CreateDroneCommand>
    {
        public CreateDroneCommandValidator()
        {
            RuleFor(d => d.SerialNumber).MaximumLength(100);

            RuleFor(d => d.BatteryCapacityInPercentage)
                .GreaterThanOrEqualTo(0)
                .WithMessage($"{nameof(CreateDroneCommand.BatteryCapacityInPercentage)} field must be greater or equals to 0")
                .LessThanOrEqualTo(100)
                .WithMessage($"{nameof(CreateDroneCommand.BatteryCapacityInPercentage)} field must be less or equals to 100");

            RuleFor(d => d.WeightLimitInGrams)
                .GreaterThanOrEqualTo(0)
                .WithMessage($"{nameof(CreateDroneCommand.WeightLimitInGrams)} field can't be negative")
                .LessThanOrEqualTo(500)
                .WithMessage($"{nameof(CreateDroneCommand.WeightLimitInGrams)} field can't be greater than 500");
        }
    }
}
