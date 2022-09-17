using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using Microsoft.EntityFrameworkCore;
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
            DBContext.Entry(cineRoomToUpdate).State = EntityState.Modified;
        }

        public void Delete(CineRoom cineRoomToDelete)
        {
            DBContext.Entry(cineRoomToDelete).State = EntityState.Deleted;
        }

        public CineRoom Retrieve(int cineRoomID)
        {
            return DBContext.CineRooms
                            .Where(cr => cr.CineRoomID == cineRoomID)
                            .FirstOrDefault();
        }

        public CineRoom RetrieveByName(string cineRoomName)
        {
            return DBContext.CineRooms
                            .Where(cr => cr.Name == cineRoomName)
                            .FirstOrDefault();
        }

        public IEnumerable<CineRoom> RetrieveAll()
        {
            return DBContext.CineRooms;
        }
    }
}
