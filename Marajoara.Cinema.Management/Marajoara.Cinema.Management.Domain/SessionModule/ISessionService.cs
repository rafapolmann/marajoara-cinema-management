using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.SessionModule
{
    public interface ISessionService
    {
        /// <summary>
        /// Add new session in the system.
        /// In case of null session parameter, throws exception.        
        /// Will not be possible to register a new session without a valid CineRoom and a valid Movie. (Throws exception)
        /// Will not be possible add a session in the CineRoom taht already existing a session in the same time. (Throws exception)
        /// </summary>
        /// <param name="session">Session to add.</param>
        /// <returns>Return the ID of new session registered in the system.</returns>
        int AddSession(Session session);

        /// <summary>
        /// Update all properties of a given session in the system.
        /// In case of the null session parameter or session not found, throws exception.
        /// </summary>
        /// <param name="session">Session with properties to update.</param>
        bool UpdateSession(Session session);

        /// <summary>
        /// Remove a given session of the system.
        /// In case of null session parameter or session not found, throws exception.
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
        /// Returns all sessions that are linked to a given cine room        
        /// </summary>
        /// <param name="cineRoomID">Cine room ID to search linked sessions</param>
        /// <returns>A IEnumerable of Sessions</returns>
        IEnumerable<Session> GetSessionsByCineRoom(int cineRoomID);

        /// <summary>
        /// Returns all sessions that are presenting a given movie title.
        /// </summary>
        /// <param name="movieTitle">The movie title</param>
        /// <returns>A list of Sessions</returns>
        IEnumerable<Session> GetSessionsByMovieTitle(string movieTitle);

        /// <summary>
        /// Returns the sessions for a specific date. (Full day. Ignores the time)
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
