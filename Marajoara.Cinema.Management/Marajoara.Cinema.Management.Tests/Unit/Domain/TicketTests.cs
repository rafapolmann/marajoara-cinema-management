using FluentAssertions;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Marajoara.Cinema.Management.Tests.Unit.Domain
{
    [TestClass]
    public class TicketTests
    {
        [TestMethod]
        public void Ticket_Set_Session_Should_Set_SessionID_Porperty()
        {
            Session session = GetCompleteSessionToTest();
            Ticket ticketToTest = new Ticket();

            ticketToTest.Session = session;

            ticketToTest.SessionID.Should().Be(session.SessionID);
        }
        [TestMethod]
        public void Ticket_Set_Session_To_Null_Should_Set_SessionID_Porperty_To_Zero()
        {
            Ticket ticketToTest = GetCompleteTicketToTest();

            ticketToTest.Session = null;

            ticketToTest.SessionID.Should().Be(0);
        }


        [TestMethod]
        public void Ticket_Set_UserAccount_Should_Set_UserAccountID_Porperty()
        {

            UserAccount userAccount = GetUserAccountToTest();
            Ticket ticketToTest = new Ticket();

            ticketToTest.UserAccount = userAccount;

            ticketToTest.UserAccountID.Should().Be(userAccount.UserAccountID);
        }
        [TestMethod]
        public void Ticket_Set_UserAccount_To_Null_Should_Set_UserAccountID_Porperty_To_Zero()
        {
            Ticket ticketToTest = GetCompleteTicketToTest();

            ticketToTest.UserAccount = null;

            ticketToTest.UserAccountID.Should().Be(0);
        }

        #region Helpers

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

        private Ticket GetCompleteTicketToTest(int ticketID = 1, int seatNumber = 1)
        {
            return GetTicketToTest(ticketID, DateTime.UtcNow, Guid.NewGuid(), 10, 1, false, GetCompleteSessionToTest(), GetUserAccountToTest());
        }
        #endregion Helpers
    }
}
