using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Commands
{
    public class DeleteSessionCommand : IRequest<Result<Exception, bool>>
    {
        public int SessionID { get; set; }
        public DeleteSessionCommand(int id)
        {
            SessionID = id;
        }
    }
}
