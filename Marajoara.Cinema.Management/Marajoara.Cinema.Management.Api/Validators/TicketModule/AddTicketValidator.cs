using FluentValidation;
using Marajoara.Cinema.Management.Api.Helpers;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Commands;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Marajoara.Cinema.Management.Api.Validators.TicketModule
{
    public class AddTicketValidator : AbstractValidator<AddTicketCommand>
    {
        public AddTicketValidator(IHttpContextAccessor context)
        {

            RuleFor(cr => cr.SeatNumber)
                .GreaterThan(0)
                .WithMessage("Invalid Seat number.") ;
            RuleFor(cr => cr.UserAccountID)
                .Equal(ClaimsHelper.GetUserAccountID(context))
                .WithMessage("Invalid UserAccountID.");
            RuleFor(cr => cr.SessionID)
                .GreaterThan(0)
                .WithMessage("Invalid SessionID.");

        }
    }
}
