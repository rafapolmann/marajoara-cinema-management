using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
            DBContext.Entry(movieToDelete).State = EntityState.Deleted;
        }

        public IEnumerable<Movie> RetrieveAll()
        {
            return DBContext.Movies;
        }

        public Movie Retrieve(int movieID)
        {
            return DBContext.Movies
                            .Where(mv => mv.MovieID == movieID)
                            .FirstOrDefault();
        }
        public IEnumerable<Movie> RetrieveBySessionDate(DateTime initialDate, DateTime finalDate)
        {
            //Todo: Find a way to generate a better query. (should improve performance)

            var result = DBContext.Movies.Where(m => m.Sessions.Any(s => s.SessionDate >= initialDate && s.SessionDate <= finalDate)).Include(m => m.Sessions.Where(s => s.SessionDate >= initialDate && s.SessionDate <= finalDate));

            var test = result.Distinct();
            return result.Distinct();
        }

        public Movie RetrieveBySessionDate(int movieID, DateTime initialDate, DateTime finalDate)
        {
            var result = DBContext.Movies.Include(m => m.Sessions.Where(s => s.SessionDate >= initialDate && s.SessionDate <= finalDate)).FirstOrDefault(m => m.MovieID == movieID);

            return result;
        }

        public Movie RetrieveByTitle(string movieTitle)
        {
            return DBContext.Movies
                            .Where(mv => mv.Title == movieTitle)
                            .FirstOrDefault();
        }

        public void Update(Movie movieToUpdate)
        {
            DBContext.Entry(movieToUpdate).State = EntityState.Modified;
        }
    }

}