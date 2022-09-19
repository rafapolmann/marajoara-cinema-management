using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Commom
{
    public class MarajoaraContext : DbContext
    {
        public MarajoaraContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new CineRoomConfiguration());
            modelBuilder.ApplyConfiguration(new SessionConfiguration());
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<CineRoom> CineRooms { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
