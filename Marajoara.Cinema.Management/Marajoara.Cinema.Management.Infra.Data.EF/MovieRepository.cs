using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Infra.Data.EF
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MarajoaraContext DBContext;

        public MovieRepository(MarajoaraContext dbContext)
        {
            DBContext = dbContext;
        }

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
