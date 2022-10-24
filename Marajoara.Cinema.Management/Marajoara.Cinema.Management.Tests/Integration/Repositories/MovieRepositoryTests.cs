using FluentAssertions;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Tests.Integration.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            movieAdded.Minutes.Should().Be(90);
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
            movieToUpdate.Minutes = 120;
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
            movieToAssert.Minutes.Should().Be(120);
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
            movieToAssert.Minutes.Should().Be(90);
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
            movieToAssert.Minutes.Should().Be(90);
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
            movieToAssert.Minutes.Should().Be(90);
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


        [TestMethod]
        public void UnitOfWork_Should_Return_Movie_With_Session_In_Between_Dates_By_ID_From_Database()
        {
            DateTime baseDate = new DateTime(2022,1,1);
            CineRoom cineRoom = GetCineRoomToTest();
            Movie movieToAdd01 = GetMovieToTest("movie01");
            Movie movieToAdd02 = GetMovieToTest("movie02");
            Movie movieToAdd03 = GetMovieToTest("movie03");
            
            Session session1 = GetSessionToTest(cineRoom, movieToAdd01, baseDate.AddDays(-1));
            Session session2 = GetSessionToTest(cineRoom, movieToAdd01, baseDate.AddDays(1));
            Session session3 = GetSessionToTest(cineRoom, movieToAdd01, baseDate.AddDays(2));
            Session session4 = GetSessionToTest(cineRoom, movieToAdd02, baseDate.AddDays(1));

            Session session5 = GetSessionToTest(cineRoom, movieToAdd03, baseDate.AddDays(-2));

            _marajoaraUnitOfWork.Movies.Add(movieToAdd01);
            _marajoaraUnitOfWork.Movies.Add(movieToAdd02);
            _marajoaraUnitOfWork.Movies.Add(movieToAdd03);
            _marajoaraUnitOfWork.Sessions.Add(session1);
            _marajoaraUnitOfWork.Sessions.Add(session2);
            _marajoaraUnitOfWork.Sessions.Add(session3);
            _marajoaraUnitOfWork.Sessions.Add(session4);
            _marajoaraUnitOfWork.Sessions.Add(session5);

            _marajoaraUnitOfWork.Commit();
            var id = movieToAdd01.MovieID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Movie movieOnDB = _marajoaraUnitOfWork.Movies.RetrieveBySessionDate(id, baseDate, baseDate.AddDays(3));
            movieOnDB.Should().NotBeNull();
            movieOnDB.Title.Should().Be("movie01");
                        
            //Movie1 should have sessions 2 and 3
            var movie1Sessions = movieOnDB.Sessions.ToList();
            movie1Sessions.Should().NotBeNullOrEmpty();
            movie1Sessions.Should().HaveCount(2);
            movie1Sessions.Where(s => s.SessionDate < baseDate).Should().BeNullOrEmpty();
            
            _marajoaraUnitOfWork.Dispose();

        }

        [TestMethod]
        public void UnitOfWork_Should_Return_Movies_With_Session_In_Between_Dates_From_Database()
        {
            DateTime baseDate = new DateTime(2022, 1, 1);
            CineRoom cineRoom = GetCineRoomToTest();
            Movie movieToAdd01 = GetMovieToTest("movie01");
            Movie movieToAdd02 = GetMovieToTest("movie02");
            Movie movieToAdd03 = GetMovieToTest("movie03");

            Session session1 = GetSessionToTest(cineRoom, movieToAdd01, baseDate.AddDays(-1));
            Session session2 = GetSessionToTest(cineRoom, movieToAdd01, baseDate.AddDays(1));
            Session session3 = GetSessionToTest(cineRoom, movieToAdd01, baseDate.AddDays(2));
            Session session4 = GetSessionToTest(cineRoom, movieToAdd02, baseDate.AddDays(1));

            Session session5 = GetSessionToTest(cineRoom, movieToAdd03, baseDate.AddDays(-2));

            _marajoaraUnitOfWork.Movies.Add(movieToAdd01);
            _marajoaraUnitOfWork.Movies.Add(movieToAdd02);
            _marajoaraUnitOfWork.Movies.Add(movieToAdd03);
            _marajoaraUnitOfWork.Sessions.Add(session1);
            _marajoaraUnitOfWork.Sessions.Add(session2);
            _marajoaraUnitOfWork.Sessions.Add(session3);
            _marajoaraUnitOfWork.Sessions.Add(session4);
            _marajoaraUnitOfWork.Sessions.Add(session5);

            _marajoaraUnitOfWork.Commit();


            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            List<Movie> moviesOnDB = _marajoaraUnitOfWork.Movies.RetrieveBySessionDate(baseDate, baseDate.AddDays(3)).ToList();
            moviesOnDB.Should().NotBeNullOrEmpty();
            moviesOnDB.Should().HaveCount(2);

            //Should have Movie 1 and 2
            var movie1OnDB = moviesOnDB.Find(us => us.Title.Equals("movie01"));
            var movie2OnDB = moviesOnDB.Find(us => us.Title.Equals("movie02"));
            movie1OnDB.Should().NotBeNull();
            movie2OnDB.Should().NotBeNull();

            //Movie1 should have sessions 2 and 3
            var movie1Sessions = movie1OnDB.Sessions.ToList();
            movie1Sessions.Should().NotBeNullOrEmpty();
            movie1Sessions.Should().HaveCount(2);
            movie1Sessions.Where(s => s.SessionDate < baseDate).Should().BeNullOrEmpty();

            //Movie2 should have session 4

            var movie2Sessions = movie2OnDB.Sessions.ToList();
            movie2Sessions.Should().NotBeNullOrEmpty();
            movie2Sessions.Should().HaveCount(1);
            movie2Sessions.Where(s => s.SessionDate < baseDate).Should().BeNullOrEmpty();

            _marajoaraUnitOfWork.Dispose();

        }


        [TestMethod]
        public void UnitOfWork_Should_Return_Empty_When_There_Is_No_Session_In_Between_Dates_From_Database()
        {
            DateTime baseDate = new DateTime(2022, 1, 1);
            CineRoom cineRoom = GetCineRoomToTest();
            Movie movieToAdd01 = GetMovieToTest("movie01");
            Movie movieToAdd02 = GetMovieToTest("movie02");
            Movie movieToAdd03 = GetMovieToTest("movie03");

            Session session1 = GetSessionToTest(cineRoom, movieToAdd01, baseDate.AddDays(-1));
            Session session2 = GetSessionToTest(cineRoom, movieToAdd01, baseDate.AddDays(1));
            Session session3 = GetSessionToTest(cineRoom, movieToAdd01, baseDate.AddDays(2));
            Session session4 = GetSessionToTest(cineRoom, movieToAdd02, baseDate.AddDays(1));

            Session session5 = GetSessionToTest(cineRoom, movieToAdd03, baseDate.AddDays(-2));

            _marajoaraUnitOfWork.Movies.Add(movieToAdd01);
            _marajoaraUnitOfWork.Movies.Add(movieToAdd02);
            _marajoaraUnitOfWork.Movies.Add(movieToAdd03);
            _marajoaraUnitOfWork.Sessions.Add(session1);
            _marajoaraUnitOfWork.Sessions.Add(session2);
            _marajoaraUnitOfWork.Sessions.Add(session3);
            _marajoaraUnitOfWork.Sessions.Add(session4);
            _marajoaraUnitOfWork.Sessions.Add(session5);

            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            List<Movie> moviesOnDB = _marajoaraUnitOfWork.Movies.RetrieveBySessionDate(baseDate.AddDays(3), baseDate.AddDays(4)).ToList();
            moviesOnDB.Should().BeNullOrEmpty();
            moviesOnDB.Should().HaveCount(0);

            _marajoaraUnitOfWork.Dispose();

        }
    }
}
