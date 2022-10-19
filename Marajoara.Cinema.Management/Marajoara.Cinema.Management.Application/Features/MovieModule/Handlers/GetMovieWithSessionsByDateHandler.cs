using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Models;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Queries;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Handlers
{
    public class GetMovieWithSessionsByDateHandler : IRequestHandler<GetMovieWithSessionsByDateQuery, Result<Exception, MovieWithSessionsModel>>
    {
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;
        public GetMovieWithSessionsByDateHandler(IMapper mapper, IMovieService movieService)
        {
            _mapper = mapper;
            _movieService = movieService;
        }

        public Task<Result<Exception, MovieWithSessionsModel>> Handle(GetMovieWithSessionsByDateQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, MovieWithSessionsModel> result = Result.Run(() =>
            {
                return _mapper.Map<MovieWithSessionsModel>(_movieService.GetMovieBySessionDateRange( request.MovieID, request.InitialDate, request.FinalDate));
            });

            return Task.FromResult(result);
        }
    }
}
