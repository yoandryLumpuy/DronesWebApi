using System.Text.RegularExpressions;
using DronesWebApi.Commons.Constants;
using DronesWebApi.Commons.Exceptions;
using DronesWebApi.Core;
using DronesWebApi.Core.Domain;
using FluentValidation;
using MediatR;

namespace DronesWebApi.Models.Medication.Commands.CreateMedicationCommand
{
    public class CreateMedicationCommandValidator : AbstractValidator<CreateMedicationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMedicationCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(m => m.Name)
                .Must(name => Regex.IsMatch(input: name, pattern: @"^[a-zA-Z\\d\\-_]+$"))
                .WithMessage($"{nameof(CreateMedicationCommand.Name)} field only accepts: letters, numbers, '-' and '_' characters");

            RuleFor(m => m.Code)
                .Must(code => Regex.IsMatch(input: code, pattern: @"^[A-Z\\d_]+$"))
                .WithMessage($"{nameof(CreateMedicationCommand.Code)} only accepts: upper case letters, underscore and numbers");


            RuleFor(createMedicationCommand => createMedicationCommand)
                .Must(createMedicationCommand =>
                {
                    if (!createMedicationCommand.DroneId.HasValue) return true;

                    var drone = unitOfWork.Drones.Get(createMedicationCommand.DroneId.Value);

                    return drone != null;

                }).WithMessage(createMedicationCommand => $"Drone with id: '{createMedicationCommand.DroneId.Value}' was not found")

                .Must(createMedicationCommand =>
                {
                    if (!createMedicationCommand.DroneId.HasValue) return true;

                    var drone = unitOfWork.Drones.Get(createMedicationCommand.DroneId.Value);

                    return drone.BatteryCapacityInPercentage >= Constants.MinPercentageOfBatteryLevelRequired;

                }).WithMessage(createMedicationCommand => 
                    $"Drone battery level is less than required value of '{Constants.MinPercentageOfBatteryLevelRequired}'. " +
                    $"DroneId: '{createMedicationCommand.DroneId.Value}'")

                .Must(createMedicationCommand =>
                {
                    if (!createMedicationCommand.DroneId.HasValue) return true;

                    var drone = unitOfWork.Drones.Get(createMedicationCommand.DroneId.Value);

                    return unitOfWork.Drones.TotalLoadInGrams(createMedicationCommand.DroneId.Value) +
                           createMedicationCommand.WeightInGrams <= drone.WeightLimitInGrams;

                }).WithMessage(createMedicationCommand => $"Weight limit is exceeded for Drone with Id '{createMedicationCommand.DroneId.Value}'");
        }
    }
}
