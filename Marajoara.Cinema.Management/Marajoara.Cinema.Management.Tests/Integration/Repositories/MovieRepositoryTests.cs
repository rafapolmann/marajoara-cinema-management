using FluentAssertions;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Tests.Integration.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Marajoara.Cinema.Management.Tests.Integration.Repositories
{
    [TestClass]
    public class MovieRepositoryTests : UnitOfWorkIntegrationBase
    {
        [TestInitialize]
        public void Initialize()
        {
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance();
        }

        [TestMethod]
        public void UnitOfWork_Should_Insert_Movie_On_Database()
        {
            Movie movieToAdd = GetMovieToTest();

            _marajoaraUnitOfWork.Movies.Add(movieToAdd);
            _marajoaraUnitOfWork.Commit();
            int movieID = movieToAdd.MovieID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Movie movieAdded = _marajoaraUnitOfWork.Movies.Retrieve(movieID);
            movieAdded.Should().NotBeNull();
            movieAdded.MovieID.Should().Be(movieID);
            movieAdded.Title.Should().Be("Title");
            movieAdded.Description.Should().Be("Description");
            movieAdded.Duration.TotalHours.Should().Be(1.5);
            movieAdded.Is3D.Should().BeFalse();
            movieAdded.IsOriginalAudio.Should().BeFalse();

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Delete_Existing_Movie_On_Database()
        {
            Movie movieToAdd = GetMovieToTest();

            _marajoaraUnitOfWork.Movies.Add(movieToAdd);
            _marajoaraUnitOfWork.Commit();
            int movieID = movieToAdd.MovieID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Movie movieToDelete = _marajoaraUnitOfWork.Movies.Retrieve(movieID);
            _marajoaraUnitOfWork.Movies.Delete(movieToDelete);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            _marajoaraUnitOfWork.Movies.Retrieve(movieID).Should().BeNull();
            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Update_All_Properties_Of_Existing_Movie_On_Database()
        {
            Movie movieToAdd = GetMovieToTest();
            _marajoaraUnitOfWork.Movies.Add(movieToAdd);
            _marajoaraUnitOfWork.Commit();

            int movieID = movieToAdd.MovieID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Movie movieToUpdate = _marajoaraUnitOfWork.Movies.Retrieve(movieID);
            movieToUpdate.Title = "TitleUpdated";
            movieToUpdate.Description = "DescriptionUpdated";
            movieToUpdate.Duration = new System.TimeSpan(2, 0, 0);
            movieToUpdate.Is3D = true;
            movieToUpdate.IsOriginalAudio = true;
            _marajoaraUnitOfWork.Movies.Update(movieToUpdate);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Movie movieToAssert = _marajoaraUnitOfWork.Movies.Retrieve(movieID);
            movieToAssert.Should().NotBeNull();
            movieToAssert.MovieID.Should().Be(movieID);
            movieToAssert.Title.Should().Be("TitleUpdated");
            movieToAssert.Description.Should().Be("DescriptionUpdated");
            movieToAssert.Duration.TotalHours.Should().Be(2.0);
            movieToAssert.Is3D.Should().BeTrue();
            movieToAssert.IsOriginalAudio.Should().BeTrue();

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_Persisted_Movie_On_Database_By_MovieID()
        {
            Movie movieToAdd = GetMovieToTest();

            _marajoaraUnitOfWork.Movies.Add(movieToAdd);
            _marajoaraUnitOfWork.Commit();
            int movieID = movieToAdd.MovieID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Movie movieToAssert = _marajoaraUnitOfWork.Movies.Retrieve(movieID);

            movieToAssert.Should().NotBeNull();
            movieToAssert.MovieID.Should().Be(movieID);
            movieToAssert.Title.Should().Be("Title");
            movieToAssert.Description.Should().Be("Description");
            movieToAssert.Duration.TotalHours.Should().Be(1.5);
            movieToAssert.Is3D.Should().BeFalse();
            movieToAssert.IsOriginalAudio.Should().BeFalse();

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_Persisted_Movie_On_Database_By_Name()
        {
            Movie movieToAdd = GetMovieToTest();
            string movieTitle = movieToAdd.Title;

            _marajoaraUnitOfWork.Movies.Add(movieToAdd);
            _marajoaraUnitOfWork.Commit();
            int movieID = movieToAdd.MovieID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Movie movieToAssert = _marajoaraUnitOfWork.Movies.RetrieveByTitle(movieTitle);

            movieToAssert.Should().NotBeNull();
            movieToAssert.MovieID.Should().Be(movieID);
            movieToAssert.Title.Should().Be("Title");
            movieToAssert.Description.Should().Be("Description");
            movieToAssert.Duration.TotalHours.Should().Be(1.5);
            movieToAssert.Is3D.Should().BeFalse();
            movieToAssert.IsOriginalAudio.Should().BeFalse();

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_Persisted_Movie_On_Database_By_Name_Case_Insensitive()
        {
            Movie movieToAdd = GetMovieToTest("Movie Title");
            string movieTitleToRetrive = "movie title";

            _marajoaraUnitOfWork.Movies.Add(movieToAdd);
            _marajoaraUnitOfWork.Commit();
            int movieID = movieToAdd.MovieID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Movie movieToAssert = _marajoaraUnitOfWork.Movies.RetrieveByTitle(movieTitleToRetrive);

            movieToAssert.Should().NotBeNull();
            movieToAssert.MovieID.Should().Be(movieID);
            movieToAssert.Title.Should().Be("Movie Title");
            movieToAssert.Description.Should().Be("Description");
            movieToAssert.Duration.TotalHours.Should().Be(1.5);
            movieToAssert.Is3D.Should().BeFalse();
            movieToAssert.IsOriginalAudio.Should().BeFalse();

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_All_Movies_From_Database()
        {
            Movie movieToAdd01 = GetMovieToTest("movie01");
            Movie movieToAdd02 = GetMovieToTest("movie02");
            Movie movieToAdd03 = GetMovieToTest("movie03");

            _marajoaraUnitOfWork.Movies.Add(movieToAdd01);
            _marajoaraUnitOfWork.Movies.Add(movieToAdd02);
            _marajoaraUnitOfWork.Movies.Add(movieToAdd03);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            List<Movie> allMoviesOnDB = _marajoaraUnitOfWork.Movies.RetrieveAll().ToList();
            allMoviesOnDB.Should().NotBeNullOrEmpty();
            allMoviesOnDB.Should().HaveCount(3);
            allMoviesOnDB.Find(us => us.Title.Equals("movie01")).Should().NotBeNull();
            allMoviesOnDB.Find(us => us.Title.Equals("movie02")).Should().NotBeNull();
            allMoviesOnDB.Find(us => us.Title.Equals("movie03")).Should().NotBeNull();

            _marajoaraUnitOfWork.Dispose();
        }
    }
}
