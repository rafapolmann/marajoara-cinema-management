using FluentAssertions;
using Marajoara.Cinema.Management.Application.Features.MovieModule;
using Marajoara.Cinema.Management.Domain.Common;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Marajoara.Cinema.Management.Tests.Unit.Application
{
    [TestClass]
    public class MovieServiceTests
    {
        private IMovieService _movieService;
        private Mock<IFileImageService> _fileImageServiceMock;
        private Mock<IMarajoaraUnitOfWork> _unitOfWorkMock;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWorkMock = new Mock<IMarajoaraUnitOfWork>();
            _fileImageServiceMock = new Mock<IFileImageService>();
            _movieService = new MovieService(_unitOfWorkMock.Object, _fileImageServiceMock.Object);
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


        [TestMethod]
        public void MovieService_GetMoviesBySessionDateRange_Should_Return_Movies()
        {
            DateTime startDate = new DateTime(2022, 1, 1);
            DateTime endDate = new DateTime(2022, 1, 30);
            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveBySessionDate(startDate, endDate)).Returns(new List<Movie> { GetMovieToTest() });

            _movieService.GetMoviesBySessionDateRange(startDate,endDate).Should().HaveCount(1);
            _unitOfWorkMock.Verify(uow => uow.Movies.RetrieveBySessionDate(startDate, endDate), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_GetMovieBySessionDateRange_Should_Return_Movie()
        {
            int movieId = 10;
            DateTime startDate = new DateTime(2022, 1, 1);
            DateTime endDate = new DateTime(2022, 1, 30);
            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveBySessionDate(movieId, startDate, endDate)).Returns(GetMovieToTest(movieId) );

            _movieService.GetMovieBySessionDateRange(movieId, startDate, endDate).Should().NotBeNull();

            _unitOfWorkMock.Verify(uow => uow.Movies.RetrieveBySessionDate(movieId,startDate, endDate), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion Gets_Movie

        #region RemoveMovie
        [TestMethod]
        public void MovieService_RemoveMovie_Should_Remove_A_Given_Movie_When_MovieID_Exists()
        {
            Movie movieOnDB = GetMovieToTest();

            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByMovieTitle(movieOnDB.Title)).Returns(new List<Session>());
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB.MovieID)).Returns(movieOnDB);

            Movie movieToDelete = new Movie { MovieID = 1 };
            _movieService.RemoveMovie(movieToDelete).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByMovieTitle(movieOnDB.Title), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Movies.Delete(movieOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void MovieService_RemoveMovie_Should_Remove_A_Given_Movie_When_Movie_Name_Exists()
        {
            Movie movieOnDB = GetMovieToTest();

            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByMovieTitle(movieOnDB.Title)).Returns(new List<Session>());
            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveByTitle(movieOnDB.Title)).Returns(movieOnDB);

            Movie movieToDelete = new Movie { MovieID = 0, Title = "Title" };
            _movieService.RemoveMovie(movieToDelete).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByMovieTitle(movieOnDB.Title), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Movies.Delete(movieOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void MovieService_RemoveMovie_Should_Throw_Exception_When_Movie_To_Remove_Is_Linked_With_Some_Session()
        {
            Movie movieOnDB = GetMovieToTest();
            Session movieSession = new Session { Movie = movieOnDB };

            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByMovieTitle(movieOnDB.Title)).Returns(new List<Session> { movieSession });
            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveByTitle(movieOnDB.Title)).Returns(movieOnDB);
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB.MovieID)).Returns(movieOnDB);

            Action action = () => _movieService.RemoveMovie(movieOnDB);
            action.Should().Throw<Exception>().WithMessage($"Cannot possible remove movie {movieOnDB.Title}. There are sessions linked with this movie.");

            _unitOfWorkMock.Verify(uow => uow.Movies.Delete(It.IsAny<Movie>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_RemoveMovie_Should_Throw_Exception_When_MovieID_Not_Exists()
        {
            Movie movieOnDB = GetMovieToTest();
            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveByTitle(movieOnDB.Title)).Returns(movieOnDB);
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB.MovieID)).Returns(movieOnDB);

            Action action = () => _movieService.RemoveMovie(new Movie { MovieID = 15, Title = "NotExists" });
            action.Should().Throw<Exception>().WithMessage("Movie not found.");

            _unitOfWorkMock.Verify(uow => uow.Movies.Delete(It.IsAny<Movie>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_RemoveMovie_Should_Throw_Exception_When_MovieID_Is_Zero_Movie_Name_Not_Exists()
        {
            Movie movieOnDB = GetMovieToTest();
            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveByTitle(movieOnDB.Title)).Returns(movieOnDB);
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB.MovieID)).Returns(movieOnDB);

            Action action = () => _movieService.RemoveMovie(new Movie { MovieID = 0, Title = "NotExists" });
            action.Should().Throw<Exception>().WithMessage("Movie not found.");

            _unitOfWorkMock.Verify(uow => uow.Movies.Delete(It.IsAny<Movie>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_RemoveMovie_Should_Throw_ArgumentException_When_Movie_Parameter_Is_Null()
        {
            Action action = () => _movieService.RemoveMovie(null);
            action.Should().Throw<ArgumentException>().WithMessage("Movie parameter cannot be null. (Parameter 'movie')");

            _unitOfWorkMock.Verify(uow => uow.Movies.Delete(It.IsAny<Movie>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion RemoveMovie

        #region AddMovie
        [TestMethod]
        public void MovieService_AddMovie_Should_Add_New_Movie()
        {
            Movie movieOnDB = GetMovieToTest(1, "MovieTitleOnDb");
            string movieTitle = movieOnDB.Title;
            int dbCurrentID = movieOnDB.MovieID;

            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveByTitle(movieOnDB.Title)).Returns(movieOnDB);

            Movie movieToAdd = GetMovieToTest(dbCurrentID + 1, "NewMovie");
            _movieService.AddMovie(movieToAdd).Should().Be(dbCurrentID + 1);

            _unitOfWorkMock.Verify(uow => uow.Movies.Add(movieToAdd), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void MovieService_AddMovie_Should_Throw_ArgumentException_When_Movie_Parameter_Is_Null()
        {
            Action action = () => _movieService.AddMovie(null);
            action.Should().Throw<ArgumentException>().WithMessage("Movie parameter cannot be null. (Parameter 'movie')");

            _unitOfWorkMock.Verify(uow => uow.Movies.Add(It.IsAny<Movie>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_AddMovie_Should_Throw_Exception_When_Movie_Name_Is_Empty()
        {
            Movie movieToAdd = GetMovieToTest(0, string.Empty);
            Action action = () => _movieService.AddMovie(movieToAdd);
            action.Should().Throw<Exception>().WithMessage("Movie title cannot be null or empty.");

            _unitOfWorkMock.Verify(uow => uow.Movies.Add(It.IsAny<Movie>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_AddMovie_Should_Throw_Exception_When_Movie_Name_Is_Null()
        {
            Movie movieToAdd = GetMovieToTest(0, null);
            Action action = () => _movieService.AddMovie(movieToAdd);
            action.Should().Throw<Exception>().WithMessage("Movie title cannot be null or empty.");

            _unitOfWorkMock.Verify(uow => uow.Movies.Add(It.IsAny<Movie>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_AddMovie_Should_Throw_Exception_When_Movie_Title_To_Add_Already_Exists()
        {
            Movie movieOnDB = GetMovieToTest(0, "MovieTitle");
            string movieTitle = movieOnDB.Title;

            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveByTitle(movieOnDB.Title)).Returns(movieOnDB);

            Action action = () => _movieService.AddMovie(GetMovieToTest(0, movieTitle));
            action.Should().Throw<Exception>().WithMessage($"Already exists movie with title \"{movieTitle}\".");

            _unitOfWorkMock.Verify(uow => uow.Movies.Add(It.IsAny<Movie>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion AddMovie

        #region UpdateMovie
        [TestMethod]
        public void MovieService_UpdateMovie_Should_Update_Movie_Register()
        {
            Movie otherMovieOnDB = GetMovieToTest(1, "MovieOtherMovieTitle", "OtherDescriptoin");
            Movie movieOnBDToUpdate = GetMovieToTest(2, "OldMovieTitle", "OldDescriptoin");
            movieOnBDToUpdate.Poster = new byte[1024];

            Movie movieToUpdate = GetMovieToTest(2, "NewMovieTitle", "NewDescriptoin");

            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnBDToUpdate.MovieID)).Returns(movieOnBDToUpdate);
            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveByTitle(otherMovieOnDB.Title)).Returns(otherMovieOnDB);

            _movieService.UpdateMovie(movieToUpdate).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.Movies.Update(movieOnBDToUpdate), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Movies.Update(It.Is<Movie>(m => m.Title.Equals(movieToUpdate.Title) &&
                                                                              m.Description.Equals(movieToUpdate.Description) &&
                                                                              m.Minutes.Equals(movieToUpdate.Minutes) &&
                                                                              m.Poster != null &&
                                                                              m.Is3D.Equals(movieToUpdate.Is3D) &&
                                                                              m.IsOriginalAudio.Equals(movieToUpdate.IsOriginalAudio))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void MovieService_UpdateMovie_Should_Not_Update_Poster_Movie_Register()
        {
            Movie movieOnBDToUpdate = GetMovieToTest(2, "OldMovieTitle", "OldDescriptoin");
            movieOnBDToUpdate.Poster = new byte[1024];

            Movie movieToUpdate = GetMovieToTest(2, "NewMovieTitle", "NewDescriptoin");
            movieToUpdate.Poster = null;

            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnBDToUpdate.MovieID)).Returns(movieOnBDToUpdate);
            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveByTitle(movieOnBDToUpdate.Title));

            _movieService.UpdateMovie(movieToUpdate).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.Movies.Update(movieOnBDToUpdate), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Movies.Update(It.Is<Movie>(m => m.Poster != null)), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void MovieService_UpdateMovie_Should_Throw_Exception_When_Movie_ID_Not_Exists()
        {
            Movie movieOnDB = GetMovieToTest(1, "MovieTitle", "MovieDescription");
            Movie movieUpdated = GetMovieToTest(2, "NewMovieTitle", "NewMovieDescription");

            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB.MovieID)).Returns(movieOnDB);

            Action action = () => _movieService.UpdateMovie(movieUpdated);
            action.Should().Throw<Exception>().WithMessage("Movie to update not found.");

            _unitOfWorkMock.Verify(uow => uow.Movies.Update(It.IsAny<Movie>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_UpdateMovie_Should_Throw_Exception_When_Movie_Title_Exists_In_Other_Register()
        {
            Movie otherMovieOnDB = GetMovieToTest(1, "MovieTitle", "MovieDescription");
            Movie movieOnBDToUpdate = GetMovieToTest(2, "OldMovieTitle", "OldMovieDescription");
            Movie movieToUpdate = GetMovieToTest(2, "MovieTitle", "NewMovieDescription");

            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnBDToUpdate.MovieID)).Returns(movieOnBDToUpdate);
            _unitOfWorkMock.Setup(uow => uow.Movies.RetrieveByTitle(otherMovieOnDB.Title)).Returns(otherMovieOnDB);

            Action action = () => _movieService.UpdateMovie(movieToUpdate);
            action.Should().Throw<Exception>().WithMessage($"Already exists movie with title {movieToUpdate.Title}.");

            _unitOfWorkMock.Verify(uow => uow.Movies.Update(It.IsAny<Movie>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_UpdateMovie_Should_Throw_Exception_When_Movie_Title_Is_Empty()
        {
            Movie movieUpdated = GetMovieToTest(2, string.Empty);
            Action action = () => _movieService.UpdateMovie(movieUpdated);
            action.Should().Throw<Exception>().WithMessage("Movie title cannot be null or empty.");

            _unitOfWorkMock.Verify(uow => uow.Movies.Update(It.IsAny<Movie>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_UpdateMovie_Should_Throw_Exception_When_Movie_Title_Is_Null()
        {
            Movie movieUpdated = GetMovieToTest(2, null);
            Action action = () => _movieService.UpdateMovie(movieUpdated);
            action.Should().Throw<Exception>().WithMessage("Movie title cannot be null or empty.");

            _unitOfWorkMock.Verify(uow => uow.Movies.Update(It.IsAny<Movie>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_UpdateMovie_Should_Throw_ArgumentException_When_Movie_Parameter_Is_Null()
        {
            Action action = () => _movieService.UpdateMovie(null);
            action.Should().Throw<ArgumentException>().WithMessage("Movie parameter cannot be null. (Parameter 'movie')");

            _unitOfWorkMock.Verify(uow => uow.Movies.Update(It.IsAny<Movie>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion UpdateMovie

        #region MoviePoster
        [TestMethod]
        public void MovieService_UpdateMoviePoster_Should_Throw_Exception_When_Movie_ID_Not_Exists()
        {
            int movieID = 2;
            Stream posterStream = null;//Paser Stream is tested in IFileImageService

            Movie movieOnDB = GetMovieToTest(1, "MovieTitle", "MovieDescription");

            _fileImageServiceMock.Setup(ps => ps.GetImageBytes(It.IsAny<Stream>())).Returns(new byte[0]);
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB.MovieID)).Returns(movieOnDB);

            Action action = () => _movieService.UpdateMoviePoster(movieID, posterStream);

            action.Should().Throw<Exception>().WithMessage("Movie to update not found.");

            _fileImageServiceMock.Verify(ps => ps.GetImageBytes(It.IsAny<Stream>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Movies.Update(It.IsAny<Movie>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_UpdateMoviePoster_Should_Update_Movie_Poster_Property()
        {
            byte[] imageBytes = { 01, 02, 03 };
            int movieID = 1;
            Stream posterStream = null;//Paser Stream is tested in IFileImageService

            Movie movieOnDB = GetMovieToTest(1, "MovieTitle", "MovieDescription");

            _fileImageServiceMock.Setup(ps => ps.GetImageBytes(It.IsAny<Stream>())).Returns(imageBytes);
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB.MovieID)).Returns(movieOnDB);

            _movieService.UpdateMoviePoster(movieID, posterStream).Should().BeTrue();

            _fileImageServiceMock.Verify(ps => ps.GetImageBytes(posterStream), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Movies.Update(movieOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Movies.Update(It.Is<Movie>(m => m.Title.Equals(movieOnDB.Title) &&
                                                                              m.Description.Equals(movieOnDB.Description) &&
                                                                              m.Minutes.Equals(movieOnDB.Minutes) &&
                                                                              m.Poster == imageBytes &&
                                                                              m.Is3D.Equals(movieOnDB.Is3D) &&
                                                                              m.IsOriginalAudio.Equals(movieOnDB.IsOriginalAudio))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void MovieService_GetMoviePoster_Should_Throw_Exception_When_Movie_ID_Not_Exists()
        {
            int movieID = 2;

            Movie movieOnDB = GetMovieToTest(1, "MovieTitle", "MovieDescription");
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB.MovieID)).Returns(movieOnDB);

            Action action = () => _movieService.GetMoviePoster(movieID);
            action.Should().Throw<Exception>().WithMessage("Movie not found.");

            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_GetMoviePoster_Should_Return_Image_Bytes_From_DB_When_Movie_Has_Poster()
        {
            byte[] imageBytes = { 01, 02, 03 };

            int movieID = 1;

            Movie movieOnDB = GetMovieToTest(1, "MovieTitle", "MovieDescription");
            movieOnDB.Poster = imageBytes;

            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB.MovieID)).Returns(movieOnDB);

            byte[] bytesToReturn = _movieService.GetMoviePoster(movieID);

            bytesToReturn.Should().BeEquivalentTo(imageBytes);

            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_GetMoviePoster_Should_Return_Null_When_Movie_Does_Not_Have_Poster()
        {
            int movieID = 1;

            Movie movieOnDB = GetMovieToTest(1, "MovieTitle", "MovieDescription");
            movieOnDB.Poster = null;

            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB.MovieID)).Returns(movieOnDB);

            _movieService.GetMoviePoster(movieID).Should().BeNull();

            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_DeleteMoviePoster_Should_Throw_Exception_When_Movie_ID_Not_Exists()
        {
            int movieID = 2;

            Movie movieOnDB = GetMovieToTest(1, "MovieTitle", "MovieDescription");
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB.MovieID)).Returns(movieOnDB);

            Action action = () => _movieService.DeleteMoviePoster(movieID);
            action.Should().Throw<Exception>().WithMessage("Movie to update not found.");

            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void MovieService_DeleteMoviePoster_Should_Update_Movie_Poster_Property_To_Null()
        {
            int movieID = 1;

            Movie movieOnDB = GetMovieToTest(1, "MovieTitle", "MovieDescription");
            movieOnDB.Poster = new byte[] { 01, 02, 03 };

            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB.MovieID)).Returns(movieOnDB);
            _movieService.DeleteMoviePoster(movieID).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Movies.Update(movieOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Movies.Update(It.Is<Movie>(m => m.Title.Equals(movieOnDB.Title) &&
                                                                              m.Description.Equals(movieOnDB.Description) &&
                                                                              m.Minutes.Equals(movieOnDB.Minutes) &&
                                                                              m.Poster == null &&
                                                                              m.Is3D.Equals(movieOnDB.Is3D) &&
                                                                              m.IsOriginalAudio.Equals(movieOnDB.IsOriginalAudio))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }
        #endregion MoviePoster

        private Movie GetMovieToTest(int movieID = 1,
                                     string title = "Title",
                                     string description = "Description",
                                     int minutes = 90,
                                     bool is3D = false,
                                     bool IsOriginalAudio = false)
        {

            return new Movie
            {
                MovieID = movieID,
                Title = title,
                Description = description,
                Minutes = minutes,
                Is3D = is3D,
                IsOriginalAudio = IsOriginalAudio
            };
        }
    }
}
