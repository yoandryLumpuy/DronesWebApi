using DronesWebApi.Commons.Models;
using DronesWebApi.Models.DroneModel.Commands.CreateDroneModelCommand;
using DronesWebApi.Models.DroneModel.Queries.GetDroneModelQuery;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DronesWebApi.Models.DroneModel.Queries.GetAllDroneModelsQuery;

namespace DronesWebApi.Controllers
{
    public class ModelsController: ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateAsync(CreateDroneModelCommand request, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request, cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }

        [HttpGet("{modelId}")]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAsync([FromRoute] int modelId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request: new GetDroneModelQuery() { ModelId = modelId }, cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellation)
        {
            var result = await Mediator.Send(new GetAllDroneModelsQuery(), cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }
    }
}
