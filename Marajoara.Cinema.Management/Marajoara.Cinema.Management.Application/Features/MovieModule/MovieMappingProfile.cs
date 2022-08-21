﻿using AutoMapper;
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
            CreateMap<DeleteMovieCommand, Movie>();
            CreateMap<AddMovieCommand, Movie>();
        }
    }
}