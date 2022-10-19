using Marajoara.Cinema.Management.Domain.SessionModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Configuration
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions");
            builder.HasKey(s => s.SessionID);
            builder.Property(s => s.SessionID).HasColumnName("SessionID");
            builder.Property(s => s.SessionDate).IsRequired().HasColumnName("SessionDate");
            builder.Property(s => s.Price).IsRequired().HasColumnName("Price");

            //FK_CineRoom
            builder.Property(s => s.CineRoomID).HasColumnName("CineRoomID");
            builder.HasOne(s => s.CineRoom).WithMany()
                   .HasForeignKey(s => s.CineRoomID).IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);


            builder.Ignore(cr => cr.EndSession);
        }
    }
}
