using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Infra.Data.EF;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ninject;

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
            kernel.Bind<MarajoaraContext>().ToSelf()
                                           .InSingletonScope()
                                           .WithConstructorArgument("options", GetDbContextOptionsForCurrentRequest());
            //.InScope(c => c.Request);


            kernel.Bind<IMarajoaraUnitOfWork>().To<MarajoaraUnitOfWork>();
        }

        private static DbContextOptions GetDbContextOptionsForCurrentRequest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MarajoaraContext>();

            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CineMarajoara;Integrated Security=SSPI;");
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddDebug(); }));
            var options = optionsBuilder.Options;

            return options;
        }
    }
}
