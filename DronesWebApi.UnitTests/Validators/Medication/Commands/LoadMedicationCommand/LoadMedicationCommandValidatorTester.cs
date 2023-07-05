using System.Collections.Generic;
using DronesWebApi.Commons.Constants;
using DronesWebApi.Core;
using DronesWebApi.Core.Domain.Enums;
using FluentValidation;

namespace DronesWebApi.UnitTests.Validators.Medication.Commands.LoadMedicationCommand
{
    public class LoadMedicationCommandValidatorTester
    {
        public LoadMedicationCommandValidatorTester(IUnitOfWork unitOfWork)
        {
            //RuleFor(command => command.Code)
            //    .NotNull().WithMessage($"Field Code can't be null")
            //    .NotEmpty().WithMessage($"Field Code can't not empty")
            //    .Must(code =>
            //    {
            //        var medication = unitOfWork.Medications.Get(code);

            //        return medication != null;
            //    })
            //    .WithMessage(command => $"Medication with code '{command.Code}' does not exists");

            //RuleFor(command => command.DroneId)
            //    .GreaterThanOrEqualTo(0).WithMessage($"DroneId parameter must be greater than or equals to zero")
            //    .Must(droneId =>
            //    {
            //        var drone = unitOfWork.Drones.Get(droneId);

            //        return drone != null;
            //    }).WithMessage(command => $"Drone with id '{command.DroneId}' does not exists")
            //    .Must(droneId =>
            //    {
            //        var drone = unitOfWork.Drones.Get(droneId);

            //        return new List<EDroneState>() { EDroneState.Idle, EDroneState.Loading }.Contains(drone.State);
            //    }).WithMessage(command => $"Drone with id '{command.DroneId}' is no longer in Idle or Loading state");

            //RuleFor(command => command)
            //    .Must(command =>
            //    {
            //        var medication = unitOfWork.Medications.GetWithDronesReferences(command.Code);

            //        return !medication.DatetimeDelivery.HasValue && medication.DeliveredByDrone == null;
            //    }).WithMessage("Medication was already delivered to user")

            //    .Must(command =>
            //    {
            //        var medication = unitOfWork.Medications.GetWithDronesReferences(command.Code);

            //        return medication.Drone == null || medication.Drone.Id == command.DroneId 
            //               || new List<EDroneState>() { EDroneState.Idle, EDroneState.Loading }.Contains(medication.Drone.State);
            //    }).WithMessage("Medication was already assigned to another Drone which is not longer in Idle or Loading state")

            //    .Must(command =>
            //    {
            //        var drone = unitOfWork.Drones.Get(command.DroneId);

            //        var medication = unitOfWork.Medications.Get(command.Code);

            //        return drone.BatteryCapacityInPercentage >= Constants.MinPercentageOfBatteryLevelRequired;

            //    }).WithMessage(command =>
            //        $"Drone battery level is less than required minimum value of '{Constants.MinPercentageOfBatteryLevelRequired}'. " +
            //        $"DroneId: '{command.DroneId}'")

            //    .Must(command =>
            //    {
            //        var drone = unitOfWork.Drones.Get(command.DroneId);

            //        var medication = unitOfWork.Medications.Get(command.Code);
                    
            //        return unitOfWork.Drones.TotalLoadInGrams(command.DroneId) + medication.WeightInGrams <= drone.WeightLimitInGrams;

            //    }).WithMessage(command => $"Weight limit is exceeded for Drone with Id '{command.DroneId}'");
        }
    }
}
