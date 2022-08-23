using FluentAssertions;
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
        private Mock<IMarajoaraUnitOfWork> _unitOfWorkMock;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWorkMock = new Mock<IMarajoaraUnitOfWork>();
            _sessionService = new SessionService(_unitOfWorkMock.Object);
        }

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
