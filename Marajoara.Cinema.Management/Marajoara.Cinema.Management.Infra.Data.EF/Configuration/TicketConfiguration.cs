using Marajoara.Cinema.Management.Domain.TicketModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(t => t.TicketID);
            builder.Property(t => t.TicketID).HasColumnName("TicketID");
            builder.HasIndex(t => t.Code).IsUnique();
            builder.Property(t => t.Code).IsRequired().HasColumnName("TicketCode");
            builder.Property(t => t.SeatNumber).IsRequired().HasColumnName("SeatNumber");
            builder.Property(t => t.Used).HasColumnName("Used");

            //FK_UserAccount
            builder.Property(t => t.UserAccountID).HasColumnName("UserAccountID");
            builder.HasOne(t => t.UserAccount).WithMany()
                   .HasForeignKey(t => t.UserAccountID).IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            //FK_Session
            builder.Property(t => t.SessionID).HasColumnName("SessionID");
            builder.HasOne(t => t.Session).WithMany()
                   .HasForeignKey(t => t.SessionID).IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
