using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Commands
{
    public class AddMovieCommand : IRequest<Result<Exception, int>>
    {
        //public byte[] Poster { get; set; } Acho que tem que ser um String Base64
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Is3D { get; set; }
        public bool IsOrignalAudio { get; set; }
    }
}
