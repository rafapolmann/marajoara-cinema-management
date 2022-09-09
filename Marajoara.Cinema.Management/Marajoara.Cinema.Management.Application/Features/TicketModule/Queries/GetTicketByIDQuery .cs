using Marajoara.Cinema.Management.Application.Features.TicketModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule.Queries
{
    public class GetTicketByIDQuery : IRequest<Result<Exception, TicketModel>>
    {
        public int TicketID { get; set; }

        public GetTicketByIDQuery(int id)
        {
            TicketID = id;
        }
    }
}
