using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.MovieModule
{
    public interface IMovieService
    {
        int AddMovie(Movie movie);
        bool UpdateMovie(Movie movie);
        bool DeleteteMovie(Movie movie);
        Movie GetMovie(int id);
        Movie GetMovie(string title);
        IEnumerable<Movie> GetAllMovies();
    }
}
