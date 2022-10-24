using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Commands;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    [ApiController]
    [Authorize]
    //[Authorize(Roles = "Manager")]
    [Route("api/[controller]")]
    public class CineRoomController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CineRoomController> _logger;

        public CineRoomController(ILogger<CineRoomController> logger,
                                  IMediator mediator,
                                  IHttpContextAccessor context) : base(context)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _mediator.Send(new GetCineRoomQuery(id)));
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            return HandleResult(await _mediator.Send(new GetCineRoomQuery(name)));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResult(await _mediator.Send(new AllCineRoomsQuery()));
        }
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCineRoomCommand addCineRoomCommand)
        {
            return HandleResult(await _mediator.Send(addCineRoomCommand));
        }
        [Authorize(Roles = "Manager")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCineRoomCommand updateCineRoomCommand)
        {
            return HandleResult(await _mediator.Send(updateCineRoomCommand));
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("ByName/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            return HandleResult(await _mediator.Send(new DeleteCineRoomCommand(name)));
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _mediator.Send(new DeleteCineRoomCommand(id)));
        }
    }
}
