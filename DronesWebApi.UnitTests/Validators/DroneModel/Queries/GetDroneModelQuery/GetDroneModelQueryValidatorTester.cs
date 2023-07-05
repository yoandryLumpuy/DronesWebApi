using DronesWebApi.Core;
using FluentValidation;
using Moq;
using System.ComponentModel.DataAnnotations;
using DronesWebApi.Models.DroneModel.Queries.GetDroneModelQuery;
using FluentValidation.TestHelper;
using Xunit;

namespace DronesWebApi.UnitTests.Validators.DroneModel.Queries.GetDroneModelQuery
{
    public class GetDroneModelQueryValidatorTester
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;

        private readonly GetDroneModelQueryValidator _validator;

        public GetDroneModelQueryValidatorTester()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _validator = new GetDroneModelQueryValidator(_unitOfWork.Object);
        }

        [Fact]
        public void Should_not_throw_error_when_drone_exists_in_database()
        {
            var model = new Models.DroneModel.Queries.GetDroneModelQuery.GetDroneModelQuery()
            {
                ModelId = 1
            };

            _unitOfWork.Setup(x => x.DroneModels.Get(It.IsAny<object[]>())).Returns(new Core.Domain.DroneModel());

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.ModelId);
        }

        [Fact]
        public void Should_throw_error_when_drone_does_not_exists_in_database()
        {
            var model = new Models.DroneModel.Queries.GetDroneModelQuery.GetDroneModelQuery()
            {
                ModelId = 1
            };

            _unitOfWork.Setup(x => x.DroneModels.Get(It.IsAny<object[]>())).Returns((Core.Domain.DroneModel)null);

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.ModelId);
        }
    }
}
