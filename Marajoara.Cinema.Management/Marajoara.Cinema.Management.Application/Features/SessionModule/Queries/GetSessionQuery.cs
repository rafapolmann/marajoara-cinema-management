using Marajoara.Cinema.Management.Application.Features.SessionModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Queries
{
    public class GetSessionQuery : IRequest<Result<Exception, SessionModel>>
    {
        public int SessionID { get; set; }

        public GetSessionQuery(int id)
        {
            SessionID = id;
        }
    }
}
