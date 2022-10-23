using FluentValidation;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;
using Microsoft.AspNetCore.Http;

namespace Marajoara.Cinema.Management.Api.Validators.UserAccountModule
{
    public class ResetUserAccountPasswordValidator : AbstractValidator<ResetUserAccountPasswordCommand>
    {
        public ResetUserAccountPasswordValidator()
        {
            RuleFor(ua => ua.UserAccountID)
                .GreaterThan(0)
                .WithMessage("Invalid Account User ID.");
            RuleFor(ua => ua.Mail)
                .EmailAddress()
                .WithMessage("Customer mail is invalid.");
        }
    }
}
