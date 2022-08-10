using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Commom
{
    public class MarajoaraUnitOfWork : IMarajoaraUnitOfWork
    {
        private readonly MarajoaraContext DBContext;
        public ICineRoomRepository CineRooms { get; private set; }
        public IUserAccountRepository UserAccounts { get; private set; }

        public MarajoaraUnitOfWork(MarajoaraContext dbContext, ICineRoomRepository cineRooms, IUserAccountRepository userAccounts)
        {
            DBContext = dbContext;
            CineRooms = cineRooms;
            UserAccounts = userAccounts;
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
