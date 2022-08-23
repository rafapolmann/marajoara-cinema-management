using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Infra.Framework.IoC;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Queries;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CineRoomController> _logger;
        public UserAccountController(ILogger<CineRoomController> logger)
        {
            _mediator = IoC.GetInstance().Get<IMediator>();
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResult(await _mediator.Send(new AllUserAccountsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return HandleResult(await _mediator.Send(new GetUserAccountQuery(id)));
        }

    }
}
