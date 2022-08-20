using Marajoara.Cinema.Management.Application.Features.MovieModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Queries
{
    public class GetMovieQuery : IRequest<Result<Exception, MovieModel>>
    {
        public int MovieID { get; set; }
        public string Title { get; set; }

        public GetMovieQuery(string title)
        {
            Title = title;
        }

        public GetMovieQuery(int id)
        {
            MovieID = id;
        }
    }
}
