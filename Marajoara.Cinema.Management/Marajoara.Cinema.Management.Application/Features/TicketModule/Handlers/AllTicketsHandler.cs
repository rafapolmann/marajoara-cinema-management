using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Models;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Queries;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule.Handlers
{
    public class AllTicketsHandler : IRequestHandler<AllTicketsQuery, Result<Exception, List<TicketModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketService _ticketService;

        public AllTicketsHandler(IMapper mapper, ITicketService ticketService)
        {
            _mapper = mapper;
            _ticketService = ticketService;
        }

        public Task<Result<Exception, List<TicketModel>>> Handle(AllTicketsQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, List<TicketModel>> result = Result.Run(() =>
            {
                return _mapper.Map<List<TicketModel>>(_ticketService.RetrieveAll());
            });

            return Task.FromResult(result);
        }
    }
}
