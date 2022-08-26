using System;
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

        bool UpdateSession(Session session);

        /// <summary>
        /// Remove a given session of the system.
        /// In case of the session parameter is null or session will not find in system, will throw exception.
        /// </summary>
        /// <param name="session">Session to remove.</param>
        /// <returns>Return true if session was removed with success.</returns>
        bool RemoveSession(Session session);

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

        /// <summary>
        /// Returns all sessions that are linked a given cine room
        /// </summary>
        /// <param name="cineRoomID">Cine room ID to search linked sessions</param>
        /// <returns>A list of Sessions</returns>
        IEnumerable<Session> GetSessionsByCineRoom(int cineRoomID);

        /// <summary>
        /// Returns all sessions that are presenting a given movie title
        /// </summary>
        /// <param name="movieTitle">The movie title</param>
        /// <returns>A list of Sessions</returns>
        IEnumerable<Session> GetSessionsByMovieTitle(string movieTitle);

        /// <summary>
        /// Returns the sessions for a specific date
        /// </summary>
        /// <param name="sessionDate">Date of the sessions</param>
        /// <returns>A list of Session on a specifc day.</returns>
        IEnumerable<Session> GetSessionsByDate(DateTime sessionDate);

        /// <summary>
        /// Returns the sessions between dates informed.
        /// </summary>
        /// <param name="initialDate">The start date</param>
        /// <param name="finalDate">The end date</param>
        /// <returns>A list of Session in the date range</returns>
        IEnumerable<Session> GetSessionsByDateRange(DateTime initialDate, DateTime finalDate);
    }
}
