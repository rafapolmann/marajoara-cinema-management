using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.UserAccountModule
{
    interface IUserAccountService
    {
        void AddUserAccount(UserAccount userAccount);

        UserAccount RetrieveUserAccountByID(int userAccountID);        
        
        IEnumerable<UserAccount> RetrieveAll();

        bool RemoveUserAccount(UserAccount userAccount);
    }
}
