using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.CineRoomModule
{
    public interface ICineRoomRepository
    {
        void Add(CineRoom cineRoom);
        IEnumerable<CineRoom> RetriveAll();
    }
}
