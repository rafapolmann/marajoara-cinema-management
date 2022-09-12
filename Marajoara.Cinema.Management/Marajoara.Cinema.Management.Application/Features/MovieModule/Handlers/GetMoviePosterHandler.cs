using Marajoara.Cinema.Management.Application.Features.MovieModule.Queries;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Handlers
{
    public class GetMoviePosterHandler : IRequestHandler<GetMoviePosterQuery, Result<Exception, byte[]>>
    {
        private readonly IMovieService _movieService;
        public GetMoviePosterHandler(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public Task<Result<Exception, byte[]>> Handle(GetMoviePosterQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, byte[]> result = Result.Run(() =>
            {
                return _movieService.GetMoviePoster(request.MovieID);
            });

            return Task.FromResult(result);
        }
    }
}
