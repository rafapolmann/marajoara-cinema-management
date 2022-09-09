using Marajoara.Cinema.Management.Application.Features.MovieModule.Commands;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Handlers
{
    public class DeleteMoviePosterHandler : IRequestHandler<DeleteMoviePosterCommand, Result<Exception, bool>>
    {
        private readonly IMovieService _movieService;
        public DeleteMoviePosterHandler(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public Task<Result<Exception, bool>> Handle(DeleteMoviePosterCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                return _movieService.DeleteMoviePoster(request.MovieID);
            });

            return Task.FromResult(result);
        }
    }
}
