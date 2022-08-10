using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.MovieModule
{
    public interface IMovieRepository
    {
        void Add(Movie movieToAdd);        
        void Update(Movie movieToUpdate);
        void Delete(Movie movieToDelete);
        Movie RetriveByTitle(string movieTitle);
        IEnumerable<Movie> RetriveAll();
    }
}
