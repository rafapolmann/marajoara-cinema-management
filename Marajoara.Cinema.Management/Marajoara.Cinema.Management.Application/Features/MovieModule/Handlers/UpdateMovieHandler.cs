using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Commands;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Handlers
{
    public class UpdateMovieHandler : IRequestHandler<UpdateMovieCommand, Result<Exception, bool>>
    {
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;
        public UpdateMovieHandler(IMapper mapper, IMovieService movieService)
        {
            _mapper = mapper;
            _movieService = movieService;
        }

        public Task<Result<Exception, bool>> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                return _movieService.UpdateMovie(_mapper.Map<Movie>(request));
            });

            return Task.FromResult(result);
        }
    }
}
