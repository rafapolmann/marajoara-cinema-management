using FluentAssertions;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Marajoara.Cinema.Management.Tests.Unit.Domain
{
    [TestClass]
    public class SessionTests
    {
        #region Properties
        [TestMethod]
        public void Session_Set_Movie_Should_Set_MovieID_Porperty()
        {
            Movie movieToTest = GetMovieToTest(10);
            Session session = new Session();
            session.Movie = movieToTest;

            session.MovieID.Should().Be(movieToTest.MovieID);
        }

        [TestMethod]
        public void Session_Set_Movie_To_Null_Should_Set_MovieID_Porperty_To_Zero()
        {
            Session session = GetCompleteSessionToTest();
            session.Movie = null;

            session.MovieID.Should().Be(0);
        }

        [TestMethod]
        public void Session_Set_CineRoom_Should_Set_CineRoomID_Porperty()
        {
            CineRoom cineRoomToTest = GetCineRoomToTest(10);
            Session session = new Session();
            session.CineRoom = cineRoomToTest;

            session.CineRoomID.Should().Be(cineRoomToTest.CineRoomID);
        }

        [TestMethod]
        public void Session_Set_CineRoom_To_Null_Should_Set_CineRoomID_Porperty_To_Zero()
        {
            Session session = GetCompleteSessionToTest();
            session.CineRoom = null;

            session.CineRoomID.Should().Be(0);
        }
        #endregion Properties

        #region CopyTo
        [TestMethod]
        public void Session_CopyTo_Should_Copy_All_Properties_Except_SessionID()
        {
            int originalID = 3;

            Session session = GetCompleteSessionToTest();
            Session sessionToCopy = new Session
            {
                SessionID = originalID
            };

            session.CopyTo(sessionToCopy);

            sessionToCopy.SessionID.Should().Be(originalID);
            sessionToCopy.Price.Should().Be(session.Price);
            sessionToCopy.SessionDate.Should().Be(session.SessionDate);
            sessionToCopy.MovieID.Should().Be(session.MovieID);
            sessionToCopy.Movie.Should().NotBeNull();
            sessionToCopy.Movie.Should().Be(session.Movie);
            sessionToCopy.CineRoomID.Should().Be(session.CineRoomID);
            sessionToCopy.CineRoom.Should().NotBeNull();
            sessionToCopy.CineRoom.Should().Be(session.CineRoom);           
        }

        [TestMethod]
        public void Session_CopyTo_Should_Not_Copy_SessionID()
        {
            int originalID = 3;

            Session session = GetCompleteSessionToTest();
            Session sessionToCopy = GetCompleteSessionToTest(originalID);

            session.CopyTo(sessionToCopy);

            sessionToCopy.SessionID.Should().NotBe(session.SessionID);
            sessionToCopy.SessionID.Should().Be(originalID);
        }

        [TestMethod]
        public void Session_CopyTo_Should_Should_Throw_ArgumentException_When_SessionToCopy_Is_The_Same_Instance()
        {
            Session session = GetCompleteSessionToTest();

            Action action = () => session.CopyTo(session);
            action.Should().Throw<ArgumentException>().WithMessage("Session to copy cannot be the same instance of the origin. (Parameter 'sessionToCopy')");
        }

        [TestMethod]
        public void Session_CopyTo_Should_Should_Throw_ArgumentException_When_SessionToCopy_Is_Null()
        {
            Session session = GetCompleteSessionToTest();

            Action action = () => session.CopyTo(null);
            action.Should().Throw<ArgumentException>().WithMessage("Session parameter cannot be null. (Parameter 'sessionToCopy')");
        }
        #endregion CopyTo

        private CineRoom GetCineRoomToTest(int cineRoomID = 1,
                                   string name = "CineRoomName",
                                   int seatsColumn = 5,
                                   int seatsRow = 10)
        {
            return new CineRoom
            {
                CineRoomID = cineRoomID,
                Name = name,
                SeatsColumn = seatsColumn,
                SeatsRow = seatsRow
            };
        }


        private Movie GetMovieToTest(int movieID = 1,
                                     string title = "Title",
                                     string description = "Description",
                                     bool is3D = false,
                                     bool IsOriginalAudio = false)
        {

            return new Movie
            {
                MovieID = movieID,
                Title = title,
                Description = description,
                Duration = new TimeSpan(1, 30, 0),
                Is3D = is3D,
                IsOriginalAudio = IsOriginalAudio
            };
        }


        private Session GetSessionToTest(int sessionID,
                                         CineRoom cineRoom,
                                         Movie movie,
                                         DateTime sessionDate,
                                         decimal price = 30)
        {
            return new Session
            {
                SessionID = sessionID,
                SessionDate = sessionDate,
                Price = price,
                CineRoom = cineRoom,
                Movie = movie
            };
        }

        private Session GetCompleteSessionToTest(int sessionID = 1, decimal price = 30)
        {
            return GetSessionToTest(sessionID, GetCineRoomToTest(), GetMovieToTest(), DateTime.Now, price);
        }
    }
}
