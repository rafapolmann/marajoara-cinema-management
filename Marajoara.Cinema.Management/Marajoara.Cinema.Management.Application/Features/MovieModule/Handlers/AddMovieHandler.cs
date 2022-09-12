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
    public class AddMovieHandler : IRequestHandler<AddMovieCommand, Result<Exception, int>>
    {
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;
        public AddMovieHandler(IMapper mapper, IMovieService movieService)
        {
            _mapper = mapper;
            _movieService = movieService;
        }

        public Task<Result<Exception, int>> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, int> result = Result.Run(() =>
            {
                return _movieService.AddMovie(_mapper.Map<Movie>(request));
            });

            return Task.FromResult(result);
        }
    }
}
