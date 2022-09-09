using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Commands;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Queries;
using Marajoara.Cinema.Management.Infra.Framework.IoC;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _mediator.Send(new DeleteSessionCommand(id)));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateSessionCommand updateSessionCommand)
        {
            return HandleResult(await _mediator.Send(updateSessionCommand));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _mediator.Send(new GetSessionQuery(id)));
        }

        [HttpGet("{id}/tickets")]
        public async Task<IActionResult> GetTickets(int id)
        {
            return HandleResult(await _mediator.Send(new GetSessionTicketsQuery(id)));
        }
        [HttpGet("{id}/occupiedseats")]
        public async Task<IActionResult> GetOccupiedSeats(int id)
        {
            return HandleResult(await _mediator.Send(new GetSessionOccupiedSeatsQuery(id)));
        }

        [HttpGet("ByDate/{dateTime}")]
        public async Task<IActionResult> GetByDate(DateTime dateTime)
        {
            return HandleResult(await _mediator.Send(new GetSessionsByDateQuery(dateTime)));
        }

        [HttpGet("ByDate/{initialDate}/{finalDate}")]
        public async Task<IActionResult> GetByDate(DateTime initialDate, DateTime finalDate)
        {
            return HandleResult(await _mediator.Send(new GetSessionsByDateRangeQuery(initialDate, finalDate)));
        }

        [HttpGet("ByCineRoom/{cineRoomID}")]
        public async Task<IActionResult> GetByCineRoom(int cineRoomID)
        {
            return HandleResult(await _mediator.Send(new GetSessionsByCineRoomQuery(cineRoomID)));
        }

        [HttpGet("ByMovieTitle/{movieTitle}")]
        public async Task<IActionResult> GetByMovieTitle(string movieTitle)
        {
            return HandleResult(await _mediator.Send(new GetSessionsByMovieTitleQuery(movieTitle)));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResult(await _mediator.Send(new AllSessionsQuery()));
        }
    }
}
