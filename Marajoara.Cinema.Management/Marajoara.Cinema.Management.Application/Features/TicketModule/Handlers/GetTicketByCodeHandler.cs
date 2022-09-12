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
        public class GetTicketByCodeHandler : IRequestHandler<GetTicketByCodeQuery, Result<Exception, TicketModel>>
        {
            private readonly IMapper _mapper;
            private readonly ITicketService _ticketService;
            public GetTicketByCodeHandler(IMapper mapper, ITicketService ticketService)
            {
                _mapper = mapper;
                _ticketService = ticketService;
            }

            public Task<Result<Exception, TicketModel>> Handle(GetTicketByCodeQuery request, CancellationToken cancellationToken)
            {
                Result<Exception, TicketModel> result = Result.Run(() =>
                {
                    return _mapper.Map<TicketModel>(_ticketService.RetrieveTicketByCode(request.TicketCode));
                });

                return Task.FromResult(result);
            }
        }
}
