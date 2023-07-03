using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DronesWebApi.Core;
using MediatR;

namespace DronesWebApi.Models.Medication.Queries.GetMedicationQuery
{
    public class GetMedicationQuery: IRequest<MedicationDto>
    {
        public string Code { get; set; }
    }

    public class GetMedicationQueryHandler : IRequestHandler<GetMedicationQuery, MedicationDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetMedicationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<MedicationDto> Handle(GetMedicationQuery request, CancellationToken cancellationToken)
        {
            var medication = _unitOfWork.Medications.GetWithDronesReferences(request.Code);

            return Task.FromResult(_mapper.Map<MedicationDto>(medication));
        }
    }
}
