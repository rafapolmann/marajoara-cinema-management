using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.CineRoomModule
{
    public interface ICineRoomService
    {
        int AddCineRoom(CineRoom cineRoom);
        void UpdateCineRoom(CineRoom cineRoom);
        bool RemoveCineRoom(CineRoom cineRoom);
        IEnumerable<CineRoom> GetAllCineRooms();
        CineRoom GetCineRoom(int id);
        CineRoom GetCineRoom(string cineRoomName);
    }
}
