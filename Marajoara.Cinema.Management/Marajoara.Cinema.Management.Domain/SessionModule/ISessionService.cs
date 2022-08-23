using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.SessionModule
{
    public interface ISessionService
    {
        /// <summary>
        /// Method to get all sessions registered on the system.
        /// </summary>
        /// <returns>Collection of Sessions including CineRoom and Movie reference.</returns>
        IEnumerable<Session> GetAllSessions();
    }
}
