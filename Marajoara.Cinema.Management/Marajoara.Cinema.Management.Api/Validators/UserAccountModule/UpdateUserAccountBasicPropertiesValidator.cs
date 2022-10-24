using FluentValidation;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;
using Marajoara.Cinema.Management.Domain.UserAccountModule;

namespace Marajoara.Cinema.Management.Api.Validators.UserAccountModule
{
    public class UpdateUserAccountBasicPropertiesValidator : AbstractValidator<UpdateUserAccountBasicPropertiesCommand>
    {
        public UpdateUserAccountBasicPropertiesValidator()
        {
            RuleFor(ua => ua.UserAccountID)
                .GreaterThan(0)
                .WithMessage("Invalid Account User ID.");

            RuleFor(ua => ua.Name)
                .NotEmpty()
                .WithMessage("Customer name cannot be null or empty.");

            RuleFor(ua => ua.Level)
                .Must(HeightValidation)
                .WithMessage("User Account Level is invalid.");
        }

        private bool HeightValidation(AccessLevel level)
        {
            return level.GetHashCode() >= 1 && level.GetHashCode() <= 3;
        }
    }

}
