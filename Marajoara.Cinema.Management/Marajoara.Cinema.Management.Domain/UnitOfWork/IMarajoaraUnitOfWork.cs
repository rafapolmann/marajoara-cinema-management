using Marajoara.Cinema.Management.Domain.CineRoomModule;
using System;

namespace Marajoara.Cinema.Management.Domain.UnitOfWork
{
    public interface IMarajoaraUnitOfWork : IDisposable
    {
        public ICineRoomRepository CineRooms { get; }
        void Commit();
        void CleanDb();
    }
}
