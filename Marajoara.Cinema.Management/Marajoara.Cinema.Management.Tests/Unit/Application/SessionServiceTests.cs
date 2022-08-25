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

        #region Gets_Session
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
