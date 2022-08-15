using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using System.Collections.Generic;
using System.Linq;

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
            DBContext.Movies.Add(movieToAdd);
        }

        public void Delete(Movie movieToDelete)
        {
            DBContext.Entry(movieToDelete).State = System.Data.Entity.EntityState.Deleted;
        }

        public IEnumerable<Movie> RetrieveAll()
        {
            return DBContext.Movies;
        }

        public Movie Retrieve(int movieID)
        {
            return DBContext.Movies
                            .Where(mv => mv.MovieID.Equals(movieID))
                            .FirstOrDefault();
        }

        public Movie RetrieveByTitle(string movieTitle)
        {
            return DBContext.Movies
                            .Where(mv => mv.Title.Equals(movieTitle))
                            .FirstOrDefault();
        }

        public void Update(Movie movieToUpdate)
        {
            DBContext.Entry(movieToUpdate).State = System.Data.Entity.EntityState.Modified;
        }
    }
}