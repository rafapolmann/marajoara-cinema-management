using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule.Commands
{
    public class SetTicketAsUsedCommand : IRequest<Result<Exception, bool>>
    {
        public Guid TicketCode { get; set; }
        public SetTicketAsUsedCommand(Guid ticketCode)
        {
            TicketCode = ticketCode;
        }
    }
}
