using Marajoara.Cinema.Management.Application.Features.TicketModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Queries
{
    public class GetSessionTicketsQuery : IRequest<Result<Exception,List<TicketModel>>>
    {
        public int SessionID { get; set; }

        public GetSessionTicketsQuery(int id)
        {
            SessionID = id;
        }
    }
}
