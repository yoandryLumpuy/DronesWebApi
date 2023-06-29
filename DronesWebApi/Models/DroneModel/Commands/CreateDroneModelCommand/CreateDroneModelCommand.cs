using System.Threading;
using System.Threading.Tasks;
using DronesWebApi.Core;
using DronesWebApi.Core.Domain.Enums;
using MediatR;

namespace DronesWebApi.Models.DroneModel.Commands.CreateDroneModelCommand
{
    public class CreateDroneModelCommand : IRequest<CreateDroneModelCommandResponse>
    {
        public string Name { get; set; }

        public int LightweightInGrams { get; set; }

        public int MiddleweightInGrams { get; set; }

        public int CruiserweightInGrams { get; set; }

        public int HeavyweightInGrams { get; set; }
    }

    public class CreateDroneModelCommandHandler : IRequestHandler<CreateDroneModelCommand, CreateDroneModelCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDroneModelCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<CreateDroneModelCommandResponse> Handle(CreateDroneModelCommand request, CancellationToken cancellationToken)
        {
            var entity = new Core.Domain.DroneModel()
            {
                Name = request.Name,
                CruiserweightInGrams = request.CruiserweightInGrams,
                HeavyweightInGrams = request.HeavyweightInGrams,
                LightweightInGrams = request.LightweightInGrams,
                MiddleweightInGrams = request.MiddleweightInGrams
            };

            _unitOfWork.DroneModels.Add(entity);

            _unitOfWork.Complete();

            return Task.FromResult(new CreateDroneModelCommandResponse(){ Id = entity.Id });
        }
    }
}
