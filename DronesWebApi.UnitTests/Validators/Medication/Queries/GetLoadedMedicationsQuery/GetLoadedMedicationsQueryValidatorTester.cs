using DronesWebApi.Core;
using DronesWebApi.Models.Medication.Queries.GetLoadedMedicationsQuery;
using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace DronesWebApi.UnitTests.Validators.Medication.Queries.GetLoadedMedicationsQuery
{
    public class GetLoadedMedicationsQueryValidatorTester
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly GetLoadedMedicationsQueryValidator _validator;

        public GetLoadedMedicationsQueryValidatorTester()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _validator = new GetLoadedMedicationsQueryValidator(_unitOfWork.Object);
        }

        [Fact]
        public void Should_throw_error_when_drone_does_not_exists()
        {
            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns((Core.Domain.Drone)null);

            var model = new Models.Medication.Queries.GetLoadedMedicationsQuery.GetLoadedMedicationsQuery()
            {
                DroneId = 1
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.DroneId);
        }

        [Fact]
        public void Should_not_throw_error_when_drone_exists()
        {
            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Drone());

            var model = new Models.Medication.Queries.GetLoadedMedicationsQuery.GetLoadedMedicationsQuery()
            {
                DroneId = 1
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.DroneId);
        }
    }
}
