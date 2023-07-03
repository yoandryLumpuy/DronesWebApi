using DronesWebApi.Core;
using FluentValidation;

namespace DronesWebApi.Models.Medication.Queries.GetMedicationQuery
{
    public class GetMedicationQueryValidator: AbstractValidator<GetMedicationQuery>
    {
        public GetMedicationQueryValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(medicationQuery => medicationQuery.Code)
                .Must(code =>
                {
                    var medication = unitOfWork.Medications.Get(code);

                    return medication != null;

                }).WithMessage(medicationQuery => $"Medication with code: '{medicationQuery.Code}' was not found");
        }
    }
}
