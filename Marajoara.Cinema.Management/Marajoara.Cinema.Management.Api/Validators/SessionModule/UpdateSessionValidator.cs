using FluentValidation;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Commands;
using System;

namespace Marajoara.Cinema.Management.Api.Validators.SessionModule
{
    public class UpdateSessionValidator : AbstractValidator<UpdateSessionCommand>
    {
        public UpdateSessionValidator()
        {
            RuleFor(s => s.SessionID)
                .GreaterThan(0)
                .WithMessage("Invalid session ID.");
            RuleFor(s => s.CineRoomID)
                .GreaterThan(0)
                .WithMessage("Invalid cine room ID.");
            RuleFor(s => s.MovieID)
                .GreaterThan(0)
                .WithMessage("Invalid movie ID.");
            RuleFor(s => s.Price)
                .GreaterThan(-1)
                .WithMessage("Session Price cannot be negative.");
            RuleFor(cr => cr.SessionDate)
                .Must(BeAValidDate).WithMessage("Session date is required");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
