using Marajoara.Cinema.Management.Domain.UnitOfWork;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Commom
{
    public class MarajoaraUnitOfWork : IMarajoaraUnitOfWork
    {
        private readonly MarajoaraContext _DBContext;
        public MarajoaraUnitOfWork(MarajoaraContext DBContext)
        {
            _DBContext = DBContext;
        }

        public void CleanDb()
        {
            throw new System.NotImplementedException();
        }

        public void Commit()
        {
            _DBContext.SaveChanges();
        }

        public void Dispose()
        {
            _DBContext.Dispose();
        }
    }
}
