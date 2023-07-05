using System.Collections.Generic;
using DronesWebApi.Core;
using DronesWebApi.Core.Domain;
using DronesWebApi.Core.Domain.Enums;
using DronesWebApi.Models.Medication.Commands.LoadMedicationListCommand;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace DronesWebApi.UnitTests.Validators.Medication.Commands.LoadMedicationListCommand
{
    public class LoadMedicationListCommandValidatorTester
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly LoadMedicationListCommandValidator _validator;

        public LoadMedicationListCommandValidatorTester()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _validator = new LoadMedicationListCommandValidator(_unitOfWork.Object);
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Should_throw_error_when_at_least_one_drone_exceeds_its_WeightLimitInGrams(
            IEnumerable<Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand> commands,
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

            var medication11 = new Core.Domain.Medication()
            {
                Code = "CODE_11",
                WeightInGrams = 100
            };

            var medication12 = new Core.Domain.Medication()
            {
                Code = "CODE_12",
                WeightInGrams = 280
            };

            var medication21 = new Core.Domain.Medication()
            {
                Code = "CODE_21",
                WeightInGrams = 100
            };

            var medication22 = new Core.Domain.Medication()
            {
                Code = "CODE_22",
                WeightInGrams = 350 + 1
            };

            _unitOfWork.Setup(x => x.Medications.Get(medication11.Code)).Returns(medication11);

            _unitOfWork.Setup(x => x.Medications.GetWithDronesReferences(medication11.Code)).Returns(medication11);

            _unitOfWork.Setup(x => x.Medications.Get(medication12.Code)).Returns(medication12);

            _unitOfWork.Setup(x => x.Medications.GetWithDronesReferences(medication12.Code)).Returns(medication12);

            _unitOfWork.Setup(x => x.Medications.Get(medication21.Code)).Returns(medication21);

            _unitOfWork.Setup(x => x.Medications.GetWithDronesReferences(medication21.Code)).Returns(medication21);

            _unitOfWork.Setup(x => x.Medications.Get(medication22.Code)).Returns(medication22);

            _unitOfWork.Setup(x => x.Medications.GetWithDronesReferences(medication22.Code)).Returns(medication22);

            var model = new Models.Medication.Commands.LoadMedicationListCommand.LoadMedicationListCommand()
            {
                Items = commands
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        public static IEnumerable<object[]> TestCases()
        {
            var result = new List<object[]>();

            var commandsTestCase1 = new List<Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand>()
            {
                new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
                {
                    Code = "CODE_11",
                    DroneId = 123,
                },
                new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
                {
                    Code = "CODE_12",
                    DroneId = 123
                },

                new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
                {
                    Code = "CODE_21",
                    DroneId = 456
                },

                new Models.Medication.Commands.LoadMedicationCommand.LoadMedicationCommand()
                {
                    Code = "CODE_22",
                    DroneId = 456
                }
            };

            var failureInDroneIdTestCase1 = 123;

            result.Add(new object[] { commandsTestCase1, failureInDroneIdTestCase1 });

            return result;
        }

    }
}
