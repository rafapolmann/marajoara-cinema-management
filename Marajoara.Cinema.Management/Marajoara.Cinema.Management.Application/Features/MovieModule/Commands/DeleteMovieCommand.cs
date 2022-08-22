using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Commands
{
    public class DeleteMovieCommand : IRequest<Result<Exception, bool>>
    {
        public int MovieID { get; set; }
        public string Title { get; set; }

        public DeleteMovieCommand(int id)
        {
            MovieID = id;
        }

        public DeleteMovieCommand(string title)
        {
            Title = title;
        }
    }
}
