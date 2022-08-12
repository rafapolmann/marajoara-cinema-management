using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public UserAccount RetriveByName(string name)
        {
            return DBContext.UserAccounts
                            .Where(ua => ua.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                            .FirstOrDefault();
        }

        public UserAccount RetrieveByMail(string userAccountMail)
        {
            return DBContext.UserAccounts
                            .Where(ua => ua.Mail.Equals(userAccountMail, StringComparison.InvariantCultureIgnoreCase))
                            .FirstOrDefault();
        }

        public void Update(UserAccount userAccountToUpdate)
        {
            DBContext.Entry(userAccountToUpdate).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
