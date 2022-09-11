using System.Collections.Generic;
using System.IO;

namespace Marajoara.Cinema.Management.Domain.MovieModule
{
    public interface IMovieService
    {
        /// <summary>
        /// Add new movie in the system.
        /// In case of the movie parameter is null, will throw exception.
        /// Will not possible to register a new movie with already existing title.
        /// </summary>
        /// <param name="movie">Movie to add.</param>
        /// <returns>Return the ID of new movie registered in the system.</returns>
        int AddMovie(Movie movie);

        /// <summary>
        /// Update all properties of a given movie in the system.
        /// In case of the movie parameter is null or cine room will not find in system, will throw exception.
        /// </summary>
        /// <param name="movie">Movie with properties to update.</param>
        bool UpdateMovie(Movie movie);

        /// <summary>
        /// Remove a given movie of the system.
        /// In case of the movie parameter is null or movie will not find in system, will throw exception.
        /// </summary>
        /// <param name="movie">Movie to remove.</param>
        /// <returns>Return true if movie was removed with success.</returns>
        bool RemoveMovie(Movie movie);

        /// <summary>
        /// Method to get a given Movie registered on the system based on database ID.
        /// </summary>
        /// <param name="id">ID used as parameter in the search.</param>
        /// <returns>Return found Movie or null if doesn't exists movie with this ID.</returns>
        Movie GetMovie(int id);

        /// <summary>
        /// Method to get a given Movie registered on the system based on movie title.
        /// </summary>
        /// <param name="title">Movie Title used as parameter in the search.</param>
        /// <returns>Return found Movie or null if doesn't exists any movie with this title.</returns>
        Movie GetMovie(string title);

        /// <summary>
        /// Method to get all movies registered on the system.
        /// </summary>
        /// <returns>Collection of movies.</returns>
        IEnumerable<Movie> GetAllMovies();

        /// <summary>
        /// Will set an image to movie poster property in the system.
        /// Valid formats for Stream are PNG, JPG and BMP. Max image size is 500kb.
        /// If MovieID will not find on database, will throw an Exception.
        /// </summary>
        /// <param name="movieID">ID used as parameter in the command.</param>
        /// <param name="stream">Data stream containing a image for movie poster</param>
        /// <returns>Returns true if process will succeed.</returns>
        bool UpdateMoviePoster(int movieID, Stream stream);

        /// <summary>
        /// Return movie poster image data.
        /// </summary>
        /// <param name="movieID">ID used as parameter in the search.</param>
        /// <returns>Byte array data of poster persisted on system.</returns>
        byte[] GetMoviePoster(int movieID);

        /// <summary>
        /// Removes movie poster data from system.
        /// Movie poster data will be setted to null on database.
        /// If MovieID will not find on database, will throw an Exception.
        /// </summary>
        /// <param name="movieID">ID used as parameter in the command.</param>
        /// <returns>Returns true if process will succeed.</returns>
        bool DeleteMoviePoster(int movieID);
    }
}
