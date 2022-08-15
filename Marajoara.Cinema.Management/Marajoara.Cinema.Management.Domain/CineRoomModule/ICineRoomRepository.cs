using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.CineRoomModule
{
    public interface ICineRoomRepository
    {
        /// <summary>
        /// Add new register of CineRoom on database.
        /// </summary>
        /// <param name="cineRoomToAdd">CineRoom that should be added.</param>
        void Add(CineRoom cineRoomToAdd);

        /// <summary>
        /// Update CineRoom properties on database.
        /// </summary>
        /// <param name="cineRoomToUpdate">An instance of CineRoom with all properties that will update on database. It should be linked with DBContext.</param>
        void Update(CineRoom cineRoomToUpdate);

        /// <summary>
        /// Remove a given CineRoom on database and of the DBContext
        /// </summary>
        /// <param name="cineRoomToDelete">An instance of CineRoom that will remove on database. It should be linked with DBContext.</param>
        void Delete(CineRoom cineRoomToDelete);

        /// <summary>
        /// Retrieves a CineRoom with a given database ID
        /// </summary>
        /// <param name="cineRoomID">ID used with condition for the search on database</param>
        /// <returns>Returns CineRoom persited on database or null.</returns>
        CineRoom Retrieve(int cineRoomID);

        /// <summary>
        /// Retrieves a CineRoom with a given cine room name
        /// </summary>
        /// <param name="cineRoomName">name used with condition for the search on database</param>
        /// <returns>Returns CineRoom persited on database or null.</returns>
        CineRoom RetrieveByName(string cineRoomName);

        /// <summary>
        /// Retrieves the collection of CineRooms
        /// </summary>
        /// <returns>Returns collection of the CineRooms from database.</returns>
        IEnumerable<CineRoom> RetrieveAll();
    }
}
