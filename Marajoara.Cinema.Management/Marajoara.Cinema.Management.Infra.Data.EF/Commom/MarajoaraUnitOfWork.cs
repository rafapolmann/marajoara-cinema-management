using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Commom
{
    public class MarajoaraUnitOfWork : IMarajoaraUnitOfWork
    {
        private readonly MarajoaraContext DBContext;
        public ICineRoomRepository CineRooms { get; private set; }

        public MarajoaraUnitOfWork(MarajoaraContext dbContext, ICineRoomRepository cineRooms)
        {
            DBContext = dbContext;
            CineRooms = cineRooms;
        }

        public void CleanDb()
        {
            throw new System.NotImplementedException();
        }

        public void Commit()
        {
            DBContext.SaveChanges();
        }

        public void Dispose()
        {
            DBContext.Dispose();
        }
    }
}
