using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DronesWebApi.Commons.Constants;
using DronesWebApi.Core;
using DronesWebApi.Core.Domain.Enums;
using DronesWebApi.Models.Medication.Commands.CreateMedicationCommand;
using FluentAssertions.Equivalency;
using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace DronesWebApi.UnitTests.Validators.Medication.Commands.CreateMedicationCommand
{
    public class CreateMedicationCommandValidatorTester
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly CreateMedicationCommandValidator _validator;

        public CreateMedicationCommandValidatorTester()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _validator = new CreateMedicationCommandValidator(_unitOfWork.Object)
            {
                RuleLevelCascadeMode = CascadeMode.Stop,
                ClassLevelCascadeMode = CascadeMode.Stop
            };
        }
        
        [Theory]
        [MemberData(nameof(TestCasesForNameValidation))]
        public void Should_throw_error_when_Name_does_not_meet_Regex_expression(char name)
        {
            var model = new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
            {
                Name = name.ToString()
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        public static IEnumerable<object[]> TestCasesForNameValidation()
        {
            var sequence = new List<int>(Enumerable.Range(0, 256));

            for (var i = (int)'a'; i <= (int)'z'; i++)
                sequence.Remove(i);

            for (var i = (int)'A'; i <= (int)'Z'; i++)
                sequence.Remove(i);

            for (var i = (int)'0'; i <= (int)'9'; i++)
                sequence.Remove(i);

            sequence.Remove((int)'-');

            sequence.Remove((int)'_');

            var result = sequence.Select(elem => new object[] { (char) elem }).ToList();

            return result;
        }

        [Theory]
        [MemberData(nameof(PassingTestCasesForNameValidation))]
        public void Should_not_throw_error_when_Name_meets_Regex_expression(char name)
        {
            var model = new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
            {
                Name = name.ToString(),
                Code = "!"
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        public static IEnumerable<object[]> PassingTestCasesForNameValidation()
        {
            var sequence = new List<int>();

            for (var i = (int)'a'; i <= (int)'z'; i++)
                sequence.Add(i);

            for (var i = (int)'A'; i <= (int)'Z'; i++)
                sequence.Add(i);

            for (var i = (int)'0'; i <= (int)'9'; i++)
                sequence.Add(i);

            sequence.Add((int)'-');

            sequence.Add((int)'_');

            var result = sequence.Select(elem => new object[] { (char)elem }).ToList();

            return result;
        }

        [Theory]
        [MemberData(nameof(TestCasesForCodeValidation))]
        public void Should_throw_error_when_Code_does_not_meet_Regex_expression(char code)
        {
            var model = new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
            {
                Name = "AAA",
                Code = code.ToString()
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        public static IEnumerable<object[]> TestCasesForCodeValidation()
        {
            var sequence = new List<int>(Enumerable.Range(0, 256));

            for (var i = (int)'A'; i <= (int)'Z'; i++)
                sequence.Remove(i);

            for (var i = (int)'0'; i <= (int)'9'; i++)
                sequence.Remove(i);

            sequence.Remove((int)'_');

            var result = sequence.Select(elem => new object[] { (char)elem }).ToList();

            return result;
        }

        [Theory]
        [MemberData(nameof(PassingTestCasesForCodeValidation))]
        public void Should_not_throw_error_when_Code_meets_Regex_expression(char code)
        {
            var model = new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
            {
                Name = "AAA",
                Code = code.ToString()
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Code);
        }

        public static IEnumerable<object[]> PassingTestCasesForCodeValidation()
        {
            var sequence = new List<int>();

            for (var i = (int)'A'; i <= (int)'Z'; i++)
                sequence.Add(i);

            for (var i = (int)'0'; i <= (int)'9'; i++)
                sequence.Add(i);

            sequence.Add((int)'_');

            var result = sequence.Select(elem => new object[] { (char)elem }).ToList();

            return result;
        }
        
        [Fact]
        public void Should_throw_error_when_drone_does_not_exists()
        {
            var model = new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
            {
                Name = "AAA",
                Code = "AA",
                DroneId = 1
            };

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns((Core.Domain.Drone)null);

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Should_throw_error_when_drone_state_is_not_in_Idle_or_Loading()
        {
            var model = new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
            {
                Name = "AAA",
                Code = "AA",
                DroneId = 1
            };

            var states = new List<EDroneState>()
                { EDroneState.Delivered, EDroneState.Delivering, EDroneState.Loaded, EDroneState.Returning };

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Drone()
            {
                State = states[new Random().Next(0, states.Count)]
            });

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Theory]
        [MemberData(nameof(TestCasesForBatteryValidation))]
        public void Should_throw_error_when_drone_battery_level_is_not_greater_or_equals_to_minimum_required_value(int batteryCapacityInPercentage)
        {
            var model = new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
            {
                Name = "AAA",
                Code = "AA",
                DroneId = 1
            };

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Drone()
            {
                State = EDroneState.Loading,
                BatteryCapacityInPercentage = batteryCapacityInPercentage
            });

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        public static IEnumerable<object[]> TestCasesForBatteryValidation() =>
            Enumerable.Range(0, Constants.MinPercentageOfBatteryLevelRequired).Select(i => new object[] { i });

        [Fact]
        public void Should_throw_error_when_drone_weight_limit_is_exceeded()
        {
            var model = new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
            {
                Name = "AAA",
                Code = "AA",
                DroneId = 1,
                WeightInGrams = 50 + 1
            };

            _unitOfWork.Setup(x => x.Drones.TotalLoadInGrams(It.IsAny<int>())).Returns(450);

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Drone()
            {
                State = EDroneState.Loading,
                BatteryCapacityInPercentage = Constants.MinPercentageOfBatteryLevelRequired,
                WeightLimitInGrams = 500
            });

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Theory]
        [MemberData(nameof(PassingTestCasesForBatteryValidation))]
        public void Should_not_throw_error_when_drone_battery_level_is_greater_or_equals_to_minimum_required_value(int batteryCapacityInPercentage)
        {
            var model = new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
            {
                Name = "AAA",
                Code = "AA",
                DroneId = 1,
                WeightInGrams = 50
            };

            _unitOfWork.Setup(x => x.Drones.TotalLoadInGrams(It.IsAny<int>())).Returns(450);

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Drone()
            {
                State = EDroneState.Loading,
                BatteryCapacityInPercentage = batteryCapacityInPercentage,
                WeightLimitInGrams = 500
            });

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x);
        }

        public static IEnumerable<object[]> PassingTestCasesForBatteryValidation() =>
            Enumerable.Range(Constants.MinPercentageOfBatteryLevelRequired, 100 - Constants.MinPercentageOfBatteryLevelRequired + 1)
                .Select(i => new object[] { i });
    }
}
