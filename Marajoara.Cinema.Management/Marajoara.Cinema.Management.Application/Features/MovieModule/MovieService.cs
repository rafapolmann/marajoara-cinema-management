using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule
{
    public class MovieService : IMovieService
    {
        private readonly IMarajoaraUnitOfWork _unitOfWork;
        public MovieService(IMarajoaraUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int AddMovie(Movie movie)
        {
            //2.Filmes não podem ter nomes repetidos - validação deve ocorrer em tempo
            //real sem que haja necessidade de submeter o formulário;
            throw new System.NotImplementedException();
        }

        public bool RemoveMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _unitOfWork.Movies.RetrieveAll();
        }

        public Movie GetMovie(int id)
        {
            return _unitOfWork.Movies.Retrieve(id);
        }

        public Movie GetMovie(string title)
        {
            return _unitOfWork.Movies.RetrieveByTitle(title);
        }

        public bool UpdateMovie(Movie movie)
        {
            //2.Filmes não podem ter nomes repetidos - validação deve ocorrer em tempo
            //real sem que haja necessidade de submeter o formulário;
            throw new System.NotImplementedException();
        }
    }
}
