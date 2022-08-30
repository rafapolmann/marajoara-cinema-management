using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Application.Authorization.Commands;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Models;
using Marajoara.Cinema.Management.Infra.Framework.IoC;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    public class AuthorizationController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CineRoomController> _logger;
        public AuthorizationController(ILogger<CineRoomController> logger) 
        {
            _mediator = IoC.GetInstance().Get<IMediator>();
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]        
        public  async Task<IActionResult> Authenticate([FromBody] AuthenticateCommand authCmd)
        {
            return HandleResult(await _mediator.Send(authCmd)); ;
        }
    }
}
