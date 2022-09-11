using Marajoara.Cinema.Management.Application.Features.MovieModule.Commands;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Handlers
{
    public class UpdateMoviePosterHandler : IRequestHandler<UpdateMoviePosterCommand, Result<Exception, bool>>
    {
        private readonly IMovieService _movieService;
        public UpdateMoviePosterHandler(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public Task<Result<Exception, bool>> Handle(UpdateMoviePosterCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                return _movieService.UpdateMoviePoster(request.MovieID, request.PosterStream);
            });

            return Task.FromResult(result);
        }
    }
}
