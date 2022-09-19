using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marajoara.Cinema.Management.Infra.Data.EF
{
    public class TicketRepository : ITicketRepository
    {
        private readonly MarajoaraContext DBContext;

        public TicketRepository(MarajoaraContext dbContext)
        {
            DBContext = dbContext;
        }

        public void Add(Ticket ticketToAdd)
        {
            DBContext.Tickets.Add(ticketToAdd);
        }

        public void Update(Ticket ticketToUpdate)
        {
            DBContext.Entry(ticketToUpdate).State = EntityState.Modified;
        }

        public void Delete(Ticket ticketToDelete)
        {
            DBContext.Entry(ticketToDelete).State = EntityState.Deleted;
        }

        public Ticket Retrieve(int ticketID)
        {
            return DBContext.Tickets.Include(t => t.UserAccount)
                                    .Include(t => t.Session)
                                    .Include(t => t.Session.CineRoom)
                                    .Include(t => t.Session.Movie)
                                    .Where(t => t.TicketID == ticketID)
                                    .FirstOrDefault();
        }

        public Ticket RetrieveByCode(Guid guidCode)
        {
            return DBContext.Tickets.Include(t => t.UserAccount)
                                    .Include(t => t.Session)
                                    .Include(t => t.Session.CineRoom)
                                    .Include(t => t.Session.Movie)
                                    .Where(t => t.Code == guidCode)
                                    .FirstOrDefault();
        }

        public IEnumerable<Ticket> RetrieveAll()
        {
            return DBContext.Tickets.Include(t => t.UserAccount)
                                    .Include(t => t.Session)
                                    .Include(t => t.Session.CineRoom)
                                    .Include(t => t.Session.Movie);
        }

        public IEnumerable<Ticket> RetrieveBySession(Session session)
        {
            if (session == null)
                throw new ArgumentException("Session parameter cannot be null.", nameof(session));

            return DBContext.Tickets.Include(t => t.UserAccount)
                                    .Include(t => t.Session)
                                    .Include(t => t.Session.CineRoom)
                                    .Include(t => t.Session.Movie)
                                    .Where(t => t.SessionID == session.SessionID);
        }

        public IEnumerable<Ticket> RetrieveByUserAccount(UserAccount customer)
        {
            if (customer == null)
                throw new ArgumentException("UserAccount parameter cannot be null.", nameof(customer));

            return DBContext.Tickets.Include(t => t.UserAccount)
                                    .Include(t => t.Session)
                                    .Include(t => t.Session.CineRoom)
                                    .Include(t => t.Session.Movie)
                                    .Where(t => t.UserAccountID == customer.UserAccountID);
        }

    }
}
