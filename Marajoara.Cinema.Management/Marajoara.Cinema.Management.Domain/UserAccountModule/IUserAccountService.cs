using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.UserAccountModule
{
    public interface IUserAccountService
    {
        UserAccount CreateUserAccount(string name, string email, string password, AccessLevel accessLevel);
        UserAccount RetrieveUserAccountByID(int userAccountID);
        IEnumerable<UserAccount> RetrieveAll();
        bool RemoveUserAccount(UserAccount userAccount);
    }
}
