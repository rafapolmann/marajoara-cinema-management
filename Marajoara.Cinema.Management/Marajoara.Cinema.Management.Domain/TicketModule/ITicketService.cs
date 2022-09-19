using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.TicketModule
{
    public interface ITicketService
    {
        /// <summary>
        /// Adds a new ticket. Validates if the Session and User account exists, Also validates if the seat is available. 
        /// In case of failed validation, throws exception;
        /// </summary>
        /// <param name="ticketToAdd"></param>
        /// <returns>New Ticket TicketID</returns>
        int AddTicket(Ticket ticketToAdd);        
        
        /// <summary>
        /// Retrive a given ticket by its code (Guid).  If not found throws exception.
        /// </summary>
        /// <param name="ticketGuid"></param>
        /// <returns></returns>
        Ticket RetrieveTicketByCode(Guid ticketGuid);

        /// <summary>
        /// Retrive a given ticket by its ID.  If not found throws exception.
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        Ticket RetrieveTicketById(int ticketId);
        
        /// <summary>
        /// Retrive all tickets
        /// </summary>
        /// <returns>A IEnumerable of Ticket</returns>
        IEnumerable<Ticket> RetrieveAll();

        /// <summary>
        /// Retrieve all tickets of a given User Account . If User Account not found, throws exception;
        /// </summary>
        /// <param name="userAccoutnId">UserAccountID</param>
        /// <returns>A IEnumerable of Ticket</returns>
        IEnumerable<Ticket> RetrieveByUserAccount(int userAccountID);

        /// <summary>
        /// Retrieve all tickets of a given Session . If Session not found, throws exception;
        /// </summary>
        /// <param name="sessionId">SessionID</param>
        /// <returns>A IEnumerable of Ticket</returns>
        IEnumerable<Ticket> RetrieveBySession(int sessionID);

        /// <summary>
        /// Removes a given Ticket. Throws exception if Ticket not found. 
        /// </summary>
        /// <param name="ticketToRemove">Ticket for removal</param>
        /// <returns>Success of the operation</returns>
        bool RemoveTicket(Ticket ticketToRemove);
    }
}
