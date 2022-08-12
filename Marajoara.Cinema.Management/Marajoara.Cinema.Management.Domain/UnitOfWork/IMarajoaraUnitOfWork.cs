using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System;

namespace Marajoara.Cinema.Management.Domain.UnitOfWork
{
    public interface IMarajoaraUnitOfWork : IDisposable
    {
        public IUserAccountRepository UserAccounts { get; }
        public ICineRoomRepository CineRooms { get; }
        public IMovieRepository Movies { get; }
        public ISessionRepository Sessions { get; }
        public ITicketRepository Tickets { get; }
        void Commit();
        void CleanDb();
    }
}
