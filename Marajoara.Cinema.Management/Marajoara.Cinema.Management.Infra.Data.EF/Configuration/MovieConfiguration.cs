using Marajoara.Cinema.Management.Domain.MovieModule;
using System.Data.Entity.ModelConfiguration;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Configuration
{
    public class MovieConfiguration : EntityTypeConfiguration<Movie>
    {
        public MovieConfiguration()
        {
            ToTable("Movies");
            HasKey(cr => cr.MovieID).Property(cr => cr.MovieID).HasColumnName("MovieID");
            HasIndex(cr => cr.Title).IsUnique();
            Property(cr => cr.Title).IsRequired().HasColumnName("Title").HasMaxLength(1024);
            Property(cr => cr.Poster).HasColumnName("Poster");
            Property(cr => cr.Duration).IsRequired().HasColumnName("Duration");
            Property(cr => cr.Is3D).IsRequired().HasColumnName("Is3D");
            Property(cr => cr.IsOrignalAudio).IsRequired().HasColumnName("IsOrignalAudio");
            
        }
    }
}
