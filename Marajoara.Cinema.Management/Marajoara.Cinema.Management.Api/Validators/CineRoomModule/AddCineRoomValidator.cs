using FluentValidation;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Commands;

namespace Marajoara.Cinema.Management.Api.Validators.CineRoomModule
{
    public class AddCineRoomValidator : AbstractValidator<AddCineRoomCommand>
    {
        public AddCineRoomValidator()
        {
            RuleFor(cr => cr.Name)
                .NotEmpty()
                .WithMessage("Cine room name cannot be null or empty.");
            RuleFor(cr => cr.SeatsColumn)
                .GreaterThan(0)
                .WithMessage("Cine room seat columns should be grater than 0.");
            RuleFor(cr => cr.SeatsRow)
                .GreaterThan(0)
                .WithMessage("Cine room seat rows should be grater than 0.");
        }
    }
}
