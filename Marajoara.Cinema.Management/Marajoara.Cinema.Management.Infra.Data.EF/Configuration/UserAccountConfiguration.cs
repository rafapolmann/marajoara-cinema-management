using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System.Data.Entity.ModelConfiguration;

namespace Marajoara.Cinema.Management.Infra.Data.EF.Configuration
{
    public class UserAccountConfiguration : EntityTypeConfiguration<UserAccount>
    {
        public UserAccountConfiguration()
        {
                ToTable("UserAccounts");
                HasKey(cr => cr.UserAccountID).Property(cr => cr.UserAccountID).HasColumnName("UserAccountID");
                HasIndex(cr => cr.Mail).IsUnique();
                Property(cr => cr.Mail).IsRequired().HasColumnName("Mail").HasMaxLength(512) ;
                Property(cr => cr.Name).IsRequired().HasColumnName("Name").HasMaxLength(1024);
                Property(cr => cr.Level).HasColumnName("AccessLevel");
                Property(cr => cr.Password).HasColumnName("Password");
                Property(cr => cr.Photo).HasColumnName("Photo");

        }
    }
}
