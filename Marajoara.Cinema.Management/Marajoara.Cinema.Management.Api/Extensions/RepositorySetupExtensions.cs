using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Infra.Data.EF;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Marajoara.Cinema.Management.Api.Extensions
{
    public static class RepositorySetupExtensions
    {
        public static void AddRepositorySetup(this IServiceCollection services)
        {
            services.AddDbContext<MarajoaraContext>(options =>
            {
                options.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CineMarajoara;Integrated Security=SSPI;");
                options.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddDebug(); }));
            });

            services.AddScoped<IMarajoaraUnitOfWork, MarajoaraUnitOfWork>();

            services.AddScoped<ICineRoomRepository, CineRoomRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
        }
    }
}