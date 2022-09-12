using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule.Commands
{
    public class DeleteTicketCommand : IRequest<Result<Exception, bool>>
    {
        public int TicketID { get; set; }
        public DeleteTicketCommand(int ticketID)
        {
            TicketID = ticketID;
        }
    }
}
