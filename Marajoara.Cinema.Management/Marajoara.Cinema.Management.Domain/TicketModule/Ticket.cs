using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System;

namespace Marajoara.Cinema.Management.Domain.TicketModule
{
    public class Ticket
    {
        private UserAccount _userAccount;
        private Session _session;

        public int TicketID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Guid Code { get; set; }
        public decimal Price { get; set; }
        public int SeatNumber { get; set; }
        public bool Used { get; set; }
        public int UserAccountID { get; set; }
        public UserAccount UserAccount
        {
            get { return _userAccount; }
            set
            {
                _userAccount = value;
                if (_userAccount == null)
                    UserAccountID = 0;
                else
                    UserAccountID = _userAccount.UserAccountID;
            }
        }
        public int SessionID { get; set; }
        public Session Session
        {
            get { return _session; }
            set
            {
                _session = value;
                if (_session == null)
                    SessionID = 0;
                else
                    SessionID = _session.SessionID;
            }
        }
    }
}
