using FluentValidation;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Commands;

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
            RuleFor(m => m.Minutes)
                .GreaterThan(0)
                .WithMessage("Movie Duration is required");
        }
    }
}
