using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule.Commands
{
    public class AddTicketCommand : IRequest<Result<Exception, int>>
    {
        public int SeatNumber { get; set; }
        public int UserAccountID { get; set; }
        public int SessionID { get; set; }
        
    }
}
