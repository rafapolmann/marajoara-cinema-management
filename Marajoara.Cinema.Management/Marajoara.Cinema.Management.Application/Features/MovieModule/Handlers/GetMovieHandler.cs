using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Models;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Queries;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Handlers
{
    public class GetMovieHandler : IRequestHandler<GetMovieQuery, Result<Exception, MovieModel>>
    {
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;

        public GetMovieHandler(IMapper mapper, IMovieService movieService)
        {
            _mapper = mapper;
            _movieService = movieService;
        }

        public Task<Result<Exception, MovieModel>> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, MovieModel> result = Result.Run(() =>
            {
                if (request.MovieID > 0)
                    return _mapper.Map<MovieModel>(_movieService.GetMovie(request.MovieID));
                else
                    return _mapper.Map<MovieModel>(_movieService.GetMovie(request.Title));

            });

            return Task.FromResult(result);
        }
    }
}
