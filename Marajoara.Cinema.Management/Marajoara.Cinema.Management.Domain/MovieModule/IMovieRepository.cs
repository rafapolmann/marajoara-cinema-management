using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
