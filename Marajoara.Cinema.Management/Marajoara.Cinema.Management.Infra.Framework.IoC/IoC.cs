using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Infra.Data.EF;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using Ninject;
using System.Data.SqlClient;

namespace Marajoara.Cinema.Management.Infra.Framework.IoC
{
    public class IoC
    {
        private static readonly object obj = new object();
        private static readonly IKernel _kernel = new StandardKernel();
        private static IoC _instance;

        public IKernel Kernel { get { return _kernel; } }

        private IoC()
        {
            DatabaseSetup();
            RepositoriesSetup();
        }
        private void RepositoriesSetup()
        {
            _kernel.Bind<ICineRoomRepository>().To<CineRoomRepository>();
        }

        private void DatabaseSetup()
        {
            SqlConnectionStringBuilder _connectionStringBuilder = new SqlConnectionStringBuilder
            {
                InitialCatalog = "CineMarajoara",
                DataSource = "(localdb)\\mssqllocaldb"
            };

            _kernel.Bind<MarajoaraContext>().ToSelf()
                                            .InSingletonScope()
                                            .WithConstructorArgument("dbConnection", new SqlConnection(_connectionStringBuilder.ConnectionString));

            _kernel.Bind<IMarajoaraUnitOfWork>().To<MarajoaraUnitOfWork>();
        }

        public static IoC GetInstance()
        {
            lock (obj)
            {
                if (_instance == null)
                    _instance = new IoC();

                return _instance;
            }
        }

        public T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
