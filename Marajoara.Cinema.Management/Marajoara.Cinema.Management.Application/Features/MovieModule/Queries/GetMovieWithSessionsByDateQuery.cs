using Marajoara.Cinema.Management.Application.Features.MovieModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Queries
{
    public class GetMovieWithSessionsByDateQuery : IRequest<Result<Exception, MovieWithSessionsModel>>
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public int MovieID{ get; set; }
        public GetMovieWithSessionsByDateQuery(int movieID,DateTime initialDate, DateTime finalDate)
        {
            MovieID = movieID;
            InitialDate = initialDate;
            FinalDate = finalDate;
        }
    }
}
