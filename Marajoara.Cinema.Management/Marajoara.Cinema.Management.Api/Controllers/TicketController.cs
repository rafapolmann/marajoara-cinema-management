using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Commands;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    [ApiController]
    [Authorize]

    [Route("api/[controller]")]
    public class TicketController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TicketController> _logger;
        public TicketController(ILogger<TicketController> logger,
                                IMediator mediator,
                                IHttpContextAccessor context) : base(context)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize(Roles = "Manager,Attendant")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResult(await _mediator.Send(new AllTicketsQuery()));
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByUser(int id)
        {
            return HandleResult(await _mediator.Send(new GetTicketsByUserAccountQuery(id)));
        }

        [Authorize(Roles = "Manager,Attendant")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return HandleResult(await _mediator.Send(new GetTicketByIDQuery(id)));
        }

        [Authorize(Roles = "Manager,Attendant")]
        [HttpGet("Guid/{code}")]
        public async Task<IActionResult> GetByCode(Guid code)
        {
            return HandleResult(await _mediator.Send(new GetTicketByCodeQuery(code)));
        }

        [Authorize(Roles = "Manager,Attendant")]
        [HttpPost("Used")]
        public async Task<IActionResult> SetAsUsed([FromBody] SetTicketAsUsedCommand setTicketAsUsedCmd)
        {
            return HandleResult(await _mediator.Send(setTicketAsUsedCmd));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddTicketCommand addTicketCommand)
        {
            return HandleResult(await _mediator.Send(addTicketCommand));
        }

        //[Authorize(Roles ="Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _mediator.Send(new DeleteTicketCommand(id)));
        }
    }
}
