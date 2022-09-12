using FluentValidation;
using Marajoara.Cinema.Management.Application.Authorization.Commands;

namespace Marajoara.Cinema.Management.Api.Validators.AuthorizationModule
{
    public class AuthenticateValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateValidator()
        {
            RuleFor(cr => cr.Mail)
                .EmailAddress()
                .WithMessage("Customer mail is invalid.");

            RuleFor(cr => cr.Password)
                .NotEmpty()
                .WithMessage("Customer password cannot be null or empty.");

        }
    }
}
