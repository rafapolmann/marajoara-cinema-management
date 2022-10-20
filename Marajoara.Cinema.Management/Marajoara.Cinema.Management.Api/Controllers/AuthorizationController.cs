using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Application.Authorization.Commands;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthorizationController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthorizationController> _logger;
        public AuthorizationController(ILogger<AuthorizationController> logger, IMediator mediator) 
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]        
        public  async Task<IActionResult> Authenticate([FromBody] AuthenticateCommand authCmd)
        {
            return HandleResult(await _mediator.Send(authCmd)); ;
        }

        [HttpPost("register")]
        public async Task<IActionResult> NewCustomer([FromBody] AddCustomerUserAccountCommand customer)
        {
            return HandleResult(await _mediator.Send(customer));
        }

    }
}
