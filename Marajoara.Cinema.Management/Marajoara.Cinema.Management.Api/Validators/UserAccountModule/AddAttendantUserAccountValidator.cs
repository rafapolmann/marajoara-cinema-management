using FluentValidation;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;

namespace Marajoara.Cinema.Management.Api.Validators.UserAccountModule
{
    public class AddAttendantUserAccountValidator : AbstractValidator<AddAttendantUserAccountCommand>
    {
        public AddAttendantUserAccountValidator()
        {
            RuleFor(cr => cr.Name)
                .NotEmpty()
                .WithMessage("Customer name cannot be null or empty.");

            RuleFor(cr => cr.Mail)
                .EmailAddress()
                .WithMessage("Customer mail is invalid.");
        }
    }
}
