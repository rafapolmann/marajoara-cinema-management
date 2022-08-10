using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Application
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public UserAccount CreateUserAccount(string name, string email, string password, AccessLevel accessLevel)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUserAccount(UserAccount userAccount)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserAccount> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public UserAccount RetrieveUserAccountByID(int userAccountID)
        {
            throw new NotImplementedException();
        }
    }
}
