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
            throw new NotImplementedException();
        }

        public void Delete(UserAccount userAccountToDelete)
        {
            throw new NotImplementedException();
        }

        public UserAccount RetrieveByMail(string userAccountMail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserAccount> RetriveAll()
        {
            throw new NotImplementedException();
        }

        public void Update(UserAccount userAccountToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
