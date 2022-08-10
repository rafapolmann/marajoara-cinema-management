using System.Data.Entity;
using System.Data.Common;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Configuration;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Commom
{
    public class MarajoaraContext : DbContext
    {
        public MarajoaraContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {
            Database.SetInitializer<MarajoaraContext>(new CreateDatabaseIfNotExists<MarajoaraContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UserAccountConfiguration());
            modelBuilder.Configurations.Add(new TicketConfiguration());
            modelBuilder.Configurations.Add(new CineRoomConfiguration());
            modelBuilder.Configurations.Add(new SessionConfiguration());
            modelBuilder.Configurations.Add(new MovieConfiguration());
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<CineRoom> CineRooms { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
