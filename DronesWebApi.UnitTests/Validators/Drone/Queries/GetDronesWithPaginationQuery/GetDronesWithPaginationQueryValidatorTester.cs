using System;
using DronesWebApi.Models.Drone.Queries.GetDronesWithPaginationQuery;
using FluentValidation.TestHelper;
using Xunit;

namespace DronesWebApi.UnitTests.Validators.Drone.Queries.GetDronesWithPaginationQuery
{
    public class GetDronesWithPaginationQueryValidatorTester
    {
        private readonly GetDronesWithPaginationQueryValidator _validator;

        public GetDronesWithPaginationQueryValidatorTester()
        {
            _validator = new GetDronesWithPaginationQueryValidator();
        }

        [Fact]
        public void Should_throw_error_when_page_number_is_less_than_1()
        {
            var model = new Models.Drone.Queries.GetDronesWithPaginationQuery.GetDronesWithPaginationQuery()
            {
                PageNumber = 0
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.PageNumber);
        }

        [Fact]
        public void Should_not_throw_error_when_page_number_is_equals_or_greater_than_1()
        {
            var model = new Models.Drone.Queries.GetDronesWithPaginationQuery.GetDronesWithPaginationQuery()
            {
                PageNumber = new Random().Next(1, int.MaxValue)
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.PageNumber);
        }

        [Fact]
        public void Should_throw_error_when_page_size_is_less_than_1()
        {
            var model = new Models.Drone.Queries.GetDronesWithPaginationQuery.GetDronesWithPaginationQuery()
            {
                PageSize = 0
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.PageSize);
        }

        [Fact]
        public void Should_not_throw_error_when_page_size_is_equals_or_greater_than_1()
        {
            var model = new Models.Drone.Queries.GetDronesWithPaginationQuery.GetDronesWithPaginationQuery()
            {
                PageSize = new Random().Next(1, int.MaxValue)
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.PageSize);
        }
    }
}
