using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Infra.Data.EF
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly MarajoaraContext DBContext;

        public UserAccountRepository(MarajoaraContext dbContext)
        {
            DBContext = dbContext;
        }

        public void Add(UserAccount userAccountToAdd)
        {
            DBContext.UserAccounts.Add(userAccountToAdd);
        }

        public void Delete(UserAccount userAccountToDelete)
        {
            DBContext.Entry(userAccountToDelete).State = System.Data.Entity.EntityState.Deleted;
        }

        public IEnumerable<UserAccount> RetriveAll()
        {
            return DBContext.UserAccounts;
        }

        public UserAccount RetriveByFullName(string fullName)
        {
            return DBContext.UserAccounts
                            .Where(ua => ua.FullName.Equals(fullName, StringComparison.InvariantCultureIgnoreCase))
                            .FirstOrDefault();
        }

        public UserAccount RetriveByMail(string email)
        {
            return DBContext.UserAccounts
                            .Where(ua => ua.Mail.Equals(email, StringComparison.InvariantCultureIgnoreCase))
                            .FirstOrDefault();
        }

        public void Update(UserAccount userAccountToUpdate)
        {
            DBContext.Entry(userAccountToUpdate).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
