using Marajoara.Cinema.Management.Api.Base;
using Marajoara.Cinema.Management.Api.Helpers;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Models;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Queries;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserAccountController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CineRoomController> _logger;
        private readonly IHttpContextAccessor _context;
        public UserAccountController(ILogger<CineRoomController> logger, IMediator mediator, IHttpContextAccessor context)
        {
            _context = context;
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
            if (callback.IsSuccess)
                return HandleResult(await _mediator.Send(new GetUserAccountQuery(id)));
            else
                return HandleResult(callback, System.Net.HttpStatusCode.Forbidden);
        }

        private Result<Exception, bool> ValitadeUserPermission(int userAccountID)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                if (ClaimsHelper.GetUserAccountID(_context) == userAccountID ||
                    ClaimsHelper.GetRole(_context) == Domain.UserAccountModule.AccessLevel.Manager)
                    return true;
                else
                    throw new Exception("User does not have permission to access those datas.");
            });
            return result;
        }

        [Authorize(Roles = "Manager,Attendant,Customer")]
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
            return HandleResult(await _mediator.Send(updateUserAccountCommand));
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await _mediator.Send(new DeleteUserAccountCommand(id)));
        }

        [Authorize(Roles = "Manager,Attendant,Customer")]
        [HttpPut("{userAccountID}/Photo")]
        public async Task<IActionResult> UploadPoster(int userAccountID, IFormFile file)
        {
            return HandleResult(await _mediator.Send(new UpdateUserAccountPhotoCommand(userAccountID, file.OpenReadStream())));
        }

        [Authorize(Roles = "Manager,Attendant,Customer")]
        [HttpGet("{userAccountID}/Photo")]
        public async Task<IActionResult> GetPoster(int userAccountID)
        {
            return HandleResult(await _mediator.Send(new GetUserAccountPhotoQuery(userAccountID)));
        }

        [Authorize(Roles = "Manager,Attendant,Customer")]
        [HttpDelete("{userAccountID}/Photo")]
        public async Task<IActionResult> DeletePoster(int userAccountID)
        {
            return HandleResult(await _mediator.Send(new DeleteUserAccountPhotoCommand(userAccountID)));
        }


    }
}
