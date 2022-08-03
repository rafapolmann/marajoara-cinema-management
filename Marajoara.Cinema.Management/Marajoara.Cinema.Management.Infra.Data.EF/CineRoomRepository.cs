using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Infra.Data.EF
{
    public class CineRoomRepository : ICineRoomRepository
    {
        private readonly MarajoaraContext DBContext;

        public CineRoomRepository(MarajoaraContext dbContext)
        {
            DBContext = dbContext;
        }

        public void Add(CineRoom cineRoom)
        {
            DBContext.CineRooms.Add(cineRoom);
        }

        public IEnumerable<CineRoom> RetriveAll()
        {
            return DBContext.CineRooms;
        }
    }
}
