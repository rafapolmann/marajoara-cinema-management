using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Models;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Queries;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule.Handlers
{
    public class GetTicketsByUserAccountHandler : IRequestHandler<GetTicketsByUserAccountQuery, Result<Exception, List<TicketModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketService _ticketService;
        public GetTicketsByUserAccountHandler(IMapper mapper, ITicketService ticketService)
        {
            _mapper = mapper;
            _ticketService = ticketService;
        }

        public Task<Result<Exception, List<TicketModel>>> Handle(GetTicketsByUserAccountQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, List<TicketModel>> result = Result.Run(() =>
            {
                return _mapper.Map<List<TicketModel>>(_ticketService.RetrieveByUserAccount(request.UserAccountID));
            });

            return Task.FromResult(result);
        }
    }
}
