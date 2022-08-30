using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Commands;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Handlers
{
    public class DeleteSessionHandler : IRequestHandler<DeleteSessionCommand, Result<Exception, bool>>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        public DeleteSessionHandler(IMapper mapper, ISessionService sessionService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
        }

        public Task<Result<Exception, bool>> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                return _sessionService.RemoveSession(_mapper.Map<Session>(request));
            });

            return Task.FromResult(result);
        }
    }
}
