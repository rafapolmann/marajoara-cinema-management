using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.UserAccountModule
{
    public interface IUserAccountService
    {
        int AddCustomerUserAccount(UserAccount customerToAdd);
        int AddAttendantUserAccount(UserAccount attendandtToAdd);
        int AddManagerUserAccount(UserAccount managerToAdd);        
        UserAccount RetrieveUserAccountByID(int userAccountID);
        IEnumerable<UserAccount> RetrieveAll();
        bool RemoveUserAccount(UserAccount userAccount);
    }
}
