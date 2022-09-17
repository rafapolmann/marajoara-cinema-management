using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using Microsoft.EntityFrameworkCore;
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

        public bool Delete(UserAccount userAccountToDelete)
        {
            DBContext.Entry(userAccountToDelete).State = EntityState.Deleted;
            return true;
        }

        public bool Update(UserAccount userAccountToUpdate)
        {
            DBContext.Entry(userAccountToUpdate).State = EntityState.Modified;
            return true;
        }

        public UserAccount Retrieve(int userAccountID)
        {
            return DBContext.UserAccounts
                            .Where(ua => ua.UserAccountID == userAccountID)
                            .FirstOrDefault();
        }

        public UserAccount RetrieveByName(string name)
        {
            return DBContext.UserAccounts
                            .Where(ua => ua.Name == name)
                            .FirstOrDefault();
        }

        public UserAccount RetrieveByMail(string userAccountMail)
        {
            return DBContext.UserAccounts
                            .Where(ua => ua.Mail == userAccountMail)
                            .FirstOrDefault();
        }

        public IEnumerable<UserAccount> RetrieveAll()
        {
            return DBContext.UserAccounts;
        }

        public IEnumerable<UserAccount> RetrieveByAccessLevel(AccessLevel accessLevel)
        {
            return DBContext.UserAccounts.Where(u => u.Level == accessLevel);
        }
    }
}
