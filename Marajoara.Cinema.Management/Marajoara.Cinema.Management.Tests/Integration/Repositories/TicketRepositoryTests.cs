using FluentAssertions;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Tests.Integration.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marajoara.Cinema.Management.Tests.Integration.Repositories
{
    [TestClass]
    public class TicketRepositoryTests : UnitOfWorkIntegrationBase
    {
        [TestInitialize]
        public void Initialize()
        {
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance();
        }

        [TestMethod]
        public void UnitOfWork_Should_Insert_New_Ticket_On_Database()
        {
            UserAccount ticketUserAccount = GetUserAccountToTest();

            _marajoaraUnitOfWork.UserAccounts.Add(ticketUserAccount);
            _marajoaraUnitOfWork.Commit();
            int userAccountID = ticketUserAccount.UserAccountID;

            CineRoom ticketCineRoom = GetCineRoomToTest();

            _marajoaraUnitOfWork.CineRooms.Add(ticketCineRoom);
            _marajoaraUnitOfWork.Commit();
            int cineRoomID = ticketCineRoom.CineRoomID;

            Movie ticketMovie = GetMovieToTest();

            _marajoaraUnitOfWork.Movies.Add(ticketMovie);
            _marajoaraUnitOfWork.Commit();
            int movieID = ticketMovie.MovieID;

            DateTime sessionDate = DateTime.Parse("14:00:00 14/08/2022");
            Session ticketSession = GetSessionToTest(ticketCineRoom, ticketMovie, sessionDate);

            _marajoaraUnitOfWork.Sessions.Add(ticketSession);
            _marajoaraUnitOfWork.Commit();
            int sessionID = ticketSession.SessionID;

            Guid tickeCode = Guid.NewGuid();
            DateTime purchaseDate = DateTime.Parse("09:45:28 09/08/2022");
            Ticket ticketToAdd = GetTicketToTest(ticketUserAccount, ticketSession, tickeCode, purchaseDate);

            _marajoaraUnitOfWork.Tickets.Add(ticketToAdd);
            _marajoaraUnitOfWork.Commit();
            int ticketID = ticketToAdd.TicketID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Ticket ticketToAssert = _marajoaraUnitOfWork.Tickets.Retrieve(ticketID);

            ticketToAssert.Should().NotBeNull();
            ticketToAssert.TicketID.Should().Be(ticketID);
            ticketToAssert.Price.Should().Be(45);
            ticketToAssert.SeatNumber.Should().Be(11);
            ticketToAssert.Code.Should().Be(tickeCode);
            ticketToAssert.Used.Should().BeFalse();
            ticketToAssert.PurchaseDate.Should().Be(purchaseDate);
            ticketToAssert.SessionID.Should().Be(sessionID);
            ticketToAssert.Session.Should().NotBeNull();
            ticketToAssert.Session.Movie.Should().NotBeNull();
            ticketToAssert.Session.CineRoom.Should().NotBeNull();
            ticketToAssert.UserAccount.Should().NotBeNull();
            ticketToAssert.UserAccountID.Should().Be(userAccountID);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Update_All_Properties_Of_Existing_Ticket_On_Database()
        {
            UserAccount ticketUserAccount01 = GetUserAccountToTest("User01", "user@mail01");
            UserAccount ticketUserAccount02 = GetUserAccountToTest("User02", "user@mail02");

            _marajoaraUnitOfWork.UserAccounts.Add(ticketUserAccount01);
            _marajoaraUnitOfWork.UserAccounts.Add(ticketUserAccount02);
            _marajoaraUnitOfWork.Commit();
            int userAccountID02 = ticketUserAccount02.UserAccountID;

            CineRoom ticketCineRoom = GetCineRoomToTest();
            _marajoaraUnitOfWork.CineRooms.Add(ticketCineRoom);
            _marajoaraUnitOfWork.Commit();

            Movie ticketMovie = GetMovieToTest();
            _marajoaraUnitOfWork.Movies.Add(ticketMovie);
            _marajoaraUnitOfWork.Commit();

            DateTime sessionDate01 = DateTime.Parse("20:00:00 15/08/2022");
            Session ticketSession01 = GetSessionToTest(ticketCineRoom, ticketMovie, sessionDate01);

            DateTime sessionDate02 = DateTime.Parse("14:00:00 14/08/2022");
            Session ticketSession02 = GetSessionToTest(ticketCineRoom, ticketMovie, sessionDate02);

            _marajoaraUnitOfWork.Sessions.Add(ticketSession01);
            _marajoaraUnitOfWork.Sessions.Add(ticketSession02);
            _marajoaraUnitOfWork.Commit();            
            int sessionID02 = ticketSession02.SessionID;

            Guid tickeCode = Guid.NewGuid();
            DateTime purchaseDate = DateTime.Parse("10:45:28 10/08/2022");
            Ticket ticketToAdd = GetTicketToTest(ticketUserAccount01, ticketSession01, tickeCode, purchaseDate);

            _marajoaraUnitOfWork.Tickets.Add(ticketToAdd);
            _marajoaraUnitOfWork.Commit();
            int ticketID = ticketToAdd.TicketID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Guid newCode = Guid.NewGuid();
            DateTime newPurchaseDate = DateTime.Parse("11:25:28 11/08/2022");

            Ticket ticketToUpdate = _marajoaraUnitOfWork.Tickets.Retrieve(ticketID);
            ticketToUpdate.Price = 35;
            ticketToUpdate.SeatNumber = 7;
            ticketToUpdate.Used = true;           
            ticketToUpdate.Code = newCode;            
            ticketToUpdate.PurchaseDate = newPurchaseDate;
            ticketToUpdate.Session = _marajoaraUnitOfWork.Sessions.Retrieve(sessionID02);
            ticketToUpdate.UserAccount = _marajoaraUnitOfWork.UserAccounts.Retrieve(userAccountID02);
            
            _marajoaraUnitOfWork.Tickets.Update(ticketToUpdate);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Ticket ticketToAssert = _marajoaraUnitOfWork.Tickets.Retrieve(ticketID);

            ticketToAssert.Should().NotBeNull();
            ticketToAssert.Price.Should().Be(35);
            ticketToAssert.SeatNumber.Should().Be(7);
            ticketToAssert.Code.Should().Be(newCode);
            ticketToAssert.Used.Should().BeTrue();
            ticketToAssert.PurchaseDate.Should().Be(newPurchaseDate);
            ticketToAssert.Session.Should().NotBeNull();
            ticketToAssert.SessionID.Should().Be(sessionID02);
            ticketToAssert.UserAccount.Should().NotBeNull();
            ticketToAssert.UserAccountID.Should().Be(userAccountID02);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Delete_Existing_Ticket_On_Database()
        {
            UserAccount ticketUserAccount = GetUserAccountToTest();

            _marajoaraUnitOfWork.UserAccounts.Add(ticketUserAccount);
            _marajoaraUnitOfWork.Commit();
            int userAccountID = ticketUserAccount.UserAccountID;

            CineRoom ticketCineRoom = GetCineRoomToTest();

            _marajoaraUnitOfWork.CineRooms.Add(ticketCineRoom);
            _marajoaraUnitOfWork.Commit();
            int cineRoomID = ticketCineRoom.CineRoomID;

            Movie ticketMovie = GetMovieToTest();

            _marajoaraUnitOfWork.Movies.Add(ticketMovie);
            _marajoaraUnitOfWork.Commit();
            int movieID = ticketMovie.MovieID;

            DateTime sessionDate = DateTime.Parse("14:00:00 14/08/2022");
            Session ticketSession = GetSessionToTest(ticketCineRoom, ticketMovie, sessionDate);

            _marajoaraUnitOfWork.Sessions.Add(ticketSession);
            _marajoaraUnitOfWork.Commit();
            int sessionID = ticketSession.SessionID;

            Guid tickeCode = Guid.NewGuid();
            DateTime purchaseDate = DateTime.Parse("09:45:28 09/08/2022");
            Ticket ticketToAdd = GetTicketToTest(ticketUserAccount, ticketSession, tickeCode, purchaseDate);

            _marajoaraUnitOfWork.Tickets.Add(ticketToAdd);
            _marajoaraUnitOfWork.Commit();
            int ticketID = ticketToAdd.TicketID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Ticket ticketToAssert = _marajoaraUnitOfWork.Tickets.Retrieve(ticketID);
            _marajoaraUnitOfWork.Tickets.Delete(ticketToAssert);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            _marajoaraUnitOfWork.Tickets.Retrieve(ticketID).Should().BeNull();
            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_Persisted_Ticket_On_Database_By_TicketID()
        {
            UserAccount ticketUserAccount = GetUserAccountToTest();

            _marajoaraUnitOfWork.UserAccounts.Add(ticketUserAccount);
            _marajoaraUnitOfWork.Commit();
            int userAccountID = ticketUserAccount.UserAccountID;

            CineRoom ticketCineRoom = GetCineRoomToTest();

            _marajoaraUnitOfWork.CineRooms.Add(ticketCineRoom);
            _marajoaraUnitOfWork.Commit();
            int cineRoomID = ticketCineRoom.CineRoomID;

            Movie ticketMovie = GetMovieToTest();

            _marajoaraUnitOfWork.Movies.Add(ticketMovie);
            _marajoaraUnitOfWork.Commit();
            int movieID = ticketMovie.MovieID;

            DateTime sessionDate = DateTime.Parse("14:00:00 14/08/2022");
            Session ticketSession = GetSessionToTest(ticketCineRoom, ticketMovie, sessionDate);

            _marajoaraUnitOfWork.Sessions.Add(ticketSession);
            _marajoaraUnitOfWork.Commit();
            int sessionID = ticketSession.SessionID;

            Guid tickeCode = Guid.NewGuid();
            DateTime purchaseDate = DateTime.Parse("09:45:28 09/08/2022");
            Ticket ticketToAdd = GetTicketToTest(ticketUserAccount, ticketSession, tickeCode, purchaseDate);

            _marajoaraUnitOfWork.Tickets.Add(ticketToAdd);
            _marajoaraUnitOfWork.Commit();
            int ticketID = ticketToAdd.TicketID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Ticket ticketToAssert = _marajoaraUnitOfWork.Tickets.Retrieve(ticketID);

            ticketToAssert.Should().NotBeNull();
            ticketToAssert.TicketID.Should().Be(ticketID);
            ticketToAssert.Price.Should().Be(45);
            ticketToAssert.SeatNumber.Should().Be(11);
            ticketToAssert.Code.Should().Be(tickeCode);
            ticketToAssert.Used.Should().BeFalse();
            ticketToAssert.PurchaseDate.Should().Be(purchaseDate);
            ticketToAssert.SessionID.Should().Be(sessionID);
            ticketToAssert.Session.Should().NotBeNull();
            ticketToAssert.Session.Movie.Should().NotBeNull();
            ticketToAssert.Session.Movie.MovieID.Should().Be(movieID);
            ticketToAssert.Session.CineRoom.Should().NotBeNull();
            ticketToAssert.Session.CineRoom.CineRoomID.Should().Be(cineRoomID);
            ticketToAssert.UserAccount.Should().NotBeNull();
            ticketToAssert.UserAccountID.Should().Be(userAccountID);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_Persisted_Ticket_On_Database_By_TicketCode()
        {
            UserAccount ticketUserAccount = GetUserAccountToTest();

            _marajoaraUnitOfWork.UserAccounts.Add(ticketUserAccount);
            _marajoaraUnitOfWork.Commit();
            int userAccountID = ticketUserAccount.UserAccountID;

            CineRoom ticketCineRoom = GetCineRoomToTest();

            _marajoaraUnitOfWork.CineRooms.Add(ticketCineRoom);
            _marajoaraUnitOfWork.Commit();
            int cineRoomID = ticketCineRoom.CineRoomID;

            Movie ticketMovie = GetMovieToTest();

            _marajoaraUnitOfWork.Movies.Add(ticketMovie);
            _marajoaraUnitOfWork.Commit();
            int movieID = ticketMovie.MovieID;

            DateTime sessionDate = DateTime.Parse("14:00:00 14/08/2022");
            Session ticketSession = GetSessionToTest(ticketCineRoom, ticketMovie, sessionDate);

            _marajoaraUnitOfWork.Sessions.Add(ticketSession);
            _marajoaraUnitOfWork.Commit();
            int sessionID = ticketSession.SessionID;

            Guid tickeCode = Guid.NewGuid();
            DateTime purchaseDate = DateTime.Parse("09:45:28 09/08/2022");
            Ticket ticketToAdd = GetTicketToTest(ticketUserAccount, ticketSession, tickeCode, purchaseDate);

            _marajoaraUnitOfWork.Tickets.Add(ticketToAdd);
            _marajoaraUnitOfWork.Commit();
            int ticketID = ticketToAdd.TicketID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            Ticket ticketToAssert = _marajoaraUnitOfWork.Tickets.RetrieveByCode(tickeCode);

            ticketToAssert.Should().NotBeNull();
            ticketToAssert.TicketID.Should().Be(ticketID);
            ticketToAssert.Price.Should().Be(45);
            ticketToAssert.SeatNumber.Should().Be(11);
            ticketToAssert.Code.Should().Be(tickeCode);
            ticketToAssert.Used.Should().BeFalse();
            ticketToAssert.PurchaseDate.Should().Be(purchaseDate);
            ticketToAssert.SessionID.Should().Be(sessionID);
            ticketToAssert.Session.Should().NotBeNull();
            ticketToAssert.Session.Movie.Should().NotBeNull();
            ticketToAssert.Session.Movie.MovieID.Should().Be(movieID);
            ticketToAssert.Session.CineRoom.Should().NotBeNull();
            ticketToAssert.Session.CineRoom.CineRoomID.Should().Be(cineRoomID);
            ticketToAssert.UserAccount.Should().NotBeNull();
            ticketToAssert.UserAccountID.Should().Be(userAccountID);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_List_Of_All_Tickets_On_Database_By_Session()
        {
            UserAccount ticketUserAccount = GetUserAccountToTest();

            _marajoaraUnitOfWork.UserAccounts.Add(ticketUserAccount);
            _marajoaraUnitOfWork.Commit();
            int userAccountID = ticketUserAccount.UserAccountID;

            CineRoom ticketCineRoom = GetCineRoomToTest();

            _marajoaraUnitOfWork.CineRooms.Add(ticketCineRoom);
            _marajoaraUnitOfWork.Commit();
            int cineRoomID = ticketCineRoom.CineRoomID;

            Movie ticketMovie = GetMovieToTest();

            _marajoaraUnitOfWork.Movies.Add(ticketMovie);
            _marajoaraUnitOfWork.Commit();
            int movieID = ticketMovie.MovieID;

            DateTime sessionDate01 = DateTime.Parse("20:00:00 15/08/2022");
            Session ticketSession01 = GetSessionToTest(ticketCineRoom, ticketMovie, sessionDate01);

            DateTime sessionDate02 = DateTime.Parse("14:00:00 14/08/2022");
            Session ticketSession02 = GetSessionToTest(ticketCineRoom, ticketMovie, sessionDate02);

            _marajoaraUnitOfWork.Sessions.Add(ticketSession01);
            _marajoaraUnitOfWork.Sessions.Add(ticketSession02);
            _marajoaraUnitOfWork.Commit();
            int sessionID01 = ticketSession01.SessionID;
            int sessionID02 = ticketSession02.SessionID;

            Guid tickeCode01 = Guid.NewGuid();
            DateTime purchaseDate01 = DateTime.Parse("10:45:28 10/08/2022");
            Ticket ticketToAdd01 = GetTicketToTest(ticketUserAccount, ticketSession01, tickeCode01, purchaseDate01);

            Guid tickeCode02 = Guid.NewGuid();
            DateTime purchaseDate02 = DateTime.Parse("09:45:28 09/08/2022");
            Ticket ticketToAdd02 = GetTicketToTest(ticketUserAccount, ticketSession02, tickeCode02, purchaseDate02);

            _marajoaraUnitOfWork.Tickets.Add(ticketToAdd01);
            _marajoaraUnitOfWork.Tickets.Add(ticketToAdd02);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            List<Ticket> ticketsToAssert = _marajoaraUnitOfWork.Tickets.RetrieveBySession(ticketSession01).ToList();

            ticketsToAssert.Should().NotBeNull();
            ticketsToAssert.Should().HaveCount(1);
            ticketsToAssert[0].Price.Should().Be(45);
            ticketsToAssert[0].SeatNumber.Should().Be(11);
            ticketsToAssert[0].Code.Should().Be(tickeCode01);
            ticketsToAssert[0].Used.Should().BeFalse();
            ticketsToAssert[0].PurchaseDate.Should().Be(purchaseDate01);
            ticketsToAssert[0].SessionID.Should().Be(sessionID01);
            ticketsToAssert[0].Session.Should().NotBeNull();
            ticketsToAssert[0].Session.Movie.Should().NotBeNull();
            ticketsToAssert[0].Session.Movie.MovieID.Should().Be(movieID);
            ticketsToAssert[0].Session.CineRoom.Should().NotBeNull();
            ticketsToAssert[0].Session.CineRoom.CineRoomID.Should().Be(cineRoomID);
            ticketsToAssert[0].UserAccount.Should().NotBeNull();
            ticketsToAssert[0].UserAccountID.Should().Be(userAccountID);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_List_Of_All_Tickets_On_Database_By_UserAccount()
        {
            UserAccount ticketUserAccount01 = GetUserAccountToTest("User01", "user@mail01");
            UserAccount ticketUserAccount02 = GetUserAccountToTest("User02", "user@mail02");

            _marajoaraUnitOfWork.UserAccounts.Add(ticketUserAccount01);
            _marajoaraUnitOfWork.UserAccounts.Add(ticketUserAccount02);
            _marajoaraUnitOfWork.Commit();
            int userAccountID01 = ticketUserAccount01.UserAccountID;
            int userAccountID02 = ticketUserAccount02.UserAccountID;

            CineRoom ticketCineRoom = GetCineRoomToTest();

            _marajoaraUnitOfWork.CineRooms.Add(ticketCineRoom);
            _marajoaraUnitOfWork.Commit();
            int cineRoomID = ticketCineRoom.CineRoomID;

            Movie ticketMovie = GetMovieToTest();

            _marajoaraUnitOfWork.Movies.Add(ticketMovie);
            _marajoaraUnitOfWork.Commit();
            int movieID = ticketMovie.MovieID;

            DateTime sessionDate = DateTime.Parse("20:00:00 15/08/2022");
            Session ticketSession = GetSessionToTest(ticketCineRoom, ticketMovie, sessionDate);

            _marajoaraUnitOfWork.Sessions.Add(ticketSession);
            _marajoaraUnitOfWork.Commit();
            int sessionID = ticketSession.SessionID;

            Guid tickeCode01 = Guid.NewGuid();
            DateTime purchaseDate01 = DateTime.Parse("10:45:28 10/08/2022");
            Ticket ticketToAdd01 = GetTicketToTest(ticketUserAccount01, ticketSession, tickeCode01, purchaseDate01);

            Guid tickeCode02 = Guid.NewGuid();
            DateTime purchaseDate02 = DateTime.Parse("09:45:28 09/08/2022");
            Ticket ticketToAdd02 = GetTicketToTest(ticketUserAccount02, ticketSession, tickeCode02, purchaseDate02);

            _marajoaraUnitOfWork.Tickets.Add(ticketToAdd01);
            _marajoaraUnitOfWork.Tickets.Add(ticketToAdd02);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            List<Ticket> ticketsToAssert = _marajoaraUnitOfWork.Tickets.RetrieveByUserAccount(ticketUserAccount01).ToList();

            ticketsToAssert.Should().NotBeNull();
            ticketsToAssert.Should().HaveCount(1);
            ticketsToAssert[0].Price.Should().Be(45);
            ticketsToAssert[0].SeatNumber.Should().Be(11);
            ticketsToAssert[0].Code.Should().Be(tickeCode01);
            ticketsToAssert[0].Used.Should().BeFalse();
            ticketsToAssert[0].PurchaseDate.Should().Be(purchaseDate01);
            ticketsToAssert[0].SessionID.Should().Be(sessionID);
            ticketsToAssert[0].Session.Should().NotBeNull();
            ticketsToAssert[0].Session.Movie.Should().NotBeNull();
            ticketsToAssert[0].Session.Movie.MovieID.Should().Be(movieID);
            ticketsToAssert[0].Session.CineRoom.Should().NotBeNull();
            ticketsToAssert[0].Session.CineRoom.CineRoomID.Should().Be(cineRoomID);
            ticketsToAssert[0].UserAccount.Should().NotBeNull();
            ticketsToAssert[0].UserAccountID.Should().Be(userAccountID01);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_List_Of_All_Tickets_From_Database()
        {
            UserAccount ticketUserAccount = GetUserAccountToTest();

            _marajoaraUnitOfWork.UserAccounts.Add(ticketUserAccount);
            _marajoaraUnitOfWork.Commit();
            int userAccountID = ticketUserAccount.UserAccountID;

            CineRoom ticketCineRoom = GetCineRoomToTest();

            _marajoaraUnitOfWork.CineRooms.Add(ticketCineRoom);
            _marajoaraUnitOfWork.Commit();
            int cineRoomID = ticketCineRoom.CineRoomID;

            Movie ticketMovie = GetMovieToTest();

            _marajoaraUnitOfWork.Movies.Add(ticketMovie);
            _marajoaraUnitOfWork.Commit();
            int movieID = ticketMovie.MovieID;

            DateTime sessionDate = DateTime.Parse("14:00:00 14/08/2022");
            Session ticketSession = GetSessionToTest(ticketCineRoom, ticketMovie, sessionDate);

            _marajoaraUnitOfWork.Sessions.Add(ticketSession);
            _marajoaraUnitOfWork.Commit();
            int sessionID = ticketSession.SessionID;

            Guid tickeCode01 = Guid.NewGuid();
            DateTime purchaseDate01 = DateTime.Parse("09:45:28 09/08/2022");
            Ticket ticket01 = GetTicketToTest(ticketUserAccount, ticketSession, tickeCode01, purchaseDate01, 25, 10);

            Guid tickeCode02 = Guid.NewGuid();
            DateTime purchaseDate02 = DateTime.Parse("15:25:18 10/08/2022");
            Ticket ticket02 = GetTicketToTest(ticketUserAccount, ticketSession, tickeCode02, purchaseDate02, 25, 11);

            Guid tickeCode03 = Guid.NewGuid();
            DateTime purchaseDate03 = DateTime.Parse("23:00:12 11/08/2022");
            Ticket ticket03 = GetTicketToTest(ticketUserAccount, ticketSession, tickeCode03, purchaseDate03, 25, 12);

            _marajoaraUnitOfWork.Tickets.Add(ticket01);
            _marajoaraUnitOfWork.Tickets.Add(ticket02);
            _marajoaraUnitOfWork.Tickets.Add(ticket03);
            _marajoaraUnitOfWork.Commit();
            int ticketID01 = ticket01.TicketID;
            int ticketID02 = ticket03.TicketID;
            int ticketID03 = ticket03.TicketID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            List<Ticket> ticketsToAssert = _marajoaraUnitOfWork.Tickets.RetrieveAll().ToList();

            ticketsToAssert.Should().NotBeNull();
            ticketsToAssert.Should().HaveCount(3);
            ticketsToAssert.Should().Contain(t => t.TicketID.Equals(ticketID01));
            ticketsToAssert.Should().Contain(t => t.TicketID.Equals(ticketID02));
            ticketsToAssert.Should().Contain(t => t.TicketID.Equals(ticketID03));

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Throw_ArgumentException_In_RetrieveBySession_When_Session_Parameter_Is_Null()
        {
            Action action = () => _marajoaraUnitOfWork.Tickets.RetrieveBySession(null);
            action.Should().Throw<ArgumentException>().WithMessage("Session parameter cannot be null. (Parameter 'session')");
        }

        [TestMethod]
        public void UnitOfWork_Should_Throw_ArgumentException_In_RetrieveByUserAccount_When_Customer_Parameter_Is_Null()
        {
            Action action = () => _marajoaraUnitOfWork.Tickets.RetrieveByUserAccount(null);
            action.Should().Throw<ArgumentException>().WithMessage("UserAccount parameter cannot be null. (Parameter 'customer')");
        }
    }
}
