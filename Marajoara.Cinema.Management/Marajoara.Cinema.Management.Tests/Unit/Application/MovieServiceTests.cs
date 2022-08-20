using FluentAssertions;
using Marajoara.Cinema.Management.Application.Features.MovieModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Tests.Unit.Application
{
    [TestClass]
    public class MovieServiceTests
    {
        private IMovieService _movieService;
        private Mock<IMarajoaraUnitOfWork> _unitOfWorkMock;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWorkMock = new Mock<IMarajoaraUnitOfWork>();
            _movieService = new MovieService(_unitOfWorkMock.Object);
        }

        #region Gets_Movie
        [TestMethod]
        public void MovieService_GetMovie_Should_Return_Movie_When_Movie_Title_Exists()
        {
            string movieTitleToRetrive = "Title";
            Movie movieOnDB = GetMovieToTest();

            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveByTitle(movieOnDB.Title)).Returns(movieOnDB);

            _movieService.GetMovie(movieTitleToRetrive).Should().NotBeNull();
            _unitOfWorkMock.Verify(uow => uow.Movies.RetrieveByTitle(movieTitleToRetrive), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_GetMovie_Should_Return_Null_When_Movie_Title_Not_Exists()
        {
            string movieTitleToRetrive = "notExistsTitle";
            Movie movieOnDB = GetMovieToTest();

            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveByTitle(movieOnDB.Title)).Returns(movieOnDB);

            _movieService.GetMovie(movieTitleToRetrive).Should().BeNull();
            _unitOfWorkMock.Verify(uow => uow.Movies.RetrieveByTitle(movieTitleToRetrive), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_GetMovie_Should_Return_Null_When_Movie_Title_Parameter_Is_Null()
        {
            string movieTitleToRetrive = null;
            Movie movieOnDB = GetMovieToTest();

            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveByTitle(movieOnDB.Title)).Returns(movieOnDB);

            _movieService.GetMovie(movieTitleToRetrive).Should().BeNull();
            _unitOfWorkMock.Verify(uow => uow.Movies.RetrieveByTitle(movieTitleToRetrive), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_GetMovie_Should_Return_Movie_When_Movie_ID_Exists()
        {
            int movieIDToRetrive = 1;
            Movie movieOnDB = GetMovieToTest();

            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB.MovieID)).Returns(movieOnDB);

            _movieService.GetMovie(movieIDToRetrive).Should().NotBeNull();
            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieIDToRetrive), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_GetMovie_Should_Return_Null_When_Movie_ID_Not_Exists()
        {
            int movieIdToRetrive = 3;
            Movie movieOnDB = GetMovieToTest(1, "MovieTitle", "MovieOnDB");

            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB.MovieID)).Returns(movieOnDB);

            _movieService.GetMovie(movieIdToRetrive).Should().BeNull();
            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieIdToRetrive), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_GetAllMovies_Should_Return_All_Movies()
        {
            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveAll()).Returns(new List<Movie> { GetMovieToTest() });

            _movieService.GetAllMovies().Should().HaveCount(1);
            _unitOfWorkMock.Verify(uow => uow.Movies.RetrieveAll(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_GetAllMovies_Should_Return_Empty_Collection_When_There_Are_No_Movies()
        {
            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveAll()).Returns(new List<Movie>());

            _movieService.GetAllMovies().Should().BeEmpty();
            _unitOfWorkMock.Verify(uow => uow.Movies.RetrieveAll(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion Gets_Movie

        private Movie GetMovieToTest(int movieID = 1,
                                     string title = "Title",
                                     string description = "Description",
                                     bool is3D = false,
                                     bool isOrignalAudio = false)
        {

            return new Movie
            {
                MovieID = movieID,
                Title = title,
                Description = description,
                Duration = new TimeSpan(1, 30, 0),
                Is3D = is3D,
                IsOrignalAudio = isOrignalAudio
            };
        }
    }
}
