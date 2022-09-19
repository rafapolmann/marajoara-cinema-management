using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Configuration
{
    public class CineRoomConfiguration : IEntityTypeConfiguration<CineRoom>
    {
        public void Configure(EntityTypeBuilder<CineRoom> builder)
        {
            builder.ToTable("CineRooms");
            builder.HasKey(cr => cr.CineRoomID);
            builder.Property(cr => cr.CineRoomID).HasColumnName("CineRoomID");
            builder.HasIndex(cr => cr.Name).IsUnique();
            builder.Property(cr => cr.Name).IsRequired().HasColumnName("Name").HasMaxLength(1024);
            builder.Property(cr => cr.SeatsColumn).HasColumnName("SeatsColumn");
            builder.Property(cr => cr.SeatsRow).HasColumnName("SeatsRow");

            builder.Ignore(cr => cr.TotalSeats);
        }
    }
}
