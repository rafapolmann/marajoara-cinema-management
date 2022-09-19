using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Configuration
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable("UserAccounts");
            builder.HasKey(ua => ua.UserAccountID);
            builder.Property(ua => ua.UserAccountID).HasColumnName("UserAccountID");
            builder.HasIndex(ua => ua.Mail).IsUnique();
            builder.Property(ua => ua.Mail).IsRequired().HasColumnName("Mail").HasMaxLength(512);
            builder.Property(ua => ua.Name).IsRequired().HasColumnName("Name").HasMaxLength(1024);
            builder.Property(ua => ua.Level).HasColumnName("AccessLevel");
            builder.Property(ua => ua.Password).HasColumnName("Password");
            builder.Property(ua => ua.Photo).HasColumnName("Photo");
        }
    }
}
