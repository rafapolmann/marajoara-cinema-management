using FluentValidation;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;

namespace Marajoara.Cinema.Management.Api.Validators.UserAccountModule
{
    public class AddManagerUserAccountValidator : AbstractValidator<AddManagerUserAccountCommand>
    {
        public AddManagerUserAccountValidator()
        {
            RuleFor(cr => cr.Name)
                .NotEmpty()
                .WithMessage("Customer name cannot be null or empty.");

            RuleFor(cr => cr.Mail)
                .EmailAddress()
                .WithMessage("Customer mail is invalid.");

            RuleFor(cr => cr.Password)
                .NotEmpty()
                .WithMessage("Customer password cannot be null or empty.");

        }
    }
}
