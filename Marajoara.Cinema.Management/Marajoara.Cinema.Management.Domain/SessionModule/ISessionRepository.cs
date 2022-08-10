using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Domain.SessionModule
{
    public interface ISessionRepository
    {
        void Add(Session sessionToAdd);
        void Update(Session sessionToUpdate);
        void Delete(Session sessionToDelete);        
        IEnumerable<Session> RetriveAll();

        /// <summary>
        /// Returns all sessions that are presenting a given movie
        /// </summary>
        /// <param name="movieTitle">The movie title</param>
        /// <returns>A list of Session</returns>
        IEnumerable<Session> RetriveByMovieTitle(string movieTitle);

        /// <summary>
        /// Returns the sessions for a specific date
        /// </summary>
        /// <param name="sessionDate">Date of the sessions</param>
        /// <returns>A list of Session</returns>
        IEnumerable<Session> RetriveByDate(DateTime sessionDate);
        /// <summary>
        /// Returns the sessions in between the informed dates
        /// </summary>
        /// <param name="minSessionDate">The start date</param>
        /// <param name="lastSessionDate">The end date</param>
        /// <returns>A list of Session</returns>
        IEnumerable<Session> RetrieveByDate(DateTime minSessionDate, DateTime lastSessionDate);

    }
}
