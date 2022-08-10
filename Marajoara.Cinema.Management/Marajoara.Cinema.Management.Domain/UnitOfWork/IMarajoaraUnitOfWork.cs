using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System;

namespace Marajoara.Cinema.Management.Domain.UnitOfWork
{
    public interface IMarajoaraUnitOfWork : IDisposable
    {
        public IUserAccountRepository UserAccounts { get; }
        public ICineRoomRepository CineRooms { get; }
        void Commit();
        void CleanDb();
    }
}
