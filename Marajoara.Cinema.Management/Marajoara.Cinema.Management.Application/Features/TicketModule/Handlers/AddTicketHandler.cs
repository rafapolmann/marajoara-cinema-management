using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Commands;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule.Handlers
{
    public class AddTicketHandler : IRequestHandler<AddTicketCommand, Result<Exception, int>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketService _ticketService;
        public AddTicketHandler(IMapper mapper, ITicketService ticketService)
        {
            _mapper = mapper;
            _ticketService = ticketService;
        }

        public Task<Result<Exception, int>> Handle(AddTicketCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, int> result = Result.Run(() =>
            {
                return _ticketService.AddTicket(_mapper.Map<Ticket>(request));
            });

            return Task.FromResult(result);
        }
    }
}

