using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Infra.Data.EF
{
    public class SessionRepository : ISessionRepository
    {
        private readonly MarajoaraContext DBContext;

        public SessionRepository(MarajoaraContext dbContext)
        {
            DBContext = dbContext;
        }

        public void Add(Session sessionToAdd)
        {
            throw new NotImplementedException();
        }

        public void Delete(Session sessionToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Session> RetrieveByDate(DateTime minSessionDate, DateTime lastSessionDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Session> RetriveAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Session> RetriveByDate(DateTime sessionDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Session> RetriveByMovieTitle(string movieTitle)
        {
            throw new NotImplementedException();
        }

        public void Update(Session sessionToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
