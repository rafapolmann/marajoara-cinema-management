using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.SessionModule
{
    public interface ISessionService
    {
        /// <summary>
        /// Method to get a given Session registered on the system based on database ID.
        /// </summary>
        /// <param name="id">ID used with parameter in the search.</param>
        /// <returns>Return found Session including CineRoom and Movie reference or null if doesn't exists session with this ID.</returns>
        Session GetSession(int id);

        /// <summary>
        /// Method to get all sessions registered on the system.
        /// </summary>
        /// <returns>Collection of Sessions including CineRoom and Movie reference.</returns>
        IEnumerable<Session> GetAllSessions();
    }
}
