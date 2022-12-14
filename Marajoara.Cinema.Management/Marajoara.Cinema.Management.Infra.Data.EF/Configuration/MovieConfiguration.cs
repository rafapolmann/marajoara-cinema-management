using Marajoara.Cinema.Management.Domain.MovieModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");
            builder.HasKey(m => m.MovieID);
            builder.Property(m => m.MovieID).HasColumnName("MovieID");
            builder.HasIndex(m => m.Title).IsUnique();
            builder.Property(m => m.Title).IsRequired().HasColumnName("Title").HasMaxLength(1024);
            builder.Property(m => m.Poster).HasColumnName("Poster");
            builder.Property(m => m.Minutes).IsRequired().HasColumnName("Minutes");
            builder.Property(m => m.Is3D).IsRequired().HasColumnName("Is3D");
            builder.Property(m => m.IsOriginalAudio).IsRequired().HasColumnName("IsOriginalAudio");

            //FK Session
            builder.HasMany(m => m.Sessions).WithOne()
                   .HasForeignKey(s => s.MovieID).IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
