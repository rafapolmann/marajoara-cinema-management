using Marajoara.Cinema.Management.Application.Features.MovieModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Queries
{
    public class GetMoviesBySessionDateQuery : IRequest<Result<Exception, List<MovieWithSessionsModel>>>
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public GetMoviesBySessionDateQuery(DateTime initialDate, DateTime finalDate)
        {
            InitialDate = initialDate;
            FinalDate = finalDate;
        }
    }
}
