using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.TicketModule
{
    public interface ITicketRepository
    {
        /// <summary>
        /// Add new register of Ticket on database.
        /// </summary>
        /// <param name="ticketToAdd">Ticket that should be added.</param>
        void Add(Ticket ticketToAdd);

        /// <summary>
        /// Update Ticket properties on database.
        /// </summary>
        /// <param name="ticketToUpdate">An instance of Ticket with all properties that will update on database. It should be linked with DBContext.</param>
        void Update(Ticket ticketToUpdate);

        /// <summary>
        /// Remove a given Ticket on database and of the DBContext
        /// </summary>
        /// <param name="ticketToDelete">An instance of Ticket that will remove on database. It should be linked with DBContext.</param>
        void Delete(Ticket ticketToDelete);

        /// <summary>
        /// Retrieves a Ticket with a given database ID
        /// </summary>
        /// <param name="ticketID">ID used with condition for the search on database</param>
        /// <returns>Returns Ticket persited on database or null.</returns>
        Ticket Retrieve(int ticketID);

        /// <summary>
        /// Retrieves a Ticket with a given ticket code
        /// </summary>
        /// <param name="guidCode">Ticket code (GUID) used with condition for the search on database</param>
        /// <returns>Returns Ticket persited on database or null.</returns>
        Ticket RetrieveByCode(Guid guidCode);

        /// <summary>
        /// Retrieves the collection of Tickets
        /// </summary>
        /// <returns>Returns collection of the Tickets from database.</returns>
        IEnumerable<Ticket> RetrieveAll();

        /// <summary>
        /// Retrieves a list of the tickets of a given custumer. (UserAccount)
        /// </summary>
        /// <param name="customer">UserAccount that is owner of tickets.</param>
        /// <returns>List of ticket bought by this UserAccount.</returns>
        IEnumerable<Ticket> RetrieveByUserAccount(UserAccount customer);

        /// <summary>
        /// Retrieves a list of tickets of a given Session.
        /// </summary>
        /// <param name="session">Session that has the tickets.</param>
        /// <returns>List of tickets for this Session.</returns>
        IEnumerable<Ticket> RetrieveBySession(Session session);
    }
}
