using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.CineRoomModule
{
    public interface ICineRoomService
    {
        /// <summary>
        /// Add new cine room in the system.
        /// In case of the cine room parameter is null, will throw exception.
        /// Will not possible to register a new cine room with already existing name.
        /// </summary>
        /// <param name="cineRoom">Cine room to add.</param>
        /// <returns>Return the ID of new cine room registered in the system.</returns>
        int AddCineRoom(CineRoom cineRoom);

        /// <summary>
        /// Update all properties of a given cine room in the system.
        /// In case of the cine room parameter is null or cine room will not find in system, will throw exception.
        /// </summary>
        /// <param name="cineRoom">Cine room with properties to update.</param>
        bool UpdateCineRoom(CineRoom cineRoom);

        /// <summary>
        /// Remove a given cine room of the system.
        /// In case of the cine room parameter is null or cine room will not find in system, will throw exception.
        /// </summary>
        /// <param name="cineRoom">Cine room to remove.</param>
        /// <returns>Return true if cine room was removed with success. </returns>
        bool RemoveCineRoom(CineRoom cineRoom);

        /// <summary>
        /// Method to get all CineRooms registered on the system.
        /// </summary>
        /// <returns>Collection of CineRooms.</returns>
        IEnumerable<CineRoom> GetAllCineRooms();

        /// <summary>
        /// Method to get a given CineRoom registered on the system based on database ID.
        /// </summary>
        /// <param name="id">ID used with parameter in the search.</param>
        /// <returns>Return found CineRoom or null if doesn't exists this ID.</returns>
        CineRoom GetCineRoom(int id);

        /// <summary>
        /// Method to get a given CineRoom registered on the system based on cine room name.
        /// </summary>
        /// <param name="cineRoomName">Name used with parameter in the search.</param>
        /// <returns>Return found CineRoom or null if doesn't exists any cine room with this name.</returns>
        CineRoom GetCineRoom(string cineRoomName);
    }
}
