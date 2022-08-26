using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Commands;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Queries;
using Marajoara.Cinema.Management.Infra.Framework.IoC;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SessionController> _logger;

        public SessionController(ILogger<SessionController> logger)
        {
            _mediator = IoC.GetInstance().Get<IMediator>();
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddSessionCommand addSessionCommand)
        {
            return HandleResult(await _mediator.Send(addSessionCommand));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _mediator.Send(new GetSessionQuery(id)));
        }

        [HttpGet("ByCineRoom/{cineRoomID}")]
        public async Task<IActionResult> GetByCineRoom(int cineRoomID)
        {
            return HandleResult(await _mediator.Send(new GetSessionsByCineRoomQuery(cineRoomID)));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResult(await _mediator.Send(new AllSessionsQuery()));
        }
    }
}
