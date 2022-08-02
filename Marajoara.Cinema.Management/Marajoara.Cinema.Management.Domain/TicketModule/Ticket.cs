using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System;

namespace Marajoara.Cinema.Management.Domain.TicketModule
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Guid Code { get; set; }
        public UserAccount UserAccount { get; set; }
        public Session Session { get; set; }
        public decimal Price { get; set; }
        public int SeatNumber { get; set; }
    }
}
