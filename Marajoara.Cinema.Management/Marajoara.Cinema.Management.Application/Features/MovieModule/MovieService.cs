﻿using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule
{
    public class MovieService : IMovieService
    {
        private readonly IMarajoaraUnitOfWork _unitOfWork;
        public MovieService(IMarajoaraUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int AddMovie(Movie movie)
        {
            //2.Filmes não podem ter nomes repetidos
            throw new System.NotImplementedException();
        }

        public bool RemoveMovie(Movie movie)
        {
            if (movie == null)
                throw new ArgumentException("Movie parameter cannot be null.", nameof(movie));

            Movie movieToDelete = movie.MovieID > 0 ?
                                        _unitOfWork.Movies.Retrieve(movie.MovieID) :
                                        _unitOfWork.Movies.RetrieveByTitle(movie.Title);

            if (movieToDelete == null)
                throw new Exception($"Movie not found.");
            if (_unitOfWork.Sessions.RetrieveByMovieTitle(movieToDelete.Title).ToList().Count > 0)
                throw new Exception($"Cannot possible remove movie {movieToDelete.Title}. There are sessions linked with this movie.");

            _unitOfWork.Movies.Delete(movieToDelete);
            _unitOfWork.Commit();

            return true;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _unitOfWork.Movies.RetrieveAll();
        }

        public Movie GetMovie(int id)
        {
            return _unitOfWork.Movies.Retrieve(id);
        }

        public Movie GetMovie(string title)
        {
            return _unitOfWork.Movies.RetrieveByTitle(title);
        }

        public bool UpdateMovie(Movie movie)
        {
            //2.Filmes não podem ter nomes repetidos - validação deve ocorrer em tempo
            //real sem que haja necessidade de submeter o formulário;
            throw new System.NotImplementedException();
        }
    }
}
