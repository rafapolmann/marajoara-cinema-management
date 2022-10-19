using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Commands;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Models;
using Marajoara.Cinema.Management.Domain.MovieModule;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<Movie, MovieModel>();
            CreateMap<Movie, MovieWithSessionsModel>();
            CreateMap<DeleteMovieCommand, Movie>();
            CreateMap<AddMovieCommand, Movie>();
            CreateMap<UpdateMovieCommand, Movie>();
        }
    }
}
