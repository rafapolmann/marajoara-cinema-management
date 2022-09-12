using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.CineRoomModule.Commands
{
    public class AddCineRoomCommand : IRequest<Result<Exception, int>>
    {
        public string Name { get; set; }
        public int SeatsRow { get; set; }
        public int SeatsColumn { get; set; }
    }
}
