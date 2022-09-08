using FluentAssertions;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule;
using Marajoara.Cinema.Management.Application.Features.MovieModule;
using Marajoara.Cinema.Management.Application.Features.SessionModule;
using Marajoara.Cinema.Management.Application.Features.TicketModule;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Tests.Unit.Application
{
    [TestClass]
    public class TicketServiceTests
    {
        private ITicketService _ticketService;
        private ISessionService _sessionService;
        private IUserAccountService _userAccountService;
        private ICineRoomService _cineRoomService;
        private IMovieService _movieService;
        private Mock<IMarajoaraUnitOfWork> _unitOfWorkMock;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWorkMock = new Mock<IMarajoaraUnitOfWork>();
            _userAccountService = new UserAccountService(_unitOfWorkMock.Object);
            _movieService = new MovieService(_unitOfWorkMock.Object);
            _cineRoomService = new CineRoomService(_unitOfWorkMock.Object);
            _sessionService = new SessionService(_unitOfWorkMock.Object, _cineRoomService, _movieService);
            _ticketService = new TicketService(_unitOfWorkMock.Object, _sessionService, _userAccountService);
        }

        #region AddTicket

        [TestMethod]
        public void TicketService_AddTicket_Should_Add_New_Ticket()
        {
            CineRoom sessionCineRoom = GetCineRoomToTest();
            Movie sessionMovie = GetMovieToTest();
            Session ticketSession = GetCompleteSessionToTest();
            UserAccount ticketUserAccount = GetUserAccountToTest();
            DateTime purchaseDate = DateTime.Now;

            Ticket ticketToTest = GetTicketToTest(1, purchaseDate, Guid.NewGuid(), 30, 1, false, ticketSession, ticketUserAccount);

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(ticketUserAccount.UserAccountID)).Returns(ticketUserAccount);
            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(ticketSession.SessionID)).Returns(ticketSession);
            _unitOfWorkMock.Setup(uow => uow.Tickets.RetrieveAll()).Returns(new List<Ticket>());
            int addedTicketId = _ticketService.AddTicket(ticketToTest);

            addedTicketId.Should().Be(ticketToTest.TicketID);

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(ticketUserAccount.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Retrieve(ticketSession.SessionID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Tickets.RetrieveAll(), Times.Once);

            _unitOfWorkMock.Verify(uow => uow.Tickets.Add(ticketToTest), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);

        }

        [TestMethod]
        public void TicketService_AddTicket_Should_Throw_ArgumentException_When_arameter_Is_Null()
        {
            Action action = () => _ticketService.AddTicket(null);
            action.Should().Throw<ArgumentException>().WithMessage("Ticket parameter cannot be null. (Parameter 'Ticket')");

            _unitOfWorkMock.Verify(uow => uow.Tickets.Add(It.IsAny<Ticket>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void TicketService_AddTicket_Should_Throw_Exception_When_SessionID_Not_Exists()
        {
            int invalidSessionID = 100;
            Ticket ticketToTest = GetTicketToTest(0, DateTime.Now, Guid.NewGuid(), 10, 1, false, GetCompleteSessionToTest(invalidSessionID), GetUserAccountToTest());
            //Session sessionToAdd = GetSessionToTest(0, GetCineRoomToTest(invalidCineRoomID), GetMovieToTest(), DateTime.Now, 50);
            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(It.IsAny<int>()));

            Action action = () => _ticketService.AddTicket(ticketToTest);
            action.Should().Throw<Exception>().WithMessage($"Session not found. SessionID: {ticketToTest.SessionID}");

            _unitOfWorkMock.Verify(uow => uow.Sessions.Retrieve(invalidSessionID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Tickets.Add(It.IsAny<Ticket>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void TicketService_AddTicket_Should_Throw_Exception_When_UserAccountID_Not_Exists()
        {
            int invalidUserAccountID = 100;
            Session ticketSession = GetCompleteSessionToTest();
            Ticket ticketToTest = GetTicketToTest(0, DateTime.Now, Guid.NewGuid(), 10, 1, false, ticketSession, GetUserAccountToTest(invalidUserAccountID));
            //Session sessionToAdd = GetSessionToTest(0, GetCineRoomToTest(invalidCineRoomID), GetMovieToTest(), DateTime.Now, 50);
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(It.IsAny<int>()));
            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(ticketSession.SessionID)).Returns(ticketSession);

            Action action = () => _ticketService.AddTicket(ticketToTest);
            action.Should().Throw<Exception>().WithMessage($"User Account not found. UserAccountID: {ticketToTest.UserAccountID}");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(invalidUserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Tickets.Add(It.IsAny<Ticket>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void TicketService_AddTicket_Should_Throw_Exception_When_Seat_Is_Taken()
        {
            int seatNumber = 1;
            Session ticketSession = GetCompleteSessionToTest();

            Ticket existingTicket = GetTicketToTest(1, DateTime.Now, Guid.NewGuid(), 10, seatNumber, false, ticketSession, GetUserAccountToTest(2, "another user"));

            UserAccount ticketUserAccount = GetUserAccountToTest();
            Ticket ticketToTest = GetTicketToTest(0, DateTime.Now, Guid.NewGuid(), 10, seatNumber, false, ticketSession, ticketUserAccount);

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(ticketUserAccount.UserAccountID)).Returns(ticketUserAccount);
            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(ticketSession.SessionID)).Returns(ticketSession);
            _unitOfWorkMock.Setup(uow => uow.Tickets.RetrieveAll()).Returns(new List<Ticket>() { existingTicket });


            Action action = () => _ticketService.AddTicket(ticketToTest);
            action.Should().Throw<Exception>().WithMessage($"Already exists a ticket for the seat number {existingTicket.SeatNumber} ");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(ticketUserAccount.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Retrieve(ticketSession.SessionID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Tickets.RetrieveAll(), Times.Once);

            _unitOfWorkMock.Verify(uow => uow.Tickets.Add(It.IsAny<Ticket>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void TicketService_AddTicket_Should_Throw_Exception_When_Seat_Is_Less_than_1()
        {
            int seatNumber = 0;
            Session ticketSession = GetCompleteSessionToTest();


            UserAccount ticketUserAccount = GetUserAccountToTest();
            Ticket ticketToTest = GetTicketToTest(0, DateTime.Now, Guid.NewGuid(), 10, seatNumber, false, ticketSession, ticketUserAccount);

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(ticketUserAccount.UserAccountID)).Returns(ticketUserAccount);
            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(ticketSession.SessionID)).Returns(ticketSession);


            Action action = () => _ticketService.AddTicket(ticketToTest);
            action.Should().Throw<Exception>().WithMessage($"Invalid seat number ({ticketToTest.SeatNumber}) for this Cine Room ");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(ticketUserAccount.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Retrieve(ticketSession.SessionID), Times.Once);

            _unitOfWorkMock.Verify(uow => uow.Tickets.Add(It.IsAny<Ticket>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void TicketService_AddTicket_Should_Throw_Exception_When_Seat_Is_Higher_than_CineRoom_TotalSeats()
        {
            Session ticketSession = GetCompleteSessionToTest();
            int seatNumber = ticketSession.CineRoom.TotalSeats + 1;


            UserAccount ticketUserAccount = GetUserAccountToTest();
            Ticket ticketToTest = GetTicketToTest(0, DateTime.Now, Guid.NewGuid(), 10, seatNumber, false, ticketSession, ticketUserAccount);

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(ticketUserAccount.UserAccountID)).Returns(ticketUserAccount);
            _unitOfWorkMock.Setup(uow => uow.Sessions.Retrieve(ticketSession.SessionID)).Returns(ticketSession);


            Action action = () => _ticketService.AddTicket(ticketToTest);
            action.Should().Throw<Exception>().WithMessage($"Invalid seat number ({ticketToTest.SeatNumber}) for this Cine Room ");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(ticketUserAccount.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Sessions.Retrieve(ticketSession.SessionID), Times.Once);

            _unitOfWorkMock.Verify(uow => uow.Tickets.Add(It.IsAny<Ticket>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        #endregion AddTicket

        #region RemoveTicket
        [TestMethod]
        public void TicketService_RemoveTicket_Should_Remove_A_Given_Ticket_When_TicketID_Exists()
        {
            int ticketIDToDelete = 1;

            CineRoom sessionCineRoom = GetCineRoomToTest();
            Movie sessionMovie = GetMovieToTest();
            Session ticketSession = GetCompleteSessionToTest();
            UserAccount ticketUserAccount = GetUserAccountToTest();
            DateTime purchaseDate = DateTime.Now;

            Ticket ticketToTest = GetTicketToTest(ticketIDToDelete, purchaseDate, Guid.NewGuid(), 30, 1, false, ticketSession, ticketUserAccount);
            _unitOfWorkMock.Setup(uow => uow.Tickets.Retrieve(ticketIDToDelete)).Returns(ticketToTest);

            _ticketService.RemoveTicket(new Ticket { TicketID = ticketIDToDelete });

            _unitOfWorkMock.Verify(uow => uow.Tickets.Retrieve(ticketIDToDelete), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Tickets.Delete(It.IsAny<Ticket>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void TicketService_RemoveTicket_Should_Throw_Exception_When_Ticket_ID_Not_Exists()
        {
            int ticketIDToDelete = 15;

            //CineRoom sessionCineRoom = GetCineRoomToTest();
            //Movie sessionMovie = GetMovieToTest();
            //Session ticketSession = GetCompleteSessionToTest();
            //UserAccount ticketUserAccount = GetUserAccountToTest();
            //DateTime purchaseDate = DateTime.Now;

            //Ticket existingTicket = GetTicketToTest(1, purchaseDate, Guid.NewGuid(), 30, 1, false, ticketSession, ticketUserAccount);

            _unitOfWorkMock.Setup(uow => uow.Tickets.Retrieve(ticketIDToDelete));
            

            Action action = () => _ticketService.RemoveTicket(new Ticket { TicketID = ticketIDToDelete }); 
            action.Should().Throw<Exception>().WithMessage($"Ticket not found: {ticketIDToDelete}");

            _unitOfWorkMock.Verify(uow => uow.Tickets.Retrieve(ticketIDToDelete), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Tickets.Delete(It.IsAny<Ticket>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void TicketService_RemoveTicket_Should_Throw_ArgumentException_When_Ticket_Parameter_Is_Null()
        {
            Action action = () => _ticketService.RemoveTicket(null);
            action.Should().Throw<ArgumentException>().WithMessage("Ticket parameter cannot be null. (Parameter 'ticketToRemove')");

            _unitOfWorkMock.Verify(uow => uow.Tickets.Delete(It.IsAny<Ticket>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion RemoveTicket

        #region Get Ticket

        [TestMethod]
        public void TicketService_RetriveAll_Should_Return_All_Tickets()
        {
            Ticket ticketToTest = GetTicketToTest(1, DateTime.Now, Guid.NewGuid(), 10, 1, false, GetCompleteSessionToTest(), GetUserAccountToTest());
            _unitOfWorkMock.Setup(uow => uow.Tickets.RetrieveAll()).Returns(new List<Ticket> { ticketToTest });

            var tickets = _ticketService.RetrieveAll();
            tickets.Should().HaveCount(1);
            tickets.Should().Contain(ticketToTest);
            _unitOfWorkMock.Verify(uow => uow.Tickets.RetrieveAll(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void TicketService_RetrieveBySession_Should_Return_Tickets_Of_Given_Session()
        {
            var ticketSession = GetCompleteSessionToTest();
            Ticket ticketToTest = GetTicketToTest(1, DateTime.Now, Guid.NewGuid(), 10, 1, false,ticketSession, GetUserAccountToTest());
            _unitOfWorkMock.Setup(uow => uow.Tickets.RetrieveAll()).Returns(new List<Ticket> { ticketToTest });

            var tickets = _ticketService.RetrieveBySession(ticketSession.SessionID);
            tickets.Should().HaveCount(1);
            tickets.Should().Contain(ticketToTest);
            _unitOfWorkMock.Verify(uow => uow.Tickets.RetrieveAll(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void TicketService_RetrieveByUserAccount_Should_Return_Tickets_Of_Given_UserAccount()
        {
            var ticketUserAccount = GetUserAccountToTest();
            Ticket ticketToTest = GetTicketToTest(1, DateTime.Now, Guid.NewGuid(), 10, 1, false, GetCompleteSessionToTest(),ticketUserAccount );
            _unitOfWorkMock.Setup(uow => uow.Tickets.RetrieveAll()).Returns(new List<Ticket> { ticketToTest });

            var tickets = _ticketService.RetrieveByUserAccount(ticketUserAccount.UserAccountID);
            tickets.Should().HaveCount(1);
            tickets.Should().Contain(ticketToTest);
            _unitOfWorkMock.Verify(uow => uow.Tickets.RetrieveAll(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void TicketService_RetrieveTicketByCode_Should_Return_Ticket_When_Exists()
        {
            Guid code = Guid.NewGuid();
            Ticket ticketToTest = GetTicketToTest(1, DateTime.Now, code, 10, 1, false, GetCompleteSessionToTest(), GetUserAccountToTest());
            _unitOfWorkMock.Setup(uow => uow.Tickets.RetrieveByCode(code)).Returns( ticketToTest );

            var ticket = _ticketService.RetrieveTicketByCode(code);
            
            ticket.Should().NotBeNull();
            ticket.Should().Be(ticketToTest);
            
            _unitOfWorkMock.Verify(uow => uow.Tickets.RetrieveByCode(code), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }


        [TestMethod]
        public void TicketService_RetrieveTicketByCode_Should_Throw_Exception_When_Not_Exists()
        {
            Guid code = Guid.NewGuid();
            
            _unitOfWorkMock.Setup(uow => uow.Tickets.RetrieveByCode(code));

            Action action =()  =>  _ticketService.RetrieveTicketByCode(code);
            action.Should().Throw<Exception>().WithMessage($"Ticket not found: {code}");
            

            _unitOfWorkMock.Verify(uow => uow.Tickets.RetrieveByCode(code), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }


        [TestMethod]
        public void TicketService_RetrieveTicketById_Should_Return_Ticket_When_Exists()
        {
            var ticketId = 1;
            Ticket ticketToTest = GetTicketToTest(ticketId, DateTime.Now, Guid.NewGuid(), 10, 1, false, GetCompleteSessionToTest(), GetUserAccountToTest());
            _unitOfWorkMock.Setup(uow => uow.Tickets.Retrieve(ticketId)).Returns(ticketToTest);

            var ticket = _ticketService.RetrieveTicketById(ticketId);

            ticket.Should().NotBeNull();
            ticket.Should().Be(ticketToTest);

            _unitOfWorkMock.Verify(uow => uow.Tickets.Retrieve(ticketId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }


        [TestMethod]
        public void TicketService_RetrieveTicketById_Should_Throw_Exception_When_Not_Exists()
        {
            var ticketId = 1;
            Ticket ticketToTest = GetTicketToTest(ticketId, DateTime.Now, Guid.NewGuid(), 10, 1, false, GetCompleteSessionToTest(), GetUserAccountToTest());
            _unitOfWorkMock.Setup(uow => uow.Tickets.Retrieve(ticketId));


            Action action = () => _ticketService.RetrieveTicketById(ticketId);
            action.Should().Throw<Exception>().WithMessage($"Ticket not found: {ticketId}");


            _unitOfWorkMock.Verify(uow => uow.Tickets.Retrieve(ticketId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        #endregion Gets_Get Ticket

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

        private UserAccount GetUserAccountToTest(int userAccountID = 1, string name = "user name", AccessLevel lvl = AccessLevel.Customer, string mail = "mail@domain.com")
        {
            return new UserAccount
            {
                UserAccountID = userAccountID,
                Name = name,
                Level = lvl,
                Mail = mail

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

        private Ticket GetTicketToTest(int ticketID, DateTime purchaseDate, Guid code, decimal price, int seatNumber, bool used, Session ticketSession, UserAccount ticketUserAccount)
        {
            return new Ticket
            {
                TicketID = ticketID,
                PurchaseDate = purchaseDate,
                Code = code,
                Price = price,
                SeatNumber = seatNumber,
                Used = used,
                Session = ticketSession,
                UserAccount = ticketUserAccount
            };
        }

        //public Ticket GetCompleteTicketToTest(int ticketID =1, decimal price = 30, int SeatNumber)
    }
}
