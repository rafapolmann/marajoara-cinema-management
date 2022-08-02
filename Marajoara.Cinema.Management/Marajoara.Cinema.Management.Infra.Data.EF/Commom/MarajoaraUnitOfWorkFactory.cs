using Marajoara.Cinema.Management.Domain.UnitOfWork;
using System.Data.SqlClient;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Commom
{
    public class MarajoaraUnitOfWorkFactory : IMarajoaraUnitOfWorkFactory
    {
        private MarajoaraUnitOfWork UoW;

        public MarajoaraUnitOfWorkFactory(string connectionString)
        {
            MarajoaraContext DBContext = new MarajoaraContext(new SqlConnection(connectionString));
            UoW = new MarajoaraUnitOfWork(DBContext);
        }
        public IMarajoaraUnitOfWork Create()
        {
            return UoW;
        }
    }
}
