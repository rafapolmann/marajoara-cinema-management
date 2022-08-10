using Marajoara.Cinema.Management.Domain.SessionModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Infra.Data.EF
{
    public class SessionRepository : ISessionRepository
    {
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
