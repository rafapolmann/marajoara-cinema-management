using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Queries;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Handlers
{
    public class GetSessionTicketsHandler : IRequestHandler<GetSessionTicketsQuery, Result<Exception, List<TicketModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketService _ticketService;
        public GetSessionTicketsHandler(IMapper mapper, ITicketService ticketService)
        {
            _mapper = mapper;
            _ticketService = ticketService;
        }
               
        public Task<Result<Exception, List<TicketModel>>> Handle(GetSessionTicketsQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, List<TicketModel>> result = Result.Run(() =>
            {
                return _mapper.Map<List<TicketModel>>(_ticketService.RetrieveBySession(request.SessionID));
            });

            return Task.FromResult(result);
        }
    }
}
