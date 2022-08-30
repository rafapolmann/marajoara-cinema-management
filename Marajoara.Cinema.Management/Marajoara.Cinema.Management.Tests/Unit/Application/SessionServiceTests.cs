using FluentAssertions;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule;
using Marajoara.Cinema.Management.Application.Features.MovieModule;
using Marajoara.Cinema.Management.Application.Features.SessionModule;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marajoara.Cinema.Management.Tests.Unit.Application
{
    [TestClass]
    public class SessionServiceTests
    {
        private ISessionService _sessionService;
        private ICineRoomService _cineRoomService;
        private IMovieService _movieService;
        private Mock<IMarajoaraUnitOfWork> _unitOfWorkMock;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWorkMock = new Mock<IMarajoaraUnitOfWork>();
            _movieService = new MovieService(_unitOfWorkMock.Object);
            _cineRoomService = new CineRoomService(_unitOfWorkMock.Object);
            _sessionService = new SessionService(_unitOfWorkMock.Object, _cineRoomService, _movieService);
        }

        #region AddSession
        [TestMethod]
        public void SessionService_AddSession_Should_Add_New_Session()
        {
            CineRoom cineRoomToTest = GetCineRoomToTest();

            Movie movieToSession = GetMovieToTest(1, "MovieToAdd", "Movie001");
            DateTime sessionToAddDate = DateTime.Parse("2022/08/25 22:00:00");
            Session sessionToAdd = GetSessionToTest(0, cineRoomToTest, movieToSession, sessionToAddDate);

            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID)).Returns(cineRoomToTest);
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieToSession.MovieID)).Returns(movieToSession);
            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByDateAndCineRoom(It.IsAny<DateTime>(), It.IsAny<int>())).Returns(new List<Session>());

            _sessionService.AddSession(sessionToAdd);

            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieToSession.MovieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByDateAndCineRoom(It.Is<DateTime>(d => d.Day.Equals(sessionToAdd.SessionDate.Day) &&
                                                                                                      d.Month.Equals(sessionToAdd.SessionDate.Month) &&
                                                                                                      d.Year.Equals(sessionToAdd.SessionDate.Year)),
                                                                                                      sessionToAdd.CineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Add(sessionToAdd), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void SessionService_AddSession_Should_Throw_ArgumentException_When_Session_Parameter_Is_Null()
        {
            Action action = () => _sessionService.AddSession(null);
            action.Should().Throw<ArgumentException>().WithMessage("Session parameter cannot be null. (Parameter 'session')");

            _unitOfWorkMock.Verify(uow => uow.Sessions.Add(It.IsAny<Session>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_AddSession_Should_Throw_Exception_When_CineRoomID_Not_Exists()
        {
            int invalidCineRoomID = 100;
            Session sessionToAdd = GetSessionToTest(0, GetCineRoomToTest(invalidCineRoomID), GetMovieToTest(), DateTime.Now, 50);
            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(It.IsAny<int>()));

            Action action = () => _sessionService.AddSession(sessionToAdd);
            action.Should().Throw<Exception>().WithMessage($"Cine Room not found. CineRoomID: {invalidCineRoomID}");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Retrieve(invalidCineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Add(It.IsAny<Session>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_AddSession_Should_Throw_Exception_When_MovieID_Not_Exists()
        {
            int invalidMovieID = 100;
            CineRoom cineRoomToTest = GetCineRoomToTest();
            Session sessionToAdd = GetSessionToTest(0, cineRoomToTest, GetMovieToTest(invalidMovieID), DateTime.Now, 50);

            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID)).Returns(cineRoomToTest);
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(invalidMovieID));

            Action action = () => _sessionService.AddSession(sessionToAdd);
            action.Should().Throw<Exception>().WithMessage($"Movie not found. MovieID: {invalidMovieID}");

            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(invalidMovieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Add(It.IsAny<Session>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_AddSession_Should_Throw_Exception_When_Already_Exists_Other_Session_In_The_Same_CinRoom_At_The_Same_StarteDate_Time_Range()
        {
            CineRoom cineRoomToTest = GetCineRoomToTest();

            Movie movieToSession = GetMovieToTest(2, "MovieToAdd", "Movie001");
            DateTime sessionToAddDate = DateTime.Parse("2022/08/25 20:00:00");
            Session sessionToAdd = GetSessionToTest(0, cineRoomToTest, movieToSession, sessionToAddDate);

            Movie movieInExistingSession = GetMovieToTest(1, "MovieTitle", "OldMovie");
            DateTime exisitngSessionDate = DateTime.Parse("2022/08/25 19:30:00");
            Session existingSession = GetSessionToTest(0, cineRoomToTest, movieInExistingSession, exisitngSessionDate);

            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID)).Returns(cineRoomToTest);
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieToSession.MovieID)).Returns(movieToSession);
            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByDateAndCineRoom(It.Is<DateTime>(d => d.Day.Equals(exisitngSessionDate.Day) &&
                                                                                                     d.Month.Equals(exisitngSessionDate.Month) &&
                                                                                                     d.Year.Equals(exisitngSessionDate.Year)),
                                                                                                     cineRoomToTest.CineRoomID)).Returns(new List<Session> { existingSession });

            Action action = () => _sessionService.AddSession(sessionToAdd);
            action.Should()
                  .Throw<Exception>()
                  .WithMessage($"Already exists other session in the {cineRoomToTest.Name} at " +
                               $"{sessionToAdd.SessionDate.ToString("HH:mm:ss")} - {sessionToAdd.EndSession.ToString("HH:mm:ss")}");

            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieToSession.MovieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByDateAndCineRoom(It.Is<DateTime>(d => d.Day.Equals(sessionToAdd.SessionDate.Day) &&
                                                                                                      d.Month.Equals(sessionToAdd.SessionDate.Month) &&
                                                                                                      d.Year.Equals(sessionToAdd.SessionDate.Year)),
                                                                                                      sessionToAdd.CineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Add(It.IsAny<Session>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_AddSession_Should_Throw_Exception_When_Already_Exists_Other_Session_In_The_Same_CinRoom_At_The_Same_EndSessionDate_Time_Range()
        {
            CineRoom cineRoomToTest = GetCineRoomToTest();

            Movie movieToSession = GetMovieToTest(2, "MovieToAdd", "Movie001");
            DateTime sessionToAddDate = DateTime.Parse("2022/08/25 19:00:00");
            Session sessionToAdd = GetSessionToTest(0, cineRoomToTest, movieToSession, sessionToAddDate);

            Movie movieInExistingSession = GetMovieToTest(1, "MovieTitle", "OldMovie");
            DateTime exisitngSessionDate = DateTime.Parse("2022/08/25 19:30:00");
            Session existingSession = GetSessionToTest(1, cineRoomToTest, movieInExistingSession, exisitngSessionDate);

            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID)).Returns(cineRoomToTest);
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieToSession.MovieID)).Returns(movieToSession);
            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByDateAndCineRoom(It.Is<DateTime>(d => d.Day.Equals(exisitngSessionDate.Day) &&
                                                                                                     d.Month.Equals(exisitngSessionDate.Month) &&
                                                                                                     d.Year.Equals(exisitngSessionDate.Year)),
                                                                                                     cineRoomToTest.CineRoomID)).Returns(new List<Session> { existingSession });

            Action action = () => _sessionService.AddSession(sessionToAdd);
            action.Should()
                  .Throw<Exception>()
                  .WithMessage($"Already exists other session in the {cineRoomToTest.Name} at " +
                               $"{sessionToAdd.SessionDate.ToString("HH:mm:ss")} - {sessionToAdd.EndSession.ToString("HH:mm:ss")}");

            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieToSession.MovieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByDateAndCineRoom(It.Is<DateTime>(d => d.Day.Equals(sessionToAdd.SessionDate.Day) &&
                                                                                                      d.Month.Equals(sessionToAdd.SessionDate.Month) &&
                                                                                                      d.Year.Equals(sessionToAdd.SessionDate.Year)),
                                                                                                      sessionToAdd.CineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Add(It.IsAny<Session>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion AddSession

        #region UpdateSession
        [TestMethod]
        public void SessionService_UpdateSession_Should_Update_All_Session_Porperties()
        {
            CineRoom cineRoomOn01 = GetCineRoomToTest(1, "CineRoom01");
            CineRoom cineRoomOn02 = GetCineRoomToTest(2, "CineRoom02");

            Movie movieOnDB01 = GetMovieToTest(1, "Movie001", "Movie001");
            Movie movieOnDB02 = GetMovieToTest(1, "Movie002", "Movie002");

            DateTime sessionDateOnDB = DateTime.Parse("2022/08/01 20:00:00");
            Session sessionOnDB = GetSessionToTest(1, cineRoomOn01, movieOnDB01, sessionDateOnDB, 30);

            DateTime sessionDateToUpdate = DateTime.Parse("2022/08/20 18:00:00");
            Session sessionToUpdate = GetSessionToTest(1, cineRoomOn02, movieOnDB02, sessionDateToUpdate, 45);


            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(sessionOnDB.SessionID)).Returns(sessionOnDB);
            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomOn02.CineRoomID)).Returns(cineRoomOn02);
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieOnDB02.MovieID)).Returns(movieOnDB02);
            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByDateAndCineRoom(It.Is<DateTime>(d => d.Day.Equals(sessionDateOnDB.Day) &&
                                                                                                     d.Month.Equals(sessionDateOnDB.Month) &&
                                                                                                     d.Year.Equals(sessionDateOnDB.Year)),
                                                                                                     sessionOnDB.CineRoomID)).Returns(new List<Session> { sessionOnDB });

            _sessionService.UpdateSession(sessionToUpdate);

            _unitOfWorkMock.Verify(uow => uow.Sessions.Retrieve(sessionToUpdate.SessionID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(sessionToUpdate.MovieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CineRooms.Retrieve(sessionToUpdate.CineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByDateAndCineRoom(It.Is<DateTime>(d => d.Day.Equals(sessionToUpdate.SessionDate.Day) &&
                                                                                                      d.Month.Equals(sessionToUpdate.SessionDate.Month) &&
                                                                                                      d.Year.Equals(sessionToUpdate.SessionDate.Year)),
                                                                                                      sessionToUpdate.CineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Update(sessionOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Update(It.Is<Session>(s => s.SessionID.Equals(sessionToUpdate.SessionID) &&
                                                                                  s.MovieID.Equals(sessionToUpdate.MovieID) &&
                                                                                  s.CineRoomID.Equals(sessionToUpdate.CineRoomID) &&
                                                                                  s.SessionDate.Equals(sessionToUpdate.SessionDate) &&
                                                                                  s.Price.Equals(sessionToUpdate.Price))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void SessionService_UpdateSession_Should_Throw_ArgumentException_When_Session_Parameter_Is_Null()
        {
            Action action = () => _sessionService.UpdateSession(null);
            action.Should().Throw<ArgumentException>().WithMessage("Session parameter cannot be null. (Parameter 'session')");

            _unitOfWorkMock.Verify(uow => uow.Sessions.Add(It.IsAny<Session>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_UpdateSession_Should_Throw_Exception_When_SessionID_To_Update_Not_Exists()
        {
            int sessionIDToUpdate = 100;
            Session sessionToUpdate = GetSessionToTest(sessionIDToUpdate, GetCineRoomToTest(), GetMovieToTest(), DateTime.Now, 20);

            Session sessionOnDB = GetSessionToTest(1, GetCineRoomToTest(), GetMovieToTest(), DateTime.Now, 50);
            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(sessionOnDB.SessionID)).Returns(sessionOnDB);

            Action action = () => _sessionService.UpdateSession(sessionToUpdate);
            action.Should().Throw<Exception>().WithMessage($"Session to update not found.");

            _unitOfWorkMock.Verify(uow => uow.Sessions.Retrieve(sessionIDToUpdate), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Update(It.IsAny<Session>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_UpdateSession_Should_Throw_Exception_When_CineRoomID_Not_Exists()
        {
            int invalidCineRoomID = 100;
            Session sessionToUpdate = GetSessionToTest(1, GetCineRoomToTest(invalidCineRoomID), GetMovieToTest(), DateTime.Now, 20);

            CineRoom cineRoomToTest = GetCineRoomToTest();
            Movie movieToTest = GetMovieToTest();
            Session sessionOnDB = GetSessionToTest(1, cineRoomToTest, movieToTest, DateTime.Now, 50);
            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(sessionOnDB.SessionID)).Returns(sessionOnDB);
            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID)).Returns(cineRoomToTest);
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieToTest.MovieID)).Returns(movieToTest);

            Action action = () => _sessionService.UpdateSession(sessionToUpdate);
            action.Should().Throw<Exception>().WithMessage($"Cine room not found. CineRoomID: {invalidCineRoomID}");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Retrieve(invalidCineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieToTest.MovieID), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Update(It.IsAny<Session>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_UpdateSession_Should_Throw_Exception_When_MovieID_Not_Exists()
        {
            int invalidMovieID = 100;
            Session sessionToUpdate = GetSessionToTest(1, GetCineRoomToTest(), GetMovieToTest(invalidMovieID), DateTime.Now, 20);

            CineRoom cineRoomToTest = GetCineRoomToTest();
            Movie movieToTest = GetMovieToTest();
            Session sessionOnDB = GetSessionToTest(1, cineRoomToTest, movieToTest, DateTime.Now, 50);
            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(sessionOnDB.SessionID)).Returns(sessionOnDB);
            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID)).Returns(cineRoomToTest);
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieToTest.MovieID)).Returns(movieToTest);

            Action action = () => _sessionService.UpdateSession(sessionToUpdate);
            action.Should().Throw<Exception>().WithMessage($"Movie not found. MovieID: {invalidMovieID}");

            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(invalidMovieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Update(It.IsAny<Session>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_UpdateSession_Should_Throw_Exception_When_Already_Exists_Other_Session_In_The_Same_CinRoom_At_The_Same_StarteDate_Time_Range_And_SessionID_Is_Different()
        {
            CineRoom cineRoomToTest = GetCineRoomToTest();

            Movie movieToSession = GetMovieToTest(2, "MovieToAdd", "Movie001");
            DateTime sessionToUpdateDate = DateTime.Parse("2022/08/25 20:00:00");
            Session sessionToUpdate = GetSessionToTest(2, cineRoomToTest, movieToSession, sessionToUpdateDate);

            Movie movieToSessionOnDB = GetMovieToTest(2, "MovieToAdd", "Movie001");
            DateTime sessionDateOnDB = DateTime.Parse("2022/08/01 20:00:00");
            Session sessionOnDB = GetSessionToTest(2, cineRoomToTest, movieToSessionOnDB, sessionDateOnDB);

            Movie movieInExistingSession = GetMovieToTest(1, "MovieTitle", "OldMovie");
            DateTime exisitngSessionDate = DateTime.Parse("2022/08/25 19:30:00");
            Session existingSession = GetSessionToTest(1, cineRoomToTest, movieInExistingSession, exisitngSessionDate);

            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(sessionOnDB.SessionID)).Returns(sessionOnDB);
            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID)).Returns(cineRoomToTest);
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieToSession.MovieID)).Returns(movieToSession);
            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByDateAndCineRoom(It.Is<DateTime>(d => d.Day.Equals(exisitngSessionDate.Day) &&
                                                                                                     d.Month.Equals(exisitngSessionDate.Month) &&
                                                                                                     d.Year.Equals(exisitngSessionDate.Year)),
                                                                                                     cineRoomToTest.CineRoomID)).Returns(new List<Session> { existingSession });

            Action action = () => _sessionService.UpdateSession(sessionToUpdate);
            action.Should()
                  .Throw<Exception>()
                  .WithMessage($"Already exists other session in the {cineRoomToTest.Name} at " +
                               $"{sessionToUpdate.SessionDate.ToString("HH:mm:ss")} - {sessionToUpdate.EndSession.ToString("HH:mm:ss")}");

            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieToSession.MovieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByDateAndCineRoom(It.Is<DateTime>(d => d.Day.Equals(sessionToUpdate.SessionDate.Day) &&
                                                                                                      d.Month.Equals(sessionToUpdate.SessionDate.Month) &&
                                                                                                      d.Year.Equals(sessionToUpdate.SessionDate.Year)),
                                                                                                      sessionToUpdate.CineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Update(It.IsAny<Session>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_UpdateSession_Should_Throw_Exception_When_Already_Exists_Other_Session_In_The_Same_CinRoom_At_The_Same_EndSessionDate_Time_Range_And_SessionID_Is_Different()
        {
            CineRoom cineRoomToTest = GetCineRoomToTest();

            Movie movieToSession = GetMovieToTest(2, "MovieToAdd", "Movie001");
            DateTime sessionToUpdateDate = DateTime.Parse("2022/08/25 19:00:00");
            Session sessionToUpdate = GetSessionToTest(2, cineRoomToTest, movieToSession, sessionToUpdateDate);

            Movie movieToSessionOnDB = GetMovieToTest(2, "MovieToAdd", "Movie001");
            DateTime sessionDateOnDB = DateTime.Parse("2022/08/01 20:00:00");
            Session sessionOnDB = GetSessionToTest(2, cineRoomToTest, movieToSessionOnDB, sessionDateOnDB);

            Movie movieInExistingSession = GetMovieToTest(1, "MovieTitle", "OldMovie");
            DateTime exisitngSessionDate = DateTime.Parse("2022/08/25 19:30:00");
            Session existingSession = GetSessionToTest(1, cineRoomToTest, movieInExistingSession, exisitngSessionDate);

            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(sessionOnDB.SessionID)).Returns(sessionOnDB);
            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID)).Returns(cineRoomToTest);
            _unitOfWorkMock.Setup(uow => uow.Movies.Retrieve(movieToSession.MovieID)).Returns(movieToSession);
            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByDateAndCineRoom(It.Is<DateTime>(d => d.Day.Equals(exisitngSessionDate.Day) &&
                                                                                                     d.Month.Equals(exisitngSessionDate.Month) &&
                                                                                                     d.Year.Equals(exisitngSessionDate.Year)),
                                                                                                     cineRoomToTest.CineRoomID)).Returns(new List<Session> { existingSession });

            Action action = () => _sessionService.UpdateSession(sessionToUpdate);
            action.Should()
                  .Throw<Exception>()
                  .WithMessage($"Already exists other session in the {cineRoomToTest.Name} at " +
                               $"{sessionToUpdate.SessionDate.ToString("HH:mm:ss")} - {sessionToUpdate.EndSession.ToString("HH:mm:ss")}");

            _unitOfWorkMock.Verify(uow => uow.Movies.Retrieve(movieToSession.MovieID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CineRooms.Retrieve(cineRoomToTest.CineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByDateAndCineRoom(It.Is<DateTime>(d => d.Day.Equals(sessionToUpdate.SessionDate.Day) &&
                                                                                                      d.Month.Equals(sessionToUpdate.SessionDate.Month) &&
                                                                                                      d.Year.Equals(sessionToUpdate.SessionDate.Year)),
                                                                                                      sessionToUpdate.CineRoomID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Update(It.IsAny<Session>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion UpdateSession

        #region RemoveSession
        [TestMethod]
        public void SessionService_RemoveSession_Should_Remove_A_Given_Session_When_SessionID_Exists()
        {
            int sessionIdToDelete = 1;

            Session sessionOnDB = GetCompleteSessionToTest(sessionIdToDelete);
            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(sessionOnDB.SessionID)).Returns(sessionOnDB);

            _sessionService.RemoveSession(new Session { SessionID = sessionIdToDelete });

            _unitOfWorkMock.Verify(uow => uow.Sessions.Retrieve(sessionIdToDelete), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Delete(sessionOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void SessionService_RemoveSession_Should_Throw_Exception_When_Session_ID_Not_Exists()
        {
            int sessionIdToDelete = 15;

            Session sessionOnDB = GetCompleteSessionToTest(1);
            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(sessionOnDB.SessionID)).Returns(sessionOnDB);

            Action action = () => _sessionService.RemoveSession(new Session { SessionID = sessionIdToDelete });
            action.Should().Throw<Exception>().WithMessage("Session not found.");

            _unitOfWorkMock.Verify(uow => uow.Sessions.Retrieve(sessionIdToDelete), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Delete(It.IsAny<Session>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_RemoveSession_Should_Throw_ArgumentException_When_Session_Parameter_Is_Null()
        {
            Action action = () => _sessionService.RemoveSession(null);
            action.Should().Throw<ArgumentException>().WithMessage("Session parameter cannot be null. (Parameter 'session')");

            _unitOfWorkMock.Verify(uow => uow.Sessions.Delete(It.IsAny<Session>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion RemoveSession

        #region Gets_Session
        [TestMethod]
        public void SessionService_GetSessionsByDateRange_Should_Return_Sessions_In_A_Given_Date_Range()
        {
            DateTime initialDateToRetrieve = DateTime.Parse("2022/08/25 00:00:00");
            DateTime finalDateToRetrieve = DateTime.Parse("2022/08/27 00:00:00");

            CineRoom cineRoomToTest = GetCineRoomToTest();

            Movie sessionMovieToRetrive = GetMovieToTest(1);
            DateTime sessionDate01 = DateTime.Parse("2022/08/26 18:00:00");
            Session session01 = GetSessionToTest(1, cineRoomToTest, sessionMovieToRetrive, sessionDate01, 45);

            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByDate(It.Is<DateTime>(d => d <= sessionDate01),
                                                                     It.Is<DateTime>(d => d >= sessionDate01))).Returns(new List<Session> { session01 });

            List<Session> cineRoomSessions = _sessionService.GetSessionsByDateRange(initialDateToRetrieve, finalDateToRetrieve).ToList();

            cineRoomSessions.Should().NotBeNull();
            cineRoomSessions.Should().HaveCount(1);
            cineRoomSessions[0].SessionID.Should().Be(session01.SessionID);
            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByDate(initialDateToRetrieve, finalDateToRetrieve), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_GetSessionsByDateRange_Should_Return_Sessions_List_Empty_When_There_Are_No_Sessions_In_A_Given_Date_Range()
        {
            DateTime initialDateToRetrieve = DateTime.Parse("2022/08/01 00:00:00");
            DateTime finalDateToRetrieve = DateTime.Parse("2022/08/25 00:00:00");

            CineRoom cineRoomToTest = GetCineRoomToTest();

            Movie sessionMovieToRetrive = GetMovieToTest(1);
            DateTime sessionDate01 = DateTime.Parse("2022/08/26 18:00:00");
            Session session01 = GetSessionToTest(1, cineRoomToTest, sessionMovieToRetrive, sessionDate01, 45);

            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByDate(It.Is<DateTime>(d => d <= sessionDate01),
                                                                     It.Is<DateTime>(d => d >= sessionDate01))).Returns(new List<Session> { session01 });

            List<Session> cineRoomSessions = _sessionService.GetSessionsByDateRange(initialDateToRetrieve, finalDateToRetrieve).ToList();

            cineRoomSessions.Should().NotBeNull();
            cineRoomSessions.Should().BeEmpty();
            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByDate(initialDateToRetrieve, finalDateToRetrieve), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_GetSessionsByMovieTitle_Should_Return_Sessions_With_Movie_Title()
        {
            string movieTitleToRetrieve = "MovieToRetrieve";

            CineRoom cineRoomToTest = GetCineRoomToTest();

            Movie sessionMovieToRetrive = GetMovieToTest(1, movieTitleToRetrieve);
            DateTime sessionDate01 = DateTime.Parse("2022/08/25 18:00:00");
            Session session01 = GetSessionToTest(1, cineRoomToTest, sessionMovieToRetrive, sessionDate01, 45);

            Movie sessionMovie02 = GetMovieToTest(2, "OtherMovie");
            DateTime sessionDate02 = DateTime.Parse("2022/08/25 18:00:00");
            Session session02 = GetSessionToTest(2, cineRoomToTest, sessionMovie02, sessionDate02, 45);

            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByMovieTitle(movieTitleToRetrieve)).Returns(new List<Session> { session01 });
            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByMovieTitle("OtherMovie")).Returns(new List<Session> { session02 });

            List<Session> cineRoomSessions = _sessionService.GetSessionsByMovieTitle(movieTitleToRetrieve).ToList();

            cineRoomSessions.Should().NotBeNull();
            cineRoomSessions.Should().HaveCount(1);
            cineRoomSessions[0].Movie.Should().NotBeNull();
            cineRoomSessions[0].Movie.Title.Should().Be(movieTitleToRetrieve);

            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByMovieTitle(movieTitleToRetrieve), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_GetSessionsByMovieTitle_Should_Return_Session_List_Empty_When_Not_Exists_Movie_Title()
        {
            string movieTitleToRetrieve = "MovieToRetrieve";

            CineRoom cineRoomToTest = GetCineRoomToTest();

            Movie sessionMovieToRetrive = GetMovieToTest(1, "MovieTile");
            DateTime sessionDate01 = DateTime.Parse("2022/08/25 18:00:00");
            Session session01 = GetSessionToTest(1, cineRoomToTest, sessionMovieToRetrive, sessionDate01, 45);

            Movie sessionMovie02 = GetMovieToTest(2, "OtherMovie");
            DateTime sessionDate02 = DateTime.Parse("2022/08/25 18:00:00");
            Session session02 = GetSessionToTest(2, cineRoomToTest, sessionMovie02, sessionDate02, 45);

            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByMovieTitle("MovieTile")).Returns(new List<Session> { session01 });
            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByMovieTitle("OtherMovie")).Returns(new List<Session> { session02 });

            List<Session> cineRoomSessions = _sessionService.GetSessionsByMovieTitle(movieTitleToRetrieve).ToList();

            cineRoomSessions.Should().NotBeNull();
            cineRoomSessions.Should().BeEmpty();

            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByMovieTitle(movieTitleToRetrieve), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_GetSessionsByDate_Should_Return_Sessions_In_A_Given_Date()
        {
            DateTime dateToRetrieve = DateTime.Parse("2022/08/26 00:00:00");

            CineRoom cineRoomToTest = GetCineRoomToTest();

            Movie sessionMovieToRetrive = GetMovieToTest(1);
            DateTime sessionDate01 = DateTime.Parse("2022/08/26 18:00:00");
            Session session01 = GetSessionToTest(1, cineRoomToTest, sessionMovieToRetrive, sessionDate01, 45);

            Movie sessionMovie02 = GetMovieToTest(2, "OtherMovie");
            DateTime sessionDate02 = DateTime.Parse("2022/08/28 18:00:00");
            Session session02 = GetSessionToTest(2, cineRoomToTest, sessionMovie02, sessionDate02, 45);

            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByDate(It.Is<DateTime>(d => d.Year.Equals(sessionDate01.Year) &&
                                                                                          d.Month.Equals(sessionDate01.Month) &&
                                                                                          d.Day.Equals(sessionDate01.Day)))).Returns(new List<Session> { session01 });
            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByDate(It.Is<DateTime>(d => d.Year.Equals(sessionDate02.Year) &&
                                                                                          d.Month.Equals(sessionDate02.Month) &&
                                                                                          d.Day.Equals(sessionDate02.Day)))).Returns(new List<Session> { session02 });

            List<Session> cineRoomSessions = _sessionService.GetSessionsByDate(dateToRetrieve).ToList();

            cineRoomSessions.Should().NotBeNull();
            cineRoomSessions.Should().HaveCount(1);
            cineRoomSessions[0].SessionID.Should().Be(session01.SessionID);
            cineRoomSessions.Should().NotContain(s => s.SessionID.Equals(session02.SessionID));
            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByDate(dateToRetrieve), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_GetSessionsByDate_Should_Return_Session_List_Empty_When_Not_Exists_Sessions_In_A_Given_Date()
        {
            DateTime dateToRetrieve = DateTime.Parse("2022/08/29 00:00:00");

            CineRoom cineRoomToTest = GetCineRoomToTest();

            Movie sessionMovieToRetrive = GetMovieToTest(1);
            DateTime sessionDate01 = DateTime.Parse("2022/08/26 18:00:00");
            Session session01 = GetSessionToTest(1, cineRoomToTest, sessionMovieToRetrive, sessionDate01, 45);

            Movie sessionMovie02 = GetMovieToTest(2, "OtherMovie");
            DateTime sessionDate02 = DateTime.Parse("2022/08/28 18:00:00");
            Session session02 = GetSessionToTest(2, cineRoomToTest, sessionMovie02, sessionDate02, 45);

            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByDate(It.Is<DateTime>(d => d.Year.Equals(sessionDate01.Year) &&
                                                                                          d.Month.Equals(sessionDate01.Month) &&
                                                                                          d.Day.Equals(sessionDate01.Day)))).Returns(new List<Session> { session01 });
            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByDate(It.Is<DateTime>(d => d.Year.Equals(sessionDate02.Year) &&
                                                                                          d.Month.Equals(sessionDate02.Month) &&
                                                                                          d.Day.Equals(sessionDate02.Day)))).Returns(new List<Session> { session02 });

            List<Session> cineRoomSessions = _sessionService.GetSessionsByDate(dateToRetrieve).ToList();

            cineRoomSessions.Should().NotBeNull();
            cineRoomSessions.Should().BeEmpty();
            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByDate(dateToRetrieve), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_GetSessionByCineRoom_Should_Return_Sessions_With_A_Given_CineRoomID()
        {
            int cineRoomIDToRetrive = 3;
            CineRoom cineRoomToTest = GetCineRoomToTest(cineRoomIDToRetrive);

            Movie sessionMovie01 = GetMovieToTest();
            DateTime sessionDate01 = DateTime.Parse("2022/08/25 18:00:00");
            Session session01 = GetSessionToTest(1, cineRoomToTest, sessionMovie01, sessionDate01, 45);

            Movie sessionMovie02 = GetMovieToTest();
            DateTime sessionDate02 = DateTime.Parse("2022/08/25 18:00:00");
            Session session02 = GetSessionToTest(2, cineRoomToTest, sessionMovie02, sessionDate02, 45);

            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomIDToRetrive)).Returns(cineRoomToTest);
            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByCineRoom(It.Is<CineRoom>(c => c.CineRoomID.Equals(cineRoomIDToRetrive))))
                           .Returns(new List<Session> { session01, session02 });

            List<Session> cineRoomSessions = _sessionService.GetSessionsByCineRoom(cineRoomIDToRetrive).ToList();

            cineRoomSessions.Should().NotBeNull();
            cineRoomSessions.Should().HaveCount(2);
            cineRoomSessions[0].CineRoomID.Should().Be(cineRoomIDToRetrive);
            cineRoomSessions[1].CineRoomID.Should().Be(cineRoomIDToRetrive);
            _unitOfWorkMock.Verify(uow => uow.CineRooms.Retrieve(cineRoomIDToRetrive), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByCineRoom(cineRoomToTest), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_GetSessionByCineRoom_Should_Throw_Exception_When_CineRoomID_Not_Exists()
        {
            CineRoom cineRoomOnDB = GetCineRoomToTest();

            int cineRoomIDToRetrive = 3;
            CineRoom cineRoomToTest = GetCineRoomToTest(cineRoomIDToRetrive);

            Movie sessionMovie01 = GetMovieToTest();
            DateTime sessionDate01 = DateTime.Parse("2022/08/25 18:00:00");
            Session session01 = GetSessionToTest(1, cineRoomToTest, sessionMovie01, sessionDate01, 45);

            Movie sessionMovie02 = GetMovieToTest();
            DateTime sessionDate02 = DateTime.Parse("2022/08/25 18:00:00");
            Session session02 = GetSessionToTest(2, cineRoomToTest, sessionMovie02, sessionDate02, 45);

            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomOnDB.CineRoomID)).Returns(cineRoomOnDB);
            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveByCineRoom(It.Is<CineRoom>(c => c.CineRoomID.Equals(cineRoomIDToRetrive))))
                           .Returns(new List<Session> { session01, session02 });

            Action action = () => _sessionService.GetSessionsByCineRoom(cineRoomIDToRetrive);
            action.Should().Throw<Exception>().WithMessage($"Cine room not found. CineRoomID: {cineRoomIDToRetrive}");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Retrieve(cineRoomIDToRetrive), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveByCineRoom(cineRoomToTest), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_GetSession_Should_Return_Session_When_Session_ID_Exists()
        {
            int sessionIDToRetrive = 1;
            Session sessionOnDB = GetCompleteSessionToTest();

            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(sessionOnDB.SessionID)).Returns(sessionOnDB);

            _sessionService.GetSession(sessionIDToRetrive).Should().NotBeNull();
            _unitOfWorkMock.Verify(uow => uow.Sessions.Retrieve(sessionIDToRetrive), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_GetSession_Should_Return_Null_When_Session_ID_Not_Exists()
        {
            int sessionIDToRetrive = 3;
            Session sessionOnDB = GetCompleteSessionToTest(1);

            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(sessionOnDB.SessionID)).Returns(sessionOnDB);

            _sessionService.GetSession(sessionIDToRetrive).Should().BeNull();
            _unitOfWorkMock.Verify(uow => uow.Sessions.Retrieve(sessionIDToRetrive), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_GetAllSessions_Should_Return_All_Sessions()
        {
            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveAll()).Returns(new List<Session> { GetCompleteSessionToTest() });

            _sessionService.GetAllSessions().Should().HaveCount(1);
            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveAll(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void SessionService_GetAllSessions_Should_Return_Empty_Collection_When_There_Are_No_Sessions()
        {
            _unitOfWorkMock.Setup(uow => uow.Sessions.RetrieveAll()).Returns(new List<Session>());

            _sessionService.GetAllSessions().Should().BeEmpty();
            _unitOfWorkMock.Verify(uow => uow.Sessions.RetrieveAll(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion Gets_Session

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
