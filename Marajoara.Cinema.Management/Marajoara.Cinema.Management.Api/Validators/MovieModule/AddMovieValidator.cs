using FluentValidation;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Commands;

namespace Marajoara.Cinema.Management.Api.Validators.MovieModule
{
    public class AddMovieValidator : AbstractValidator<AddMovieCommand>
    {
        public AddMovieValidator()
        {
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