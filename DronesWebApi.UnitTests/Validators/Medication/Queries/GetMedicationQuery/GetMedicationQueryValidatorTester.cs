using DronesWebApi.Core;
using FluentValidation;

namespace DronesWebApi.UnitTests.Validators.Medication.Queries.GetMedicationQuery
{
    public class GetMedicationQueryValidatorTester
    {
        public GetMedicationQueryValidatorTester(IUnitOfWork unitOfWork)
        {
            //RuleFor(medicationQuery => medicationQuery.Code)
            //    .Must(code =>
            //    {
            //        var medication = unitOfWork.Medications.Get(code);

            //        return medication != null;

            //    }).WithMessage(medicationQuery => $"Medication with code: '{medicationQuery.Code}' was not found");
        }
    }
}
