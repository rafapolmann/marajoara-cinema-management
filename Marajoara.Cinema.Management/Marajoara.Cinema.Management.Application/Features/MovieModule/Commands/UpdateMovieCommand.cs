using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Commands
{
    public class UpdateMovieCommand : IRequest<Result<Exception, bool>>
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Minutes { get; set; }
        public bool Is3D { get; set; }
        public bool IsOriginalAudio { get; set; }
    }
}
