using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.MovieModule
{
    public interface IMovieRepository
    {
        /// <summary>
        /// Add new register of Movie on database.
        /// </summary>
        /// <param name="movieToAdd">Movie that should be added.</param>
        void Add(Movie movieToAdd);

        /// <summary>
        /// Update Movie properties on database.
        /// </summary>
        /// <param name="movieToUpdate">An instance of Movie with all properties that will update on database. It should be linked with DBContext.</param>
        void Update(Movie movieToUpdate);

        /// <summary>
        /// Remove a given Movie on database and of the DBContext
        /// </summary>
        /// <param name="movieToDelete">An instance of Movie that will remove on database. It should be linked with DBContext.</param>
        void Delete(Movie movieToDelete);

        /// <summary>
        /// Retrieves a Movie with a given database ID
        /// </summary>
        /// <param name="movieID">ID used with condition for the search on database</param>
        /// <returns>Returns Movie persited on database or null.</returns>
        Movie Retrieve(int movieID);

        /// <summary>
        /// Retrieves a Movie with a given movie title
        /// </summary>
        /// <param name="movieTitle">Title used with condition for the search on database</param>
        /// <returns>Returns Movie persited on database or null.</returns>
        Movie RetrieveByTitle(string movieTitle);

        /// <summary>
        /// Retrieves the collection of Movies
        /// </summary>
        /// <returns>Returns collection of the Movies from database.</returns>
        IEnumerable<Movie> RetrieveAll();


        /// <summary>
        /// Returns the movies with sessions in between the informed dates
        /// </summary>
        /// <param name="initialDate">The start date</param>
        /// <param name="finalDate">The end date</param>
        /// <returns>A list of Session</returns>   
        IEnumerable<Movie> RetrieveBySessionDate(DateTime initialDate, DateTime finalDate);


        /// <summary>
        /// Returns a movie with sessions in between the informed dates
        /// </summary>
        /// <param name="initialDate">The start date</param>
        /// <param name="finalDate">The end date</param>
        /// <param name="movieID">Movie's ID</param>
        /// <returns></returns>
        Movie RetrieveBySessionDate(int movieID, DateTime initialDate, DateTime finalDate);


    }
}
