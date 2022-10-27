using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Infra.Data.EF;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Marajoara.Cinema.Management.Api.Extensions
{
    public static class RepositorySetupExtensions
    {
        public static void AddRepositorySetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MarajoaraContext>(options =>
            {
                options.UseSqlServer(GetDatabaseConnectionString(configuration));
                options.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddDebug(); }));
            });

            services.AddScoped<IMarajoaraUnitOfWork, MarajoaraUnitOfWork>();

            services.AddScoped<ICineRoomRepository, CineRoomRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
        }

        private static string GetDatabaseConnectionString(IConfiguration configuration)
        {
            string serverDbHostName = Environment.GetEnvironmentVariable("SQLCINE_HOSTNAME");

            if (string.IsNullOrEmpty(serverDbHostName))
                return configuration.GetConnectionString("MarajoaraDb");//from appsettings.json

            string dbUser = Environment.GetEnvironmentVariable("SQLCINE_UserName");
            string dbUserPassword = Environment.GetEnvironmentVariable("SQLCINE_PASSWORD");
            string dbName = Environment.GetEnvironmentVariable("SQLCINE_DatabaseName");

            return $"Data Source={serverDbHostName};Database={dbName};User Id={dbUser};Password={dbUserPassword};";
        }
    }
}