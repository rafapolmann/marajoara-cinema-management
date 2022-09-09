using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Queries
{
    public class GetMoviePosterQuery : IRequest<Result<Exception, byte[]>>
    {
        public int MovieID { get; set; }
        public GetMoviePosterQuery(int movieID)
        {
            MovieID = movieID;
        }
    }
}
