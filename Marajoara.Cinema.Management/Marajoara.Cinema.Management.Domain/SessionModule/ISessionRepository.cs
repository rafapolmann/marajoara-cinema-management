using Marajoara.Cinema.Management.Domain.CineRoomModule;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.SessionModule
{
    public interface ISessionRepository
    {
        /// <summary>
        /// Add new register of Session on database.
        /// </summary>
        /// <param name="sessionToAdd">Session that should be added.</param>
        void Add(Session sessionToAdd);

        /// <summary>
        /// Update Session properties on database.
        /// </summary>
        /// <param name="sessionToUpdate">An instance of Session with all properties that will update on database. It should be linked with DBContext.</param>
        void Update(Session sessionToUpdate);

        /// <summary>
        /// Remove a given Session on database and of the DBContext
        /// </summary>
        /// <param name="sessionToDelete">An instance of Session that will remove on database. It should be linked with DBContext.</param>
        void Delete(Session sessionToDelete);

        /// <summary>
        /// Retrieves the collection of Sessions
        /// </summary>
        /// <returns>Returns collection of the Sessions from database.</returns>
        IEnumerable<Session> RetrieveAll();

        /// <summary>
        /// Retrieves a Session with a given database ID
        /// </summary>
        /// <param name="sessionID">ID used with condition for the search on database</param>
        /// <returns>Returns Session persited on database or null.</returns>
        Session Retrieve(int sessionID);

        /// <summary>
        /// Returns all sessions that are linked a given cine room
        /// </summary>
        /// <param name="cineRoom">Cine room to search linked sessions</param>
        /// <returns>A list of Sessions</returns>
        IEnumerable<Session> RetrieveByCineRoom(CineRoom cineRoom);

        /// <summary>
        /// Returns all sessions that are presenting a given movie title
        /// </summary>
        /// <param name="movieTitle">The movie title</param>
        /// <returns>A list of Sessions</returns>
        IEnumerable<Session> RetrieveByMovieTitle(string movieTitle);

        /// <summary>
        /// Returns the sessions for a specific date
        /// </summary>
        /// <param name="sessionDate">Date of the sessions</param>
        /// <returns>A list of Session</returns>
        IEnumerable<Session> RetrieveByDate(DateTime sessionDate);

        /// <summary>
        /// Returns the sessions in between the informed dates
        /// </summary>
        /// <param name="minSessionDate">The start date</param>
        /// <param name="lastSessionDate">The end date</param>
        /// <returns>A list of Session</returns>
        IEnumerable<Session> RetrieveByDate(DateTime minSessionDate, DateTime lastSessionDate);

        /// <summary>
        /// Returns the sessions for a specific date and in a specific cine room
        /// </summary>
        /// <param name="sessionDate">Date of the sessions</param>
        /// <param name="cineRoomID">Cine room ID to search</param>
        /// <returns>A list of Session in the date and at cine room </returns>
        IEnumerable<Session> RetrieveByDateAndCineRoom(DateTime sessionDate, int cineRoomID);
    }
}
