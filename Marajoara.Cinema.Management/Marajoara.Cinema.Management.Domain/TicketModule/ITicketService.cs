using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Domain.TicketModule
{
    public interface ITicketService
    {
        int AddTicket(Ticket ticketToAdd);        

        Ticket RetrieveTicketByCode(Guid ticketGuid);
        Ticket RetrieveTicketById(int ticketId);
        
        IEnumerable<Ticket> RetrieveAll();
        IEnumerable<Ticket> RetrieveByUserAccount(int userAccoutnId);
        IEnumerable<Ticket> RetrieveBySession(int sessionId);

        bool RemoveTicket(Ticket ticketToRemove);
    }
}
