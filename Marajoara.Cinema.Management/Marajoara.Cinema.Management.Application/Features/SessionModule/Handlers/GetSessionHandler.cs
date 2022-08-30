using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Models;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Queries;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Handlers
{
    public class GetSessionHandler : IRequestHandler<GetSessionQuery, Result<Exception, SessionModel>>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        public GetSessionHandler(IMapper mapper, ISessionService sessionService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
        }

        public Task<Result<Exception, SessionModel>> Handle(GetSessionQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, SessionModel> result = Result.Run(() =>
            {
                return _mapper.Map<SessionModel>(_sessionService.GetSession(request.SessionID));
            });

            return Task.FromResult(result);
        }
    }
}
