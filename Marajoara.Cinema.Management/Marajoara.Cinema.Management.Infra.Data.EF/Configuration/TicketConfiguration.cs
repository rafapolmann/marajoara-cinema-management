using Marajoara.Cinema.Management.Domain.TicketModule;
using System.Data.Entity.ModelConfiguration;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Configuration
{
    public class TicketConfiguration : EntityTypeConfiguration<Ticket>
    {
        public TicketConfiguration()
        {
            ToTable("Tickets");
            HasKey(cr => cr.TicketID).Property(cr => cr.TicketID).HasColumnName("TicketID");
            HasIndex(cr => cr.Code).IsUnique();
            Property(cr => cr.Code).IsRequired().HasColumnName("TicketCode");
            Property(cr => cr.SeatNumber).IsRequired().HasColumnName("SeatNumber");            
            
            //FKs
            Property(cr => cr.UserAccountID).HasColumnName("UserAccountID");
            HasRequired(cr => cr.UserAccount).WithMany().HasForeignKey(o=> o.UserAccountID);
            
            Property(cr => cr.SessionID).HasColumnName("SessionID");
            HasRequired(cr => cr.Session).WithMany().HasForeignKey(o=> o.SessionID);


        }
    }
}
