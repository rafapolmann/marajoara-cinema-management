using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.CineRoomModule.Commands
{
    public class DeleteCineRoomCommand : IRequest<Result<Exception, bool>>
    {
        public int CineRoomID { get; set; }
        public string Name { get; set; }

        public DeleteCineRoomCommand(string name)
        {
            Name = name;
        }

        public DeleteCineRoomCommand(int id)
        {
            CineRoomID = id;
        }
    }
}
