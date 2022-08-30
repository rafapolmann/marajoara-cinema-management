using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Commands
{
    public class UpdateMovieCommand : IRequest<Result<Exception, bool>>
    {
        private TimeSpan _duration;
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MovieDuration { get; set; }
        public TimeSpan Duration
        {
            get
            {
                if (TimeSpan.TryParse(MovieDuration, out _duration))
                    return _duration;
                else
                    return TimeSpan.Zero;
            }
        }
        public bool Is3D { get; set; }
        public bool IsOrignalAudio { get; set; }
    }
}
