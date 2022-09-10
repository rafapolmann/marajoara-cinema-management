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
        /// Will set an image to user account photo property in the system.
        /// Valid formats for Stream are PNG, JPG and BMP. Max image size is 500kb.
        /// If UserAccountID will not find on database, will throw an Exception.
        /// </summary>
        /// <param name="userAccountID">ID used as parameter in the command.</param>
        /// <param name="stream">Data stream containing a image for movie poster</param>
        /// <returns>Returns true if process will succeed.</returns>
        bool UpdateUserAccountPhoto(int userAccountID, Stream stream);

        /// <summary>
        /// Return user account photo image data.
        /// </summary>
        /// <param name="userAccountID">ID used as parameter in the search.</param>
        /// <returns>Byte array data of photo persisted on system.</returns>
        byte[] GetUserAccountPhoto(int userAccountID);

        /// <summary>
        /// Removes user account photo data from system.
        /// UserAccount photo data will be setted to null on database.
        /// If UserAccountID will not find on database, will throw an Exception.
        /// </summary>
        /// <param name="userAccountID">ID used as parameter in the command.</param>
        /// <returns>Returns true if process will succeed.</returns>
        bool DeleteUserAccountPhoto(int userAccountID);
    }
}
