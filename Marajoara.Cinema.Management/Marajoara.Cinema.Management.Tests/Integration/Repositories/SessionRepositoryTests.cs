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
    public class SessionRepositoryTests : UnitOfWorkIntegrationBase
    {
        [TestInitialize]
        public void Initialize()
        {
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance();
        }

        [TestMethod]
        public void UnitOfWork_Should_Insert_New_Session_On_Database()
        {
            CineRoom sessionCineRoom = GetCineRoomToTest();

            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom);
            _marajoaraUnitOfWork.Commit();
            int cineRoomID = sessionCineRoom.CineRoomID;

            Movie sessionMovie = GetMovieToTest();

            _marajoaraUnitOfWork.Movies.Add(sessionMovie);
            _marajoaraUnitOfWork.Commit();
            int movieID = sessionMovie.MovieID;

            DateTime sessionDate = DateTime.Parse("14:00:00 14/08/2022");
            Session sessionToAdd = GetSessionToTest(sessionCineRoom, sessionMovie, sessionDate);

            _marajoaraUnitOfWork.Sessions.Add(sessionToAdd);
            _marajoaraUnitOfWork.Commit();
            int sessionID = sessionToAdd.SessionID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Session sessionToAssert = _marajoaraUnitOfWork.Sessions.Retrieve(sessionID);

            sessionToAssert.Should().NotBeNull();
            sessionToAssert.SessionID.Should().Be(sessionID);
            sessionToAssert.Price.Should().Be(30);
            sessionToAssert.SessionDate.Should().Be(sessionDate);
            sessionToAssert.Movie.Should().NotBeNull();
            sessionToAssert.Movie.MovieID.Should().Be(movieID);
            sessionToAssert.CineRoom.Should().NotBeNull();
            sessionToAssert.CineRoom.CineRoomID.Should().Be(cineRoomID);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Update_All_Properties_Of_Existing_Session_On_Database()
        {
            CineRoom sessionCineRoom01 = GetCineRoomToTest("Sala01");
            CineRoom sessionCineRoom02 = GetCineRoomToTest("Sala02");

            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom01);
            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom02);
            _marajoaraUnitOfWork.Commit();
            int sessionCineRoomID02 = sessionCineRoom02.CineRoomID;

            Movie sessionMovie01 = GetMovieToTest("Movie01");
            Movie sessionMovie02 = GetMovieToTest("Movie02");

            _marajoaraUnitOfWork.Movies.Add(sessionMovie01);
            _marajoaraUnitOfWork.Movies.Add(sessionMovie02);
            _marajoaraUnitOfWork.Commit();
            int sessionMovieID02 = sessionMovie02.MovieID;

            DateTime sessionDate = DateTime.Parse("21:00:00 15/08/2022");
            Session session = GetSessionToTest(sessionCineRoom01, sessionMovie01, sessionDate);

            _marajoaraUnitOfWork.Sessions.Add(session);
            _marajoaraUnitOfWork.Commit();
            int sessionID = session.SessionID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Session sessionsToUpdate = _marajoaraUnitOfWork.Sessions.Retrieve(sessionID);
            DateTime newSessionDate = DateTime.Parse("22:30:00 25/09/2022");
            sessionsToUpdate.Price = 50;
            sessionsToUpdate.SessionDate = newSessionDate;
            sessionsToUpdate.Movie = _marajoaraUnitOfWork.Movies.Retrieve(sessionMovieID02);
            sessionsToUpdate.CineRoom = _marajoaraUnitOfWork.CineRooms.Retrieve(sessionCineRoomID02);

            _marajoaraUnitOfWork.Sessions.Update(sessionsToUpdate);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Session sessionToAssert = _marajoaraUnitOfWork.Sessions.Retrieve(sessionID);

            sessionToAssert.Should().NotBeNull();
            sessionToAssert.SessionID.Should().Be(sessionID);
            sessionToAssert.Price.Should().Be(50);
            sessionToAssert.SessionDate.Should().Be(newSessionDate);
            sessionToAssert.Movie.Should().NotBeNull();
            sessionToAssert.Movie.Title.Should().Be("Movie02");
            sessionToAssert.CineRoom.Should().NotBeNull();
            sessionToAssert.CineRoom.Name.Should().Be("Sala02");

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Delete_Existing_Session_On_Database()
        {
            CineRoom sessionCineRoom = GetCineRoomToTest();

            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom);
            _marajoaraUnitOfWork.Commit();

            Movie sessionMovie = GetMovieToTest();

            _marajoaraUnitOfWork.Movies.Add(sessionMovie);
            _marajoaraUnitOfWork.Commit();

            DateTime sessionDate = DateTime.Parse("14:00:00 14/08/2022");
            Session sessionToAdd = GetSessionToTest(sessionCineRoom, sessionMovie, sessionDate);

            _marajoaraUnitOfWork.Sessions.Add(sessionToAdd);
            _marajoaraUnitOfWork.Commit();
            int sessionID = sessionToAdd.SessionID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Session sessionToDelete = _marajoaraUnitOfWork.Sessions.Retrieve(sessionID);
            _marajoaraUnitOfWork.Sessions.Delete(sessionToDelete);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            _marajoaraUnitOfWork.Sessions.Retrieve(sessionID).Should().BeNull();
            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_Persisted_Session_On_Database_By_SessionID()
        {
            CineRoom sessionCineRoom = GetCineRoomToTest();

            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom);
            _marajoaraUnitOfWork.Commit();

            Movie sessionMovie = GetMovieToTest();

            _marajoaraUnitOfWork.Movies.Add(sessionMovie);
            _marajoaraUnitOfWork.Commit();

            DateTime sessionDate = DateTime.Parse("21:00:00 20/08/2022");
            Session sessionToAdd = GetSessionToTest(sessionCineRoom, sessionMovie, sessionDate);

            _marajoaraUnitOfWork.Sessions.Add(sessionToAdd);
            _marajoaraUnitOfWork.Commit();
            int sessionID = sessionToAdd.SessionID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Session sessionToAssert = _marajoaraUnitOfWork.Sessions.Retrieve(sessionID);

            sessionToAssert.Should().NotBeNull();
            sessionToAssert.SessionID.Should().Be(sessionID);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_List_Of_Sessions_On_Database_By_Date()
        {
            CineRoom sessionCineRoom01 = GetCineRoomToTest("Sala01");
            CineRoom sessionCineRoom02 = GetCineRoomToTest("Sala02");

            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom01);
            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom02);
            _marajoaraUnitOfWork.Commit();

            Movie sessionMovie01 = GetMovieToTest("Movie01");
            Movie sessionMovie02 = GetMovieToTest("Movie02");

            _marajoaraUnitOfWork.Movies.Add(sessionMovie01);
            _marajoaraUnitOfWork.Movies.Add(sessionMovie02);
            _marajoaraUnitOfWork.Commit();

            DateTime sessionDate01 = DateTime.Parse("21:00:00 20/08/2022");
            Session session01 = GetSessionToTest(sessionCineRoom01, sessionMovie01, sessionDate01);
            DateTime sessionDate02 = DateTime.Parse("14:00:00 19/08/2022");
            Session session02 = GetSessionToTest(sessionCineRoom02, sessionMovie02, sessionDate02);

            _marajoaraUnitOfWork.Sessions.Add(session01);
            _marajoaraUnitOfWork.Sessions.Add(session02);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            DateTime dateToRetrive = DateTime.Parse("21:00:00 20/08/2022");
            List<Session> sessionsToAssert = _marajoaraUnitOfWork.Sessions.RetrieveByDate(dateToRetrive).ToList();

            sessionsToAssert.Should().NotBeNull();
            sessionsToAssert.Should().HaveCount(1);
            sessionsToAssert[0].SessionID.Should().Be(session01.SessionID);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_List_Of_Sessions_On_Database_By_Date_Range()
        {
            CineRoom sessionCineRoom01 = GetCineRoomToTest("Sala01");
            CineRoom sessionCineRoom02 = GetCineRoomToTest("Sala02");

            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom01);
            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom02);
            _marajoaraUnitOfWork.Commit();

            Movie sessionMovie01 = GetMovieToTest("Movie01");
            Movie sessionMovie02 = GetMovieToTest("Movie02");

            _marajoaraUnitOfWork.Movies.Add(sessionMovie01);
            _marajoaraUnitOfWork.Movies.Add(sessionMovie02);
            _marajoaraUnitOfWork.Commit();

            DateTime sessionDate01 = DateTime.Parse("21:00:00 15/08/2022");
            Session session01 = GetSessionToTest(sessionCineRoom01, sessionMovie01, sessionDate01);
            DateTime sessionDate02 = DateTime.Parse("14:00:00 20/08/2022");
            Session session02 = GetSessionToTest(sessionCineRoom02, sessionMovie02, sessionDate02);
            DateTime sessionDate03 = DateTime.Parse("14:00:00 25/08/2022");
            Session session03 = GetSessionToTest(sessionCineRoom02, sessionMovie02, sessionDate03);

            _marajoaraUnitOfWork.Sessions.Add(session01);
            _marajoaraUnitOfWork.Sessions.Add(session02);
            _marajoaraUnitOfWork.Sessions.Add(session03);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            DateTime initialDate = DateTime.Parse("15/08/2022 00:00:00");
            DateTime finalDate = DateTime.Parse("20/08/2022 23:59:00");
            List<Session> sessionsToAssert = _marajoaraUnitOfWork.Sessions.RetrieveByDate(initialDate, finalDate).ToList();

            sessionsToAssert.Should().NotBeNull();
            sessionsToAssert.Should().HaveCount(2);
            sessionsToAssert.Should().Contain(s => s.SessionID.Equals(session01.SessionID));
            sessionsToAssert.Should().Contain(s => s.SessionID.Equals(session02.SessionID));
            sessionsToAssert.Should().NotContain(s => s.SessionID.Equals(session03.SessionID));

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_List_Of_All_Sessions_From_Database()
        {
            CineRoom sessionCineRoom01 = GetCineRoomToTest("Sala01");
            CineRoom sessionCineRoom02 = GetCineRoomToTest("Sala02");

            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom01);
            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom02);
            _marajoaraUnitOfWork.Commit();

            Movie sessionMovie01 = GetMovieToTest("Movie01");
            Movie sessionMovie02 = GetMovieToTest("Movie02");

            _marajoaraUnitOfWork.Movies.Add(sessionMovie01);
            _marajoaraUnitOfWork.Movies.Add(sessionMovie02);
            _marajoaraUnitOfWork.Commit();

            DateTime sessionDate01 = DateTime.Parse("21:00:00 20/08/2022");
            Session session01 = GetSessionToTest(sessionCineRoom01, sessionMovie01, sessionDate01);
            DateTime sessionDate02 = DateTime.Parse("14:00:00 20/08/2022");
            Session session02 = GetSessionToTest(sessionCineRoom02, sessionMovie02, sessionDate02);

            _marajoaraUnitOfWork.Sessions.Add(session01);
            _marajoaraUnitOfWork.Sessions.Add(session02);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            List<Session> sessionsToAssert = _marajoaraUnitOfWork.Sessions.RetrieveAll().ToList();

            sessionsToAssert.Should().NotBeNull();
            sessionsToAssert.Should().HaveCount(2);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_List_Of_Sessions_Of_A_Given_Movie_Retrived_By_Movie_Title_From_Database()
        {
            string movieTitle = "MovieTitle";
            Movie sessionsMovie = GetMovieToTest(movieTitle);

            string otherMovieTitle = "OtherMovieTitle";
            Movie otherSessionsMovie = GetMovieToTest(otherMovieTitle);

            _marajoaraUnitOfWork.Movies.Add(sessionsMovie);
            _marajoaraUnitOfWork.Movies.Add(otherSessionsMovie);
            _marajoaraUnitOfWork.Commit();
            int movieID = sessionsMovie.MovieID;

            CineRoom sessionCineRoom01 = GetCineRoomToTest("Sala01");
            CineRoom sessionCineRoom02 = GetCineRoomToTest("Sala02");
            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom01);
            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom02);
            _marajoaraUnitOfWork.Commit();

            DateTime sessionDate01 = DateTime.Parse("14:00:00 14/08/2022");
            Session session01 = GetSessionToTest(sessionCineRoom01, sessionsMovie, sessionDate01);

            DateTime sessionDate02 = DateTime.Parse("20:00:00 15/08/2022");
            Session session02 = GetSessionToTest(sessionCineRoom01, sessionsMovie, sessionDate02);

            DateTime sessionWithOtherMovieDate = DateTime.Parse("21:00:00 15/08/2022");
            Session sessionWithOtherMovie = GetSessionToTest(sessionCineRoom02, otherSessionsMovie, sessionWithOtherMovieDate);

            _marajoaraUnitOfWork.Sessions.Add(session01);
            _marajoaraUnitOfWork.Sessions.Add(session02);
            _marajoaraUnitOfWork.Sessions.Add(sessionWithOtherMovie);
            _marajoaraUnitOfWork.Commit();
            int sessionWithOtherMovieID = sessionWithOtherMovie.SessionID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            List<Session> movieSessions = _marajoaraUnitOfWork.Sessions.RetrieveByMovieTitle(movieTitle).ToList();

            movieSessions.Should().NotBeNull();
            movieSessions.Should().HaveCount(2);
            movieSessions[0].MovieID.Should().Be(movieID);
            movieSessions[0].Movie.Should().NotBeNull();
            movieSessions[0].Movie.Title.Should().Be(movieTitle);
            movieSessions[1].MovieID.Should().Be(movieID);
            movieSessions[1].Movie.Should().NotBeNull();
            movieSessions[1].Movie.Title.Should().Be(movieTitle);
            movieSessions.Should().NotContain(s => s.SessionID.Equals(sessionWithOtherMovieID));

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_List_Of_Sessions_Of_A_Given_Movie_Retrived_By_Movie_Title_From_Database_Case_Insensitive()
        {
            string movieTitle = "MovieTitle";
            string movieTitleToRetrive = "movietitle";
            Movie sessionsMovie = GetMovieToTest(movieTitle);

            string otherMovieTitle = "OtherMovieTitle";
            Movie otherSessionsMovie = GetMovieToTest(otherMovieTitle);

            _marajoaraUnitOfWork.Movies.Add(sessionsMovie);
            _marajoaraUnitOfWork.Movies.Add(otherSessionsMovie);
            _marajoaraUnitOfWork.Commit();
            int movieID = sessionsMovie.MovieID;

            CineRoom sessionCineRoom01 = GetCineRoomToTest("Sala01");
            CineRoom sessionCineRoom02 = GetCineRoomToTest("Sala02");
            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom01);
            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom02);
            _marajoaraUnitOfWork.Commit();

            DateTime sessionDate01 = DateTime.Parse("14:00:00 14/08/2022");
            Session session01 = GetSessionToTest(sessionCineRoom01, sessionsMovie, sessionDate01);

            DateTime sessionDate02 = DateTime.Parse("20:00:00 15/08/2022");
            Session session02 = GetSessionToTest(sessionCineRoom01, sessionsMovie, sessionDate02);

            DateTime sessionWithOtherMovieDate = DateTime.Parse("21:00:00 15/08/2022");
            Session sessionWithOtherMovie = GetSessionToTest(sessionCineRoom02, otherSessionsMovie, sessionWithOtherMovieDate);

            _marajoaraUnitOfWork.Sessions.Add(session01);
            _marajoaraUnitOfWork.Sessions.Add(session02);
            _marajoaraUnitOfWork.Sessions.Add(sessionWithOtherMovie);
            _marajoaraUnitOfWork.Commit();
            int sessionWithOtherMovieID = sessionWithOtherMovie.SessionID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            List<Session> movieSessions = _marajoaraUnitOfWork.Sessions.RetrieveByMovieTitle(movieTitleToRetrive).ToList();

            movieSessions.Should().NotBeNull();
            movieSessions.Should().HaveCount(2);
            movieSessions[0].MovieID.Should().Be(movieID);
            movieSessions[0].Movie.Should().NotBeNull();
            movieSessions[0].Movie.Title.Should().Be(movieTitle);
            movieSessions[1].MovieID.Should().Be(movieID);
            movieSessions[1].Movie.Should().NotBeNull();
            movieSessions[1].Movie.Title.Should().Be(movieTitle);
            movieSessions.Should().NotContain(s => s.SessionID.Equals(sessionWithOtherMovieID));

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_List_Of_Sessions_Of_A_Given_CineRoom_Retrived_By_CineRoom()
        {
            string movieTitle = "MovieTitle";
            Movie sessionsMovie = GetMovieToTest(movieTitle);

            string otherMovieTitle = "OtherMovieTitle";
            Movie otherSessionsMovie = GetMovieToTest(otherMovieTitle);

            _marajoaraUnitOfWork.Movies.Add(sessionsMovie);
            _marajoaraUnitOfWork.Movies.Add(otherSessionsMovie);
            _marajoaraUnitOfWork.Commit();

            CineRoom sessionCineRoom = GetCineRoomToTest("Sala01");
            CineRoom otherSessionCineRoom = GetCineRoomToTest("OtherCineRoom");
            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom);
            _marajoaraUnitOfWork.CineRooms.Add(otherSessionCineRoom);
            _marajoaraUnitOfWork.Commit();
            int sessionCineRoomID = sessionCineRoom.CineRoomID;
            int otherSessionCineRoomID = otherSessionCineRoom.CineRoomID;

            DateTime sessionDate01 = DateTime.Parse("14:00:00 14/08/2022");
            Session session01 = GetSessionToTest(sessionCineRoom, sessionsMovie, sessionDate01);

            DateTime sessionDate02 = DateTime.Parse("20:00:00 15/08/2022");
            Session session02 = GetSessionToTest(sessionCineRoom, sessionsMovie, sessionDate02);

            DateTime sessionWithOtherMovieDate = DateTime.Parse("21:00:00 15/08/2022");
            Session sessionWithOtherCineRoom = GetSessionToTest(otherSessionCineRoom, otherSessionsMovie, sessionWithOtherMovieDate);

            _marajoaraUnitOfWork.Sessions.Add(session01);
            _marajoaraUnitOfWork.Sessions.Add(session02);
            _marajoaraUnitOfWork.Sessions.Add(sessionWithOtherCineRoom);
            _marajoaraUnitOfWork.Commit();
            int sessionWithOtherMovieID = sessionWithOtherCineRoom.SessionID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            List<Session> cineRoomSessions = _marajoaraUnitOfWork.Sessions.RetrieveByCineRoom(sessionCineRoom).ToList();

            cineRoomSessions.Should().NotBeNull();
            cineRoomSessions.Should().HaveCount(2);
            cineRoomSessions[0].CineRoomID.Should().Be(sessionCineRoomID);
            cineRoomSessions[0].CineRoom.Should().NotBeNull();
            cineRoomSessions[1].CineRoomID.Should().Be(sessionCineRoomID);
            cineRoomSessions[1].CineRoom.Should().NotBeNull();
            cineRoomSessions.Should().NotContain(s => s.SessionID.Equals(sessionWithOtherMovieID));

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_List_Of_Sessions_Of_A_Given_CineRoomID_And_In_Specific_Date()
        {
            string movieTitle = "MovieTitle";
            Movie sessionsMovie = GetMovieToTest(movieTitle);

            string otherMovieTitle = "OtherMovieTitle";
            Movie otherSessionsMovie = GetMovieToTest(otherMovieTitle);

            _marajoaraUnitOfWork.Movies.Add(sessionsMovie);
            _marajoaraUnitOfWork.Movies.Add(otherSessionsMovie);
            _marajoaraUnitOfWork.Commit();

            CineRoom sessionCineRoom = GetCineRoomToTest("Sala01");
            CineRoom otherSessionCineRoom = GetCineRoomToTest("OtherCineRoom");
            _marajoaraUnitOfWork.CineRooms.Add(sessionCineRoom);
            _marajoaraUnitOfWork.CineRooms.Add(otherSessionCineRoom);
            _marajoaraUnitOfWork.Commit();
            int sessionCineRoomID = sessionCineRoom.CineRoomID;
            int otherSessionCineRoomID = otherSessionCineRoom.CineRoomID;

            DateTime sessionDate01 = DateTime.Parse("14:00:00 14/08/2022");
            Session session01 = GetSessionToTest(sessionCineRoom, sessionsMovie, sessionDate01);

            DateTime sessionDate02 = DateTime.Parse("20:00:00 15/08/2022");
            Session session02 = GetSessionToTest(sessionCineRoom, sessionsMovie, sessionDate02);

            DateTime sessionWithOtherMovieDate = DateTime.Parse("21:00:00 15/08/2022");
            Session sessionWithOtherCineRoom = GetSessionToTest(otherSessionCineRoom, otherSessionsMovie, sessionWithOtherMovieDate);

            _marajoaraUnitOfWork.Sessions.Add(session01);
            _marajoaraUnitOfWork.Sessions.Add(session02);
            _marajoaraUnitOfWork.Sessions.Add(sessionWithOtherCineRoom);
            _marajoaraUnitOfWork.Commit();
            int sessionWithOtherCineRoomID = sessionWithOtherCineRoom.SessionID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            DateTime dateToRetirve = DateTime.Parse("2022/08/14 00:00:00");
            List<Session> sessions = _marajoaraUnitOfWork.Sessions.RetrieveByDateAndCineRoom(dateToRetirve, sessionCineRoomID).ToList();

            sessions.Should().NotBeNull();
            sessions.Should().HaveCount(1);
            sessions[0].CineRoomID.Should().Be(sessionCineRoomID);
            sessions[0].CineRoom.Should().NotBeNull();

            sessions.Should().NotContain(s => s.SessionID.Equals(session02.SessionID));
            sessions.Should().NotContain(s => s.SessionID.Equals(sessionWithOtherCineRoomID));

            _marajoaraUnitOfWork.Dispose();
        }
    }
}
