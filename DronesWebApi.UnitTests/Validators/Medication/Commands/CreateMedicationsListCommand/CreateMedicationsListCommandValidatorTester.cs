using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using DronesWebApi.Core;
using DronesWebApi.Core.Domain.Enums;
using DronesWebApi.Models.Medication.Commands.CreateMedicationsListCommand;
using DronesWebApi.UnitTests.Validators.Medication.Commands.CreateMedicationCommand;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace DronesWebApi.UnitTests.Validators.Medication.Commands.CreateMedicationsListCommand
{
    public class CreateMedicationsListCommandValidatorTester
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly CreateMedicationsListCommandValidator _validator;

        public CreateMedicationsListCommandValidatorTester()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _validator = new CreateMedicationsListCommandValidator(_unitOfWork.Object);
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Should_throw_error_when_at_least_one_drone_exceeds_its_WeightLimitInGrams(
            IEnumerable<Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand> commands,
            int failureInDroneWithId)
        {
            _unitOfWork.Setup(x => x.Drones.TotalLoadInGrams(123)).Returns(20);

            _unitOfWork.Setup(x => x.Drones.TotalLoadInGrams(456)).Returns(50);

            _unitOfWork.Setup(x => x.Drones.Get(123)).Returns(new Core.Domain.Drone()
            {
                Id = 123,
                BatteryCapacityInPercentage = 80,
                SerialNumber = "123",
                WeightLimitInGrams = 400,
                State = EDroneState.Loading
            });

            _unitOfWork.Setup(x => x.Drones.Get(456)).Returns(new Core.Domain.Drone()
            {
                Id = 456,
                BatteryCapacityInPercentage = 90,
                SerialNumber = "456",
                WeightLimitInGrams = 500,
                State = EDroneState.Loading
            });

            var model = new Models.Medication.Commands.CreateMedicationsListCommand.CreateMedicationsListCommand()
            {
                Items = commands
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        public static IEnumerable<object[]> TestCases()
        {
            var result = new List<object[]>();

            var commandsTestCase1 = new List<Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand>()
            {
                new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
                {
                    Code = "CODE_11",
                    Name = "Name11",
                    DroneId = 123,
                    WeightInGrams = 80
                },
                new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
                {
                    Code = "CODE_12",
                    Name = "Name12",
                    DroneId = 123,
                    WeightInGrams = 300 + 1
                },

                new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
                {
                    Code = "CODE_21",
                    Name = "Name21",
                    DroneId = 456,
                    WeightInGrams = 100
                },

                new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
                {
                    Code = "CODE_22",
                    Name = "Name22",
                    DroneId = 456,
                    WeightInGrams = 350
                }
            };

            var failureInDroneIdTestCase1 = 123;

            var commandsTestCase2 = new List<Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand>()
            {
                new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
                {
                    Code = "CODE_11",
                    Name = "Name11",
                    DroneId = 123,
                    WeightInGrams = 80
                },
                new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
                {
                    Code = "CODE_12",
                    Name = "Name12",
                    DroneId = 123,
                    WeightInGrams = 300 
                },

                new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
                {
                    Code = "CODE_21",
                    Name = "Name21",
                    DroneId = 456,
                    WeightInGrams = 100
                },

                new Models.Medication.Commands.CreateMedicationCommand.CreateMedicationCommand()
                {
                    Code = "CODE_22",
                    Name = "Name22",
                    DroneId = 456,
                    WeightInGrams = 350 + 1
                }
            };

            var failureInDroneIdTestCase2 = 456;

            result.Add(new object[]{ commandsTestCase1, failureInDroneIdTestCase1 });
            result.Add(new object[] { commandsTestCase2, failureInDroneIdTestCase2 });

            return result;
        }
    }
}
