using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.SessionModule
{
    public interface ISessionService
    {
        /// <summary>
        /// Add new session in the system.
        /// In case of the session parameter is null, will throw exception.
        /// Will not possible to register a new session without a valid CineRoom and a valid Movie.
        /// Will not possible add a session in the CineRoom taht already existing a session in the same time.
        /// </summary>
        /// <param name="movie">Session to add.</param>
        /// <returns>Return the ID of new session registered in the system.</returns>
        int AddSession(Session session);

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
