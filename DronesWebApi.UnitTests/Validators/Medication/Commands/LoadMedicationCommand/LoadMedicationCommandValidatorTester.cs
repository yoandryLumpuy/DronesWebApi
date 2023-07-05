using System;
using System.Collections.Generic;
using DronesWebApi.Commons.Constants;
using DronesWebApi.Core;
using DronesWebApi.Core.Domain.Enums;
using DronesWebApi.Models.Medication.Commands.LoadMedicationCommand;
using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace DronesWebApi.UnitTests.Validators.Medication.Commands.LoadMedicationCommand
{
    public class LoadMedicationCommandValidatorTester
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly LoadMedicationCommandValidator _validator;

        public LoadMedicationCommandValidatorTester()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _validator = new LoadMedicationCommandValidator(_unitOfWork.Object)
            {
                RuleLevelCascadeMode = CascadeMode.Stop,
                ClassLevelCascadeMode = CascadeMode.Stop
            };
        }

        [Fact]
        public void Should_throw_error_when_Code_is_null()
        {
            var model = new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
            {
                Code = null,
                DroneId = 1
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Should_throw_error_when_Code_is_empty()
        {
            var model = new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
            {
                Code = "  ",
                DroneId = 1
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Should_throw_error_when_Medication_does_not_exists()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns((Core.Domain.Medication)null);

            var model = new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
            {
                Code = "AA",
                DroneId = 1
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Should_throw_error_when_droneId_is_not_greater_or_equals_to_zero()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Medication());

            var model = new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
            {
                Code = "AA",
                DroneId = -1
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.DroneId);
        }

        [Fact]
        public void Should_throw_error_when_drone_does_not_exists()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Medication());

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns((Core.Domain.Drone)null);

            var model = new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
            {
                Code = "AA",
                DroneId = 1
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.DroneId);
        }

        [Fact]
        public void Should_throw_error_when_drone_state_is_not_Idle_nor_Loading()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Medication());

            var droneStates = new List<EDroneState>()
                { EDroneState.Delivered, EDroneState.Delivering, EDroneState.Loaded, EDroneState.Returning };

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Drone()
            {
                State = droneStates[new Random().Next(0, droneStates.Count)] 
            });

            var model = new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
            {
                Code = "AA",
                DroneId = 1
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.DroneId);
        }

        [Fact]
        public void Should_throw_error_when_medication_was_already_delivered()
        {
            var drone = new Core.Domain.Drone()
            {
                Id = 1,
                State = EDroneState.Loading
            };

            var medication = new Core.Domain.Medication()
            {
                DeliveredByDrone = drone,
                DatetimeDelivery = DateTime.Now
            };

            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(medication);

            _unitOfWork.Setup(x => x.Medications.GetWithDronesReferences(It.IsAny<string>())).Returns(medication);

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns(drone);

            var model = new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
            {
                Code = "AA",
                DroneId = drone.Id
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Should_throw_error_when_medication_is_already_assigned_to_another_drone_which_is_no_longer_in_Idle_or_Loading_state()
        {
            var assignedToDrone = new Core.Domain.Drone()
            {
                Id = 2,
                State = EDroneState.Delivering
            };

            var drone = new Core.Domain.Drone()
            {
                Id = 1,
                State = EDroneState.Loading
            };

            var medication = new Core.Domain.Medication()
            {
                Drone = assignedToDrone
            };

            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(medication);

            _unitOfWork.Setup(x => x.Medications.GetWithDronesReferences(It.IsAny<string>())).Returns(medication);

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns(drone);

            var model = new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
            {
                Code = "AA",
                DroneId = drone.Id
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Should_throw_error_when_drone_battery_capacity_is_less_than_minimum_required_value()
        {
            var drone = new Core.Domain.Drone()
            {
                Id = 1,
                State = EDroneState.Loading,
                BatteryCapacityInPercentage = Constants.MinPercentageOfBatteryLevelRequired - 1
            };

            var medication = new Core.Domain.Medication();

            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(medication);

            _unitOfWork.Setup(x => x.Medications.GetWithDronesReferences(It.IsAny<string>())).Returns(medication);

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns(drone);

            var model = new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
            {
                Code = "AA",
                DroneId = drone.Id
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Should_throw_error_when_load_exceeds_drone_weight_limit()
        {
            var drone = new Core.Domain.Drone()
            {
                Id = 1,
                State = EDroneState.Loading,
                BatteryCapacityInPercentage = Constants.MinPercentageOfBatteryLevelRequired,
                WeightLimitInGrams = 500
            };

            var medication = new Core.Domain.Medication()
            {
                WeightInGrams = 400 + 1
            };

            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(medication);

            _unitOfWork.Setup(x => x.Medications.GetWithDronesReferences(It.IsAny<string>())).Returns(medication);

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns(drone);

            _unitOfWork.Setup(x => x.Drones.TotalLoadInGrams(It.IsAny<int>())).Returns(100);

            var model = new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
            {
                Code = "AA",
                DroneId = drone.Id
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Should_not_throw_any_error()
        {
            var drone = new Core.Domain.Drone()
            {
                Id = 1,
                State = EDroneState.Loading,
                BatteryCapacityInPercentage = Constants.MinPercentageOfBatteryLevelRequired,
                WeightLimitInGrams = 500
            };

            var medication = new Core.Domain.Medication()
            {
                WeightInGrams = 400
            };

            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(medication);

            _unitOfWork.Setup(x => x.Medications.GetWithDronesReferences(It.IsAny<string>())).Returns(medication);

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns(drone);

            _unitOfWork.Setup(x => x.Drones.TotalLoadInGrams(It.IsAny<int>())).Returns(100);

            var model = new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
            {
                Code = "AA",
                DroneId = drone.Id
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x);
        }
    }
}
