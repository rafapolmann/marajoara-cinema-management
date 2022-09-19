using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Models;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Queries;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Handlers
{
    public class AllMoviesHandler : IRequestHandler<AllMoviesQuery, Result<Exception, List<MovieModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;
        public AllMoviesHandler(IMapper mapper, IMovieService movieService)
        {
            _mapper = mapper;
            _movieService = movieService;
        }

        public Task<Result<Exception, List<MovieModel>>> Handle(AllMoviesQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, List<MovieModel>> result = Result.Run(() =>
            {
                return _mapper.Map<List<MovieModel>>(_movieService.GetAllMovies());
            });

            return Task.FromResult(result);
        }
    }
}
