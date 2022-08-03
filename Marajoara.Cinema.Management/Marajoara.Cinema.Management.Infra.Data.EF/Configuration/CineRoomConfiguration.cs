using Marajoara.Cinema.Management.Domain.CineRoomModule;
using System.Data.Entity.ModelConfiguration;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Configuration
{
    public class CineRoomConfiguration : EntityTypeConfiguration<CineRoom>
    {
        public CineRoomConfiguration()
        {
            ToTable("CineRooms");
            HasKey(cr => cr.CineRoomID).Property(cr => cr.CineRoomID).HasColumnName("CineRoomID");
            HasIndex(cr => cr.Name).IsUnique();
            Property(cr => cr.Name).IsRequired().HasColumnName("Name").HasMaxLength(1024);
            Property(cr => cr.SeatsColumn).HasColumnName("SeatsColumn");
            Property(cr => cr.SeatsRow).HasColumnName("SeatsRow");

            Ignore(cr => cr.TotalSeats);
        }
    }
}
