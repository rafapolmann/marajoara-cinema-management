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

        public int Add(UserAccount userAccountToAdd)
        {
            return DBContext.UserAccounts.Add(userAccountToAdd).UserAccountID;
        }

        public bool Delete(UserAccount userAccountToDelete)
        {
            DBContext.Entry(userAccountToDelete).State = System.Data.Entity.EntityState.Deleted;
            return true;
        }

        public bool Update(UserAccount userAccountToUpdate)
        {
            DBContext.Entry(userAccountToUpdate).State = System.Data.Entity.EntityState.Modified;
            return true;
        }

        public UserAccount Retrieve(int userAccountID)
        {
            return DBContext.UserAccounts
                            .Where(ua => ua.UserAccountID.Equals(userAccountID))
                            .FirstOrDefault();
        }

        public UserAccount RetrieveByName(string name)
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

        public IEnumerable<UserAccount> RetrieveAll()
        {
            return DBContext.UserAccounts;
        }
    }
}
