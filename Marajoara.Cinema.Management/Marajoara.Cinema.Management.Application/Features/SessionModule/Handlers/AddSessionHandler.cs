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
    public class AddSessionHandler : IRequestHandler<AddSessionCommand, Result<Exception, int>>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        public AddSessionHandler(IMapper mapper, ISessionService sessionService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
        }

        public Task<Result<Exception, int>> Handle(AddSessionCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, int> result = Result.Run(() =>
            {
                return _sessionService.AddSession(_mapper.Map<Session>(request));
            });

            return Task.FromResult(result);
        }
    }
}
