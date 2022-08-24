using FluentValidation;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;

namespace Marajoara.Cinema.Management.Api.Validators.UserAccountModule
{
    public class AddCustomerUserAccountValidator : AbstractValidator<AddCustomerUserAccountCommand>
    {
        public AddCustomerUserAccountValidator()
        {
            RuleFor(cr => cr.Name)
                .NotEmpty()
                .WithMessage("Customer name cannot be null or empty.");

            RuleFor(cr => cr.Mail)
                .EmailAddress()
                .WithMessage("Customer mail is invalid.");

            RuleFor(cr => cr.Password)
                .NotEmpty()
                .WithMessage("Customer mail cannot be null or empty.");
            
        }
    }
}
