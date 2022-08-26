using FluentValidation;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Commands;
using System;

namespace Marajoara.Cinema.Management.Api.Validators.MovieModule
{
    public class UpdateMovieValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieValidator()
        {
            RuleFor(m => m.MovieID)
                 .GreaterThan(0)
                 .WithMessage("Invalid Movie ID.");
            RuleFor(m => m.Title)
                 .NotEmpty()
                 .WithMessage("Movie Title cannot be null or empty.");
            RuleFor(m => m.Description)
                .NotEmpty()
                .WithMessage("Movie Description cannot be null or empty.");
            RuleFor(m => m.MovieDuration)
                .NotEmpty()
                .WithMessage("Movie Duration is required");
            RuleFor(m => m.Duration)
                .Must(BeAValidDuration).WithMessage("Movie Duration is required");

        }

        private bool BeAValidDuration(TimeSpan timeSpan)
        {
            return !timeSpan.Equals(default(TimeSpan));
        }
    }
}
