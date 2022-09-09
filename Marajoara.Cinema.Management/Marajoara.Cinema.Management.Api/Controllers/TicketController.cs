using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Commands;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Queries;
using Marajoara.Cinema.Management.Infra.Framework.IoC;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    [ApiController]
    [Authorize]
    
    [Route("api/[controller]")]
    public class TicketController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CineRoomController> _logger;
        public TicketController(ILogger<CineRoomController> logger)
        {

            _mediator = IoC.GetInstance().Get<IMediator>();
            _logger = logger;
        }
       
        [Authorize(Roles = "Manager,Attendant")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResult(await _mediator.Send(new AllTicketsQuery()));
        }
       
        [Authorize(Roles = "Manager,Attendant")]
        [AllowAnonymous]
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


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddTicketCommand addTicketCommand)
        {
            return HandleResult(await _mediator.Send(addTicketCommand));
        }

        [Authorize(Roles ="Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _mediator.Send(new DeleteTicketCommand(id)));
        }
    }
}
