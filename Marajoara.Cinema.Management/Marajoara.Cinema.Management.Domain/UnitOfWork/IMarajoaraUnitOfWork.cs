using System;

namespace Marajoara.Cinema.Management.Domain.UnitOfWork
{
    public interface IMarajoaraUnitOfWork : IDisposable
    {
        void Commit();
        void CleanDb();
    }
}
