using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    [ApiController]
    //[Authorize(Roles = "Manager")]
    [Route("api/[controller]")]
    public class UserAccountController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CineRoomController> _logger;
        public UserAccountController(ILogger<CineRoomController> logger, IMediator mediator)
        {
            _mediator = mediator;
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

        [HttpGet("{id}/tickets")]
        public async Task<IActionResult> GetTickets(int id)
        {
            return HandleResult(await _mediator.Send(new GetUserAccountTicketsQuery(id)));
        }

        [HttpPost("Customer")]
        public async Task<IActionResult> NewCustomer([FromBody] AddCustomerUserAccountCommand customer)
        {
            return HandleResult(await _mediator.Send(customer));
        }

        [HttpPost("Attendant")]
        public async Task<IActionResult> NewAttendand([FromBody] AddAttendantUserAccountCommand customer)
        {
            return HandleResult(await _mediator.Send(customer));
        }

        [HttpPost("Manager")]
        public async Task<IActionResult> NewManger([FromBody] AddManagerUserAccountCommand customer)
        {
            return HandleResult(await _mediator.Send(customer));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserAccountBasicPropertiesCommand updateUserAccountCommand)
        {
            return HandleResult(await _mediator.Send(updateUserAccountCommand));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _mediator.Send(new DeleteUserAccountCommand(id)));
        }


        [HttpPut("Photo")]
        public async Task<IActionResult> UploadPoster(int userAccountID, IFormFile file)
        {
            return HandleResult(await _mediator.Send(new UpdateUserAccountPhotoCommand(userAccountID, file.OpenReadStream())));
        }

        [HttpGet("Photo")]
        public async Task<IActionResult> GetPoster(int userAccountID)
        {
            return HandleResult(await _mediator.Send(new GetUserAccountPhotoQuery(userAccountID)));
        }

        [HttpDelete("Photo")]
        public async Task<IActionResult> DeletePoster(int userAccountID)
        {
            return HandleResult(await _mediator.Send(new DeleteUserAccountPhotoCommand(userAccountID)));
        }

    }
}
