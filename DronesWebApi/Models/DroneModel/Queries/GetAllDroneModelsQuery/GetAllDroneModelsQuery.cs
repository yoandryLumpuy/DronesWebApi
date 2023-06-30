using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DronesWebApi.Core;
using DronesWebApi.Models.DroneModel.Queries.GetDroneModelQuery;
using MediatR;

namespace DronesWebApi.Models.DroneModel.Queries.GetAllDroneModelsQuery
{
    public class GetAllDroneModelsQuery: IRequest<IEnumerable<DroneModelDto>>
    { }

    public class GetAllDroneModelsQueryHandler: IRequestHandler<GetAllDroneModelsQuery, IEnumerable<DroneModelDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllDroneModelsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<DroneModelDto>> Handle(GetAllDroneModelsQuery request, CancellationToken cancellationToken)
        {
            var entities = _unitOfWork.DroneModels.GetAll();

            return Task.FromResult(_mapper.Map<IEnumerable<DroneModelDto>>(entities));
        }
    }
}
