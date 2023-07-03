using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DronesWebApi.Core;
using DronesWebApi.Models.Medication.Queries.GetMedicationQuery;
using MediatR;

namespace DronesWebApi.Models.Medication.Queries.GetLoadedMedicationsQuery
{
    public class GetLoadedMedicationsQuery: IRequest<IEnumerable<MedicationDto>>
    {
        public int DroneId { get; set; }
    }

    public class GetLoadedMedicationsQueryHandler : IRequestHandler<GetLoadedMedicationsQuery, IEnumerable<MedicationDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetLoadedMedicationsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<IEnumerable<MedicationDto>> Handle(GetLoadedMedicationsQuery request, CancellationToken cancellationToken)
        {
            var drone = _unitOfWork.Drones.GetWithLoadedMedications(request.DroneId);

            var resultAsCollection = _mapper.Map<ICollection<MedicationDto>>(drone.LoadedMedications);

            return Task.FromResult((IEnumerable<MedicationDto>)resultAsCollection);
        }
    }
}
