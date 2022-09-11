using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.IO;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Commands
{
    public class UpdateMoviePosterCommand : IRequest<Result<Exception, bool>>
    {
        public int MovieID { get; set; }
        public Stream PosterStream { get; set; }

        public UpdateMoviePosterCommand(int movieID, Stream stream)
        {
            MovieID = movieID;
            PosterStream = stream;
        }
    }
}
