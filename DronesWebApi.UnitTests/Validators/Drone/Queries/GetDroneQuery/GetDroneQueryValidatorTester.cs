using DronesWebApi.Core;
using DronesWebApi.Models.Drone.Queries.GetDroneBatteryLevelQuery;
using DronesWebApi.Models.Drone.Queries.GetDroneQuery;
using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace DronesWebApi.UnitTests.Validators.Drone.Queries.GetDroneQuery
{
    public class GetDroneQueryValidatorTester
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;

        private readonly GetDroneQueryValidator _validator;

        public GetDroneQueryValidatorTester()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _validator = new GetDroneQueryValidator(_unitOfWork.Object);
        }

        [Fact]
        public void Should_not_throw_error_when_drone_exists_in_database()
        {
            var model = new Models.Drone.Queries.GetDroneQuery.GetDroneQuery()
            {
                Id = 1
            };

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Drone());

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_throw_error_when_drone_does_not_exists_in_database()
        {
            var model = new Models.Drone.Queries.GetDroneQuery.GetDroneQuery()
            {
                Id = 1
            };

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns((Core.Domain.Drone)null);

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}
