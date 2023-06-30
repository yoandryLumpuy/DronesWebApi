using FluentValidation;

namespace DronesWebApi.Models
{
    public class PaginatedListRequestValidator: AbstractValidator<PaginatedListRequest>
    {
        public PaginatedListRequestValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}
