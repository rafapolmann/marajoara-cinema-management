using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule
{
    public class TicketService : ITicketService
    {
        private readonly IMarajoaraUnitOfWork _unitOfWork;
        private readonly ISessionService _sessionService;
        private readonly IUserAccountService _userAccountService;

        public TicketService(IMarajoaraUnitOfWork unitOfWork, ISessionService sessionService, IUserAccountService userAccountService)
        {
            _unitOfWork = unitOfWork;
            _sessionService = sessionService;
            _userAccountService = userAccountService;
        }
        public int AddTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentException("Ticket parameter cannot be null.", nameof(ticket));

            ticket = GetValidatedTicket(ticket);

            ValidateTicketSeat(ticket);
            _unitOfWork.Tickets.Add(ticket);
            _unitOfWork.Commit();
            return ticket.TicketID;
        }

        public bool RemoveTicket(Ticket ticketToRemove)
        {
            if (ticketToRemove == null)
                throw new ArgumentException("Ticket parameter cannot be null.", nameof(ticketToRemove));

            var dbTicket = _unitOfWork.Tickets.Retrieve(ticketToRemove.TicketID);

            if (dbTicket == null)
                throw new Exception($"Ticket not found: {ticketToRemove.TicketID}");

            _unitOfWork.Tickets.Delete(dbTicket);
            _unitOfWork.Commit();

            return true;
        }

        public IEnumerable<Ticket> RetrieveAll()
        {
            return _unitOfWork.Tickets.RetrieveAll();
        }

        public IEnumerable<Ticket> RetrieveBySession(int sessionID)
        {
            return _unitOfWork.Tickets.RetrieveAll().Where(x => x.SessionID == sessionID);
        }

        public IEnumerable<Ticket> RetrieveByUserAccount(int userAccountID)
        {
            return _unitOfWork.Tickets.RetrieveAll().Where(x => x.UserAccountID == userAccountID);
        }

        public Ticket RetrieveTicketByCode(Guid ticketGuid)
        {
            var ticket = _unitOfWork.Tickets.RetrieveByCode(ticketGuid);
            if (ticket == null)
                throw new Exception($"Ticket not found: {ticketGuid}");

            return ticket;
        }

        public Ticket RetrieveTicketById(int ticketId)
        {
            var ticket =  _unitOfWork.Tickets.Retrieve(ticketId);
            if (ticket == null)
                throw new Exception($"Ticket not found: {ticketId}");
            
            return ticket;
        }

        public bool SetTicketAsUsed(Guid ticketGuid)
        {
            var ticketOnDb =RetrieveTicketByCode(ticketGuid);
            if (ticketOnDb.Used)
                throw new Exception($"Ticket {ticketOnDb.Code} already used!");

            ticketOnDb.Used = true;
            _unitOfWork.Tickets.Update(ticketOnDb);
            _unitOfWork.Commit();

            return true;
        }

        private Ticket GetValidatedTicket(Ticket ticket)
        {
            Session ticketSession = _sessionService.GetSession(ticket.SessionID);
            if (ticketSession == null)
                throw new Exception($"Session not found. SessionID: {ticket.SessionID}");

            ticket.Session = ticketSession;
            UserAccount ticketUserAccount = _userAccountService.GetUserAccountByID(ticket.UserAccountID);

            if (ticketUserAccount == null)
                throw new Exception($"User Account not found. UserAccountID: {ticket.UserAccountID}");

            ticket.UserAccount = ticketUserAccount;
            ticket.Code = Guid.NewGuid() ;
            ticket.PurchaseDate = DateTime.UtcNow;
            return ticket;
        }
       
        private void ValidateTicketSeat(Ticket ticket)
        {
            if (ticket.SeatNumber <= 0 || ticket.SeatNumber > ticket.Session.CineRoom.TotalSeats)
                throw new Exception($"Invalid seat number ({ticket.SeatNumber}) for this Cine Room ");

            var alreadyExists = _unitOfWork.Tickets.RetrieveAll().Where(t => t.SessionID == ticket.SessionID && t.SeatNumber == ticket.SeatNumber).Any();
            if (alreadyExists)
                throw new Exception($"Already exists a ticket for the seat number {ticket.SeatNumber} ");

        }

    }
}
