using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public void Delete(Ticket ticketToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ticket> RetriveAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ticket> RetriveBySession(Session session)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ticket> RetriveByUserAccount(UserAccount customer)
        {
            throw new NotImplementedException();
        }

        public void Update(Ticket ticketToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
