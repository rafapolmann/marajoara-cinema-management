using System.Collections.Generic;

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
        /// <param name="id">ID used with parameter in the search.</param>
        /// <returns>Return found Movie or null if doesn't exists movie with this ID.</returns>
        Movie GetMovie(int id);

        /// <summary>
        /// Method to get a given Movie registered on the system based on movie title.
        /// </summary>
        /// <param name="title">Movie Title used with parameter in the search.</param>
        /// <returns>Return found Movie or null if doesn't exists any movie with this title.</returns>
        Movie GetMovie(string title);

        /// <summary>
        /// Method to get all movies registered on the system.
        /// </summary>
        /// <returns>Collection of movies.</returns>
        IEnumerable<Movie> GetAllMovies();
    }
}
