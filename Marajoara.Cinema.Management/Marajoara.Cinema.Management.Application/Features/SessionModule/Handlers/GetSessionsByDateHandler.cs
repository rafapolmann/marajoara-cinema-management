using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Models;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Queries;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Handlers
{
    public class GetSessionsByDateHandler : IRequestHandler<GetSessionsByDateQuery, Result<Exception, List<SessionModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        public GetSessionsByDateHandler(IMapper mapper, ISessionService sessionService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
        }

        public Task<Result<Exception, List<SessionModel>>> Handle(GetSessionsByDateQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, List<SessionModel>> result = Result.Run(() =>
            {
                return _mapper.Map<List<SessionModel>>(_sessionService.GetSessionsByDate(request.Date));
            });

            return Task.FromResult(result);
        }
    }
}
