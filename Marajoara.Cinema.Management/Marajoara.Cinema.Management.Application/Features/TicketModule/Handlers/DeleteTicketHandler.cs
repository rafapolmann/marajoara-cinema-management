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
    public class DeleteTicketHandler : IRequestHandler<DeleteTicketCommand, Result<Exception, bool>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketService _ticketService;
        public DeleteTicketHandler(IMapper mapper, ITicketService ticketService)
        {
            _mapper = mapper;
            _ticketService = ticketService;
        }

        public Task<Result<Exception, bool>> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                return _ticketService.RemoveTicket(_mapper.Map<Ticket>(request));
            });

            return Task.FromResult(result);
        }
    }
}
