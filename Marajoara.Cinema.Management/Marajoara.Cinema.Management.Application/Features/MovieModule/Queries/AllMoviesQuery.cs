using Marajoara.Cinema.Management.Application.Features.MovieModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Queries
{
    public class AllMoviesQuery : IRequest<Result<Exception, List<MovieModel>>> { }
}
