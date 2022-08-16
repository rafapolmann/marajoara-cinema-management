using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Application.Features.CineRoom.Queries;
using Marajoara.Cinema.Management.Infra.Framework.IoC;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CineRoomController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CineRoomController> _logger;

        public CineRoomController(ILogger<CineRoomController> logger)
        {
            _mediator = IoC.GetInstance().Get<IMediator>();
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResult(await _mediator.Send(new AllCineRoomsQuery()));
        }
    }
}
