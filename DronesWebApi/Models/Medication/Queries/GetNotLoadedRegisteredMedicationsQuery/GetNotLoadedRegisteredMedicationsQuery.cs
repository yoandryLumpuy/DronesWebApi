using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DronesWebApi.Core;
using DronesWebApi.Models.Medication.Queries.GetMedicationQuery;
using MediatR;

namespace DronesWebApi.Models.Medication.Queries.GetNotLoadedRegisteredMedicationsQuery
{
    public class GetNotLoadedRegisteredMedicationsQuery: IRequest<IEnumerable<MedicationDto>>
    { }

    public class GetNotLoadedRegisteredMedicationsQueryHandler : IRequestHandler<GetNotLoadedRegisteredMedicationsQuery, IEnumerable<MedicationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNotLoadedRegisteredMedicationsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<MedicationDto>> Handle(GetNotLoadedRegisteredMedicationsQuery request, CancellationToken cancellationToken)
        {
            var entities = _unitOfWork.Medications.GetNotLoaded();

            return Task.FromResult(_mapper.Map<IEnumerable<MedicationDto>>(entities));
        }
    }
}
