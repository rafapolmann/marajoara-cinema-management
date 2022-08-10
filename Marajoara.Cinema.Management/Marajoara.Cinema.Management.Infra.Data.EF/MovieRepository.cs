using Marajoara.Cinema.Management.Domain.MovieModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Infra.Data.EF
{
    public class MovieRepository : IMovieRepository
    {
        public void Add(Movie movieToAdd)
        {
            throw new NotImplementedException();
        }

        public void Delete(Movie movieToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> RetriveAll()
        {
            throw new NotImplementedException();
        }

        public Movie RetriveByTitle(string movieTitle)
        {
            throw new NotImplementedException();
        }

        public void Update(Movie movieToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
