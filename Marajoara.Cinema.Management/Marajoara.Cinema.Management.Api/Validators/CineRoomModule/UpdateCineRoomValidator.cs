using FluentValidation;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Commands;

namespace Marajoara.Cinema.Management.Api.Validators.CineRoomModule
{
    public class UpdateCineRoomValidator:AbstractValidator<UpdateCineRoomCommand>
    {
        public UpdateCineRoomValidator()
        {
            RuleFor(cr => cr.CineRoomID)
                .GreaterThan(0)
                .WithMessage("Invalid Cine ID.");
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
