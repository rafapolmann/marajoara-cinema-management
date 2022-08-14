using Marajoara.Cinema.Management.Domain.SessionModule;
using System.Data.Entity.ModelConfiguration;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Configuration
{
    public class SessionConfiguration : EntityTypeConfiguration<Session>
    {
        public SessionConfiguration()
        {
            ToTable("Sessions");
            HasKey(cr => cr.SessionID).Property(cr => cr.SessionID).HasColumnName("SessionID");            
            Property(cr => cr.SessionDate).IsRequired().HasColumnName("SessionDate");
            Property(cr => cr.Price).IsRequired().HasColumnName("Price");

            //FKs
            Property(cr => cr.CineRoomID).HasColumnName("CineRoomID");
            HasRequired(cr => cr.CineRoom).WithMany().HasForeignKey(o => o.CineRoomID);

            Property(cr => cr.MovieID).HasColumnName("MovieID");
            HasRequired(cr => cr.Movie).WithMany().HasForeignKey(o => o.MovieID);
        }
    }
}
