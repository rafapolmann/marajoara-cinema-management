using FluentValidation;
using Marajoara.Cinema.Management.Api.Helpers;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;
using Microsoft.AspNetCore.Http;

namespace Marajoara.Cinema.Management.Api.Validators.UserAccountModule
{
    public class ChangeUserAccountPasswordValidator : AbstractValidator<ChangeUserAccountPasswordCommand>
    {
        public ChangeUserAccountPasswordValidator(IHttpContextAccessor context)
        {
            RuleFor(cr => cr.UserAccountID)
                .Equal(ClaimsHelper.GetUserAccountID(context))
                .WithMessage("User does not have permission to access those datas.");
            RuleFor(ua => ua.UserAccountID)
                .GreaterThan(0)
                .WithMessage("Invalid UserAccount ID.");
            RuleFor(ua => ua.Mail)
                .EmailAddress()
                .WithMessage("Customer mail is invalid.");
            RuleFor(ua => ua.Password)
               .NotEmpty()
               .WithMessage("Invalid password.");
            RuleFor(ua => ua.NewPassword)
               .MinimumLength(6)
               .WithMessage("New password is invalid.");
        }
    }
}
