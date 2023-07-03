using DronesWebApi.Models.Drone.Commands.CreateDroneCommand;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DronesWebApi.Commons.Models;
using DronesWebApi.Models.Drone.Queries.GetAllAvailableDronesForLoadingQuery;
using DronesWebApi.Models.Drone.Queries.GetDroneQuery;
using DronesWebApi.Models.Drone.Queries.GetDronesWithPaginationQuery;
using DronesWebApi.Models.DroneModel.Queries.GetAllDroneModelsQuery;

namespace DronesWebApi.Controllers
{
    public class DronesController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(Result<>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateAsync(CreateDroneCommand request, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request, cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }

        [HttpGet(template: "{droneId}")]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAsync([FromRoute] int droneId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request: new GetDroneQuery(){ Id = droneId }, cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }

        [HttpGet("paginated")]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetWithPaginationAsync([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellation)
        {
            var result = await Mediator.Send(
                request: new GetDronesWithPaginationQuery(){ PageNumber = pageNumber, PageSize = pageSize }, cancellation)
                .ConfigureAwait(false);

            return Ok(Result.Success(result));
        }

        [HttpGet("available")]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllAvailableForLoadingAsync(CancellationToken cancellation)
        {
            var result = await Mediator.Send(request: new GetAllAvailableDronesForLoadingQuery(), cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }
    }
}
