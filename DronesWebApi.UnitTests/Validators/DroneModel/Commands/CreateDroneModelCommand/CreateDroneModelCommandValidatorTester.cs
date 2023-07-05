using System;
using System.Linq;
using DronesWebApi.Models.DroneModel.Commands.CreateDroneModelCommand;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace DronesWebApi.UnitTests.Validators.DroneModel.Commands.CreateDroneModelCommand
{
    public class CreateDroneModelCommandValidatorTester
    {
        private readonly CreateDroneModelCommandValidator _validator;

        public CreateDroneModelCommandValidatorTester()
        {
            _validator = new CreateDroneModelCommandValidator();
        }

        [Fact]
        public void Should_throw_error_when_name_is_null()
        {
            var model = new Models.DroneModel.Commands.CreateDroneModelCommand.CreateDroneModelCommand()
            {
                Name = null
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_throw_error_when_name_is_empty()
        {
            var model = new Models.DroneModel.Commands.CreateDroneModelCommand.CreateDroneModelCommand()
            {
                Name = " "
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_not_throw_error_when_name_is_not_null_or_empty()
        {
            var model = new Models.DroneModel.Commands.CreateDroneModelCommand.CreateDroneModelCommand()
            {
                Name = Guid.NewGuid().ToString()
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_throw_error_when_name_length_is_greater_than_100()
        {
            var model = new Models.DroneModel.Commands.CreateDroneModelCommand.CreateDroneModelCommand()
            {
                Name = string.Join(string.Empty, Enumerable.Repeat(1,101).Select(i => i.ToString()))
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_not_throw_error_when_name_length_is_less_than_or_equals_to_100()
        {
            var model = new Models.DroneModel.Commands.CreateDroneModelCommand.CreateDroneModelCommand()
            {
                Name = string.Join(string.Empty, Enumerable.Repeat(1, 100).Select(i => i.ToString()))
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_throw_error_when_CruiserWeightInGrams_is_not_greater_than_zero()
        {
            var model = new Models.DroneModel.Commands.CreateDroneModelCommand.CreateDroneModelCommand()
            {
                CruiserweightInGrams = 0
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.CruiserweightInGrams);
        }

        [Fact]
        public void Should_not_throw_error_when_CruiserWeightInGrams_is_greater_than_zero()
        {
            var model = new Models.DroneModel.Commands.CreateDroneModelCommand.CreateDroneModelCommand()
            {
                CruiserweightInGrams = new Random().Next(1, int.MaxValue)
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.CruiserweightInGrams);
        }

        [Fact]
        public void Should_throw_error_when_HeavyweightInGrams_is_not_greater_than_zero()
        {
            var model = new Models.DroneModel.Commands.CreateDroneModelCommand.CreateDroneModelCommand()
            {
                HeavyweightInGrams = 0
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.HeavyweightInGrams);
        }

        [Fact]
        public void Should_not_throw_error_when_HeavyweightInGrams_is_greater_than_zero()
        {
            var model = new Models.DroneModel.Commands.CreateDroneModelCommand.CreateDroneModelCommand()
            {
                HeavyweightInGrams = new Random().Next(1, int.MaxValue)
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.HeavyweightInGrams);
        }

        [Fact]
        public void Should_throw_error_when_LightweightInGrams_is_not_greater_than_zero()
        {
            var model = new Models.DroneModel.Commands.CreateDroneModelCommand.CreateDroneModelCommand()
            {
                LightweightInGrams = 0
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.LightweightInGrams);
        }

        [Fact]
        public void Should_not_throw_error_when_LightweightInGrams_is_greater_than_zero()
        {
            var model = new Models.DroneModel.Commands.CreateDroneModelCommand.CreateDroneModelCommand()
            {
                LightweightInGrams = new Random().Next(1, int.MaxValue)
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.LightweightInGrams);
        }

        [Fact]
        public void Should_throw_error_when_MiddleweightInGrams_is_not_greater_than_zero()
        {
            var model = new Models.DroneModel.Commands.CreateDroneModelCommand.CreateDroneModelCommand()
            {
                MiddleweightInGrams = 0
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.MiddleweightInGrams);
        }

        [Fact]
        public void Should_not_throw_error_when_MiddleweightInGrams_is_greater_than_zero()
        {
            var model = new Models.DroneModel.Commands.CreateDroneModelCommand.CreateDroneModelCommand()
            {
                MiddleweightInGrams = new Random().Next(1, int.MaxValue)
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.MiddleweightInGrams);
        }
    }
}
