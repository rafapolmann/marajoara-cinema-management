using System.Collections.Generic;
using System.IO;

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

        /// <summary>
        /// Sets the User Accouunt Photo
        /// </summary>
        /// <param name="userAccountID"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        bool UpdateUserAccountPhoto(int userAccountID, Stream stream);
        byte[] GetUserAccountPhoto(int userAccountID);
        bool DeleteUserAccountPhoto(int userAccountID);
    }
}
