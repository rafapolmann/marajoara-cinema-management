using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marajoara.Cinema.Management.Infra.Data.EF
{
    public class CineRoomRepository : ICineRoomRepository
    {
        private readonly MarajoaraContext DBContext;

        public CineRoomRepository(MarajoaraContext dbContext)
        {
            DBContext = dbContext;
        }

        public void Add(CineRoom cineRoomToAdd)
        {
            DBContext.CineRooms.Add(cineRoomToAdd);
        }

        public void Update(CineRoom cineRoomToUpdate)
        {
            DBContext.Entry(cineRoomToUpdate).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(CineRoom cineRoomToDelete)
        {
            DBContext.Entry(cineRoomToDelete).State = System.Data.Entity.EntityState.Deleted;
        }

        public CineRoom Retrieve(int cineRoomID)
        {
            return DBContext.CineRooms
                            .Where(cr => cr.CineRoomID.Equals(cineRoomID))
                            .FirstOrDefault();
        }

        public CineRoom RetrieveByName(string cineRoomName)
        {
            return DBContext.CineRooms
                            .Where(cr => cr.Name.Equals(cineRoomName, StringComparison.InvariantCultureIgnoreCase))
                            .FirstOrDefault();
        }

        public IEnumerable<CineRoom> RetrieveAll()
        {
            return DBContext.CineRooms;
        }
    }
}
