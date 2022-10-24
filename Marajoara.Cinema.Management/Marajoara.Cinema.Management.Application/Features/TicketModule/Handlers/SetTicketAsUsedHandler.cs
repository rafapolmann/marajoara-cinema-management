using Marajoara.Cinema.Management.Application.Features.TicketModule.Commands;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule.Handlers
{
    public class SetTicketAsUsedHandler : IRequestHandler<SetTicketAsUsedCommand, Result<Exception, bool>>
    {
        private readonly ITicketService _ticketService;
        public SetTicketAsUsedHandler( ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public Task<Result<Exception, bool>> Handle(SetTicketAsUsedCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                return _ticketService.SetTicketAsUsed(request.TicketCode);
            });

            return Task.FromResult(result);
        }
    }
}
