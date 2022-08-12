using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.TicketModule
{
    public interface ITicketRepository
    {
        void Add(Ticket ticketToAdd);
        void Update(Ticket ticketToUpdate);
        void Delete(Ticket ticketToDelete);
        IEnumerable<Ticket> RetriveAll();
        /// <summary>
        /// Retrieves a list of the tickets of a given custumer (UserAccount)
        /// </summary>
        /// <param name="customer">UserAccount object</param>
        /// <returns></returns>
        IEnumerable<Ticket> RetriveByUserAccount(UserAccount customer);
        /// <summary>
        /// Retrieves a list of tickets of a given Session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        IEnumerable<Ticket> RetriveBySession(Session session);
    }
}
