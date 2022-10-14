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
    public class GetMoviesBySessionDateHandler : IRequestHandler<GetMoviesBySessionDateQuery, Result<Exception, List<MovieWithSessionsModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;
        public GetMoviesBySessionDateHandler(IMapper mapper, IMovieService movieService)
        {
            _mapper = mapper;
            _movieService = movieService;
        }

        public Task<Result<Exception, List<MovieWithSessionsModel>>> Handle(GetMoviesBySessionDateQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, List<MovieWithSessionsModel>> result = Result.Run(() =>
            {
                return _mapper.Map<List<MovieWithSessionsModel>>(_movieService.GetMovieBySessionDateRange(request.InitialDate,request.FinalDate));
            });

            return Task.FromResult(result);
        }
    }
}
