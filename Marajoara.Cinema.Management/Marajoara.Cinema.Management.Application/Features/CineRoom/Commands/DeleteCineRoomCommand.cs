using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.CineRoom.Commands
{
    public class DeleteCineRoomCommand : IRequest<Result<Exception, bool>>
    {
        public string Name { get; set; }

        public DeleteCineRoomCommand(string name)
        {
            Name = name;
        }
    }
}
