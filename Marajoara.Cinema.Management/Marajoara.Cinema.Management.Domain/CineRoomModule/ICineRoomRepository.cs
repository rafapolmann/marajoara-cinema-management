using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.CineRoomModule
{
    public interface ICineRoomRepository
    {
        void Add(CineRoom cineRoomToAdd);
        void Update(CineRoom cineRoomToUpdate);
        void Delete(CineRoom cineRoomToDelete);
        CineRoom RetriveByName(string cineRoomName);
        IEnumerable<CineRoom> RetriveAll();
    }
}
