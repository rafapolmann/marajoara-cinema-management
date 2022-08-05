using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.CineRoomModule
{
    internal interface ICineRoomService
    {
        void AddCineRoom(CineRoom cineRoom);

        IEnumerable<CineRoom> RetrieveAll();

        bool RemoveCineRoom(CineRoom cineRoom);
        
    }
}
