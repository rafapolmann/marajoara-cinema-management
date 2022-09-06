using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule.Commands
{
    public class AddTicketCommand : IRequest<Result<Exception, int>>
    {
        //public DateTime PurchaseDate { get; set; }
        public int SeatNumber { get; set; }
        public int UserAccountID { get; set; }//Todo: Find a way to use the Authorization Token to get de current UserAccountID
        public int SessionID { get; set; }
        //public int SessionMovieID { get; set; }
        //public int SessionCineRoomID { get; set; }
    }
}
