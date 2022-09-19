using Marajoara.Cinema.Management.Domain.Common;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule
{
    public class MovieService : IMovieService
    {
        private readonly IMarajoaraUnitOfWork _unitOfWork;
        private readonly IFileImageService _fileImageService;
        public MovieService(IMarajoaraUnitOfWork unitOfWork, IFileImageService fileImageService)
        {
            _unitOfWork = unitOfWork;
            _fileImageService = fileImageService;
        }

        public int AddMovie(Movie movie)
        {
            ValidateCineRoom(movie);

            if (_unitOfWork.Movies.RetrieveByTitle(movie.Title) != null)
                throw new Exception($"Already exists movie with title \"{movie.Title}\".");

            _unitOfWork.Movies.Add(movie);
            _unitOfWork.Commit();

            return movie.MovieID;
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
            return  _unitOfWork.Movies.RetrieveAll();
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
            ValidateCineRoom(movie);

            Movie movieOnDB = _unitOfWork.Movies.Retrieve(movie.MovieID);
            if (movieOnDB == null)
                throw new Exception($"Movie to update not found.");
            if (!movieOnDB.Title.Equals(movie.Title) && _unitOfWork.Movies.RetrieveByTitle(movie.Title) != null)
                throw new Exception($"Already exists movie with title {movie.Title}.");

            movie.CopyTo(movieOnDB);

            _unitOfWork.Movies.Update(movieOnDB);
            _unitOfWork.Commit();

            return true;
        }

        private void ValidateCineRoom(Movie movie)
        {
            if (movie == null)
                throw new ArgumentException("Movie parameter cannot be null.", nameof(movie));

            movie.Validate();
        }

        public bool UpdateMoviePoster(int movieID, Stream stream)
        {
            Movie movieOnDB = _unitOfWork.Movies.Retrieve(movieID);
            if (movieOnDB == null)
                throw new Exception($"Movie to update not found.");

            movieOnDB.Poster = _fileImageService.GetImageBytes(stream);

            _unitOfWork.Movies.Update(movieOnDB);
            _unitOfWork.Commit();

            return true;
        }

        public byte[] GetMoviePoster(int movieID)
        {
            Movie movieOnDB = _unitOfWork.Movies.Retrieve(movieID);
            if (movieOnDB == null)
                throw new Exception($"Movie not found.");

            return movieOnDB.Poster;
        }

        public bool DeleteMoviePoster(int movieID)
        {
            Movie movieOnDB = _unitOfWork.Movies.Retrieve(movieID);
            if (movieOnDB == null)
                throw new Exception($"Movie to update not found.");

            movieOnDB.Poster = null;

            _unitOfWork.Movies.Update(movieOnDB);
            _unitOfWork.Commit();

            return true;
        }
    }
}
