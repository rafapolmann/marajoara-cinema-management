using Marajoara.Cinema.Management.Application.Features.TicketModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Queries
{
    public class GetSessionOccupiedSeatsQuery : IRequest<Result<Exception, List<TicketSeatModel>>>
    {
        public int SessionID { get; set; }

        public GetSessionOccupiedSeatsQuery(int id)
        {
            SessionID = id;
        }
    }
}
