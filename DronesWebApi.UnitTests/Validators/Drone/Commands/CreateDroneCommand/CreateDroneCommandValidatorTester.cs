using System;
using System.Linq;
using DronesWebApi.Models.Drone.Commands.CreateDroneCommand;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace DronesWebApi.UnitTests.Validators.Drone.Commands.CreateDroneCommand
{
    public class CreateDroneCommandValidatorTester
    {
        private readonly CreateDroneCommandValidator _validator;

        public CreateDroneCommandValidatorTester()
        {
            _validator = new CreateDroneCommandValidator();
        }

        [Fact]
        public void Should_throw_error_when_serial_number_length_is_greater_than_100()
        {
            var model = new Models.Drone.Commands.CreateDroneCommand.CreateDroneCommand()
            {
                SerialNumber = string.Join(string.Empty, Enumerable.Repeat(Guid.NewGuid().ToString(), 100))
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(r => r.SerialNumber);
        }

        [Fact]
        public void Should_not_throw_error_when_serial_number_length_is_less_than_100()
        {
            var model = new Models.Drone.Commands.CreateDroneCommand.CreateDroneCommand()
            {
                SerialNumber = string.Join(string.Empty, Enumerable.Repeat("a", 100))
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(r => r.SerialNumber);
        }

        [Fact]
        public void Should_throw_error_when_battery_capacity_is_less_that_0()
        {
            var model = new Models.Drone.Commands.CreateDroneCommand.CreateDroneCommand()
            {
                BatteryCapacityInPercentage = -1
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(r => r.BatteryCapacityInPercentage);
        }

        [Fact]
        public void Should_throw_error_when_battery_capacity_is_greater_than_100()
        {
            var model = new Models.Drone.Commands.CreateDroneCommand.CreateDroneCommand()
            {
                BatteryCapacityInPercentage = 101
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(r => r.BatteryCapacityInPercentage);
        }

        [Fact]
        public void Should_not_throw_error_when_battery_capacity_is_between_0_and_100()
        {
            var model = new Models.Drone.Commands.CreateDroneCommand.CreateDroneCommand()
            {
                BatteryCapacityInPercentage = new Random().Next(0, 101)
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(r => r.BatteryCapacityInPercentage);
        }

        [Fact]
        public void Should_throw_error_when_weight_limit_is_less_than_0()
        {
            var model = new Models.Drone.Commands.CreateDroneCommand.CreateDroneCommand()
            {
                WeightLimitInGrams = -1
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(r => r.WeightLimitInGrams);
        }

        [Fact]
        public void Should_throw_error_when_weight_limit_is_greater_than_500()
        {
            var model = new Models.Drone.Commands.CreateDroneCommand.CreateDroneCommand()
            {
                WeightLimitInGrams = 501
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(r => r.WeightLimitInGrams);
        }

        [Fact]
        public void Should_not_throw_error_when_weight_limit_is_between_0_and_500()
        {
            var model = new Models.Drone.Commands.CreateDroneCommand.CreateDroneCommand()
            {
                WeightLimitInGrams = new Random().Next(0, 501)
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(r => r.WeightLimitInGrams);
        }
    }
}
