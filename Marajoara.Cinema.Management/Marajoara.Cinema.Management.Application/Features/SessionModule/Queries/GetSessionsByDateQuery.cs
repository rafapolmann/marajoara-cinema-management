using Marajoara.Cinema.Management.Application.Features.SessionModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Queries
{
    public class GetSessionsByDateQuery : IRequest<Result<Exception, List<SessionModel>>>
    {
        public DateTime Date { get; set; }
        public GetSessionsByDateQuery(DateTime dateTime)
        {
            Date = dateTime;
        }
    }
}
