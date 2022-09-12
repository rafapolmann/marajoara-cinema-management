using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Infra.Data.EF;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using Ninject;
using System.Data.SqlClient;

namespace Marajoara.Cinema.Management.Infra.Framework.IoC.Extensions
{
    public static class RepositorySetupExtensions
    {
        public static void BindRepositorySetup(this IKernel kernel)
        {
            kernel.DatabaseSetup();

            kernel.Bind<ICineRoomRepository>().To<CineRoomRepository>();
            kernel.Bind<IMovieRepository>().To<MovieRepository>();
            kernel.Bind<ISessionRepository>().To<SessionRepository>();
            kernel.Bind<ITicketRepository>().To<TicketRepository>();
            kernel.Bind<IUserAccountRepository>().To<UserAccountRepository>();
        }

        private static void DatabaseSetup(this IKernel kernel)
        {
            SqlConnectionStringBuilder _connectionStringBuilder = new SqlConnectionStringBuilder
            {
                InitialCatalog = "CineMarajoara",
                DataSource = "(localdb)\\mssqllocaldb"
            };

            kernel.Bind<MarajoaraContext>().ToSelf()
                                            .InSingletonScope()
                                            .WithConstructorArgument("dbConnection", new SqlConnection(_connectionStringBuilder.ConnectionString));

            kernel.Bind<IMarajoaraUnitOfWork>().To<MarajoaraUnitOfWork>();
        }
    }
}
