using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Models;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Queries;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Handlers
{
    public class GetUserAccountTicketsHandler:IRequestHandler<GetUserAccountTicketsQuery, Result<Exception, List<TicketModel>>>
    {
        private readonly IMapper _mapper;
    private readonly ITicketService _ticketService;
    public GetUserAccountTicketsHandler(IMapper mapper, ITicketService ticketService)
    {
        _mapper = mapper;
        _ticketService = ticketService;
    }

    public Task<Result<Exception, List<TicketModel>>> Handle(GetUserAccountTicketsQuery request, CancellationToken cancellationToken)
    {
        Result<Exception, List<TicketModel>> result = Result.Run(() =>
        {
            return _mapper.Map<List<TicketModel>>(_ticketService.RetrieveByUserAccount(request.UserAccountID));
        });

        return Task.FromResult(result);
    }
}
}
