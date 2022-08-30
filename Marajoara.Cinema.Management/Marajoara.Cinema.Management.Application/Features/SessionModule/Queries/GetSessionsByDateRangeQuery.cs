using Marajoara.Cinema.Management.Application.Features.SessionModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Queries
{
    public class GetSessionsByDateRangeQuery : IRequest<Result<Exception, List<SessionModel>>>
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public GetSessionsByDateRangeQuery(DateTime initialDate, DateTime finalDate)
        {
            InitialDate = initialDate;
            FinalDate = finalDate;
        }
    }
}
