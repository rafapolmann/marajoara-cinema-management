using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Commands
{
    public class DeleteMoviePosterCommand : IRequest<Result<Exception, bool>>
    {
        public int MovieID { get; set; }

        public DeleteMoviePosterCommand(int movieID)
        {
            MovieID = movieID;
        }
    }
}
