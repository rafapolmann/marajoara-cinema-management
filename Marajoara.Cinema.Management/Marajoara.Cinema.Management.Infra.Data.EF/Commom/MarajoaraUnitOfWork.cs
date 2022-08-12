using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Commom
{
    public class MarajoaraUnitOfWork : IMarajoaraUnitOfWork
    {
        private readonly MarajoaraContext DBContext;
        public ICineRoomRepository CineRooms { get; private set; }
        public IMovieRepository Movies { get; private set; }
        public ISessionRepository Sessions { get; private set; }
        public ITicketRepository Tickets { get; private set; }
        public IUserAccountRepository UserAccounts { get; private set; }

        public MarajoaraUnitOfWork(MarajoaraContext dbContext,
                                   ICineRoomRepository cineRooms,
                                   IMovieRepository movies,
                                   ISessionRepository sessions,
                                   ITicketRepository tickets,
                                   IUserAccountRepository userAccounts)
        {
            DBContext = dbContext;
            CineRooms = cineRooms;
            Movies = movies;
            Sessions = sessions;
            Tickets = tickets;
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
