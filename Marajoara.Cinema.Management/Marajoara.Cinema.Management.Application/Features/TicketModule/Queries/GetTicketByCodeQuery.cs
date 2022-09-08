using Marajoara.Cinema.Management.Application.Features.TicketModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule.Queries
{
    public class GetTicketByCodeQuery : IRequest<Result<Exception, TicketModel>>
    {
        public Guid TicketCode { get; set; }

        public GetTicketByCodeQuery(Guid guid)
        {
            TicketCode = guid;
        }
    }
}
