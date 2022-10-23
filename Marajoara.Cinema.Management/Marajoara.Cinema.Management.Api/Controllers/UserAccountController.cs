using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Application.Authorization.Commands;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserAccountController> _logger;
        public UserAccountController(ILogger<UserAccountController> logger,
                                     IMediator mediator,
                                     IHttpContextAccessor context) : base(context)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleResult(await _mediator.Send(new AllUserAccountsQuery()));
        }

        [Authorize(Roles = "Manager,Attendant,Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var callback = ValitadeUserPermission(id);
            return callback.IsSuccess ? HandleResult(await _mediator.Send(new GetUserAccountQuery(id))) : HandleResult(callback);
        }

        [Authorize(Roles = "Manager,Attendant")]
        [HttpGet("{id}/tickets")]
        public async Task<IActionResult> GetTickets(int id)
        {
            return HandleResult(await _mediator.Send(new GetUserAccountTicketsQuery(id)));
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("Customer")]
        public async Task<IActionResult> NewCustomer([FromBody] AddCustomerUserAccountCommand customer)
        {
            return HandleResult(await _mediator.Send(customer));
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("Attendant")]
        public async Task<IActionResult> NewAttendand([FromBody] AddAttendantUserAccountCommand customer)
        {
            return HandleResult(await _mediator.Send(customer));
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("Manager")]
        public async Task<IActionResult> NewManger([FromBody] AddManagerUserAccountCommand customer)
        {
            return HandleResult(await _mediator.Send(customer));
        }

        [Authorize(Roles = "Manager,Attendant,Customer")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserAccountBasicPropertiesCommand updateUserAccountCommand)
        {
            var callback = ValitadeUserPermission(updateUserAccountCommand.UserAccountID);
            return callback.IsSuccess ? HandleResult(await _mediator.Send(updateUserAccountCommand)) : HandleResult(callback);
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _mediator.Send(new DeleteUserAccountCommand(id)));
        }

        [Authorize(Roles = "Manager,Attendant,Customer")]
        [HttpPut("{userAccountID}/Photo")]
        public async Task<IActionResult> UploadPhoto(int userAccountID, IFormFile file)
        {
            var callback = ValitadeUserPermission(userAccountID);
            return callback.IsSuccess ? HandleResult(await _mediator.Send(new UpdateUserAccountPhotoCommand(userAccountID, file.OpenReadStream()))) :
                                        HandleResult(callback);
        }

        [Authorize(Roles = "Manager,Attendant,Customer")]
        [HttpGet("{userAccountID}/Photo")]
        public async Task<IActionResult> GetPhoto(int userAccountID)
        {
            var callback = ValitadeUserPermission(userAccountID);
            return callback.IsSuccess ? HandleResult(await _mediator.Send(new GetUserAccountPhotoQuery(userAccountID))) : HandleResult(callback);
        }

        [Authorize(Roles = "Manager,Attendant,Customer")]
        [HttpDelete("{userAccountID}/Photo")]
        public async Task<IActionResult> DeletePhoto(int userAccountID)
        {
            var callback = ValitadeUserPermission(userAccountID);
            return callback.IsSuccess ? HandleResult(await _mediator.Send(new DeleteUserAccountPhotoCommand(userAccountID))) : HandleResult(callback);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetUserAccountPasswordCommand resetPasswordCommand)
        {
            return HandleResult(await _mediator.Send(resetPasswordCommand));
        }

        //[Authorize(Roles = "Manager,Attendant,Customer")]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserAccountPasswordCommand changePasswordCommand)
        {
            return HandleResult(await _mediator.Send(changePasswordCommand));
        }
    }
}
