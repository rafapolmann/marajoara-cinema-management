using System.Collections.Generic;
using System.IO;

namespace Marajoara.Cinema.Management.Domain.UserAccountModule
{
    public interface IUserAccountService
    {
        /// <summary>
        /// Adds a new customer. (AccessLevel.Customer)
        /// If its not a valid UserAccount throws Exception. 
        /// </summary>
        /// <param name="customerToAdd">UserAccount to add</param>
        /// <returns>Id of the new UserAccount</returns>
        int AddCustomerUserAccount(UserAccount customerToAdd);

        /// <summary>
        /// Adds a new attendant. (AccessLevel.Attendant)
        /// If its not a valid UserAccount throws Exception. 
        /// </summary>
        /// <param name="attendandtToAdd">UserAccount to add</param>
        /// <returns>Id of the new UserAccount</returns>
        int AddAttendantUserAccount(UserAccount attendandtToAdd);

        /// <summary>
        /// Adds a new manager. (AccessLevel.Manager)
        /// If its not a valid UserAccount throws Exception. 
        /// </summary>
        /// <param name="managerToAdd">UserAccount to add</param>
        /// <returns>Id of the new UserAccount</returns>
        int AddManagerUserAccount(UserAccount managerToAdd);

        /// <summary>
        /// Gets the UserAccount with a given ID. 
        /// </summary>
        /// <param name="userAccountID">Id of the User Account</param>
        /// <returns>Returns user account or null if not found.</returns>
        UserAccount GetUserAccountByID(int userAccountID);

        /// <summary>
        /// Get all User Account.
        /// </summary>
        /// <returns>IEnumerable of UserAccount</returns>
        IEnumerable<UserAccount> GetAll();

        /// <summary>
        /// Removes a given user account. 
        /// If user account not found, throws exception. 
        /// If its the only manager, throws exception. 
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns>If sucess: true.</returns>
        bool RemoveUserAccount(UserAccount userAccount);

        /// <summary>
        /// Will set an image to user account photo property in the system.
        /// Validates the file format: Must be PNG, JPG or BMP and Max image size of 500kb.
        /// If UserAccountID not found in database, throws an Exception.
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
        /// UserAccount photo data will be setted to null in the database.
        /// If UserAccountID not found in database, throws an Exception.
        /// </summary>
        /// <param name="userAccountID">ID used as parameter in the command.</param>
        /// <returns>Returns true if process succeeds.</returns>
        bool DeleteUserAccountPhoto(int userAccountID);

        /// <summary>
        /// Will update basic properties (name and access level) to user account in the system.
        /// If UserAccountID not found in database, throws an Exception.
        /// </summary>
        /// <param name="userAccountID">ID used as parameter in the command.</param>
        /// <returns>Returns true if process will succeed.</returns>
        bool UpdateUserAccountBasicProperties(UserAccount userAccountToUpdate);

        /// <summary>
        /// Will reset UserAccount password to system default.
        /// If UserAccountID not found in database, throws an Exception.
        /// </summary>
        /// <param name="userAccount">UserAccount that should reset password.</param>
        /// <returns>Returns true if process will succeed.</returns>
        bool ResetUserAccountPassword(UserAccount userAccount);

        /// <summary>
        /// Updates the password of a user account.
        /// If UserAccountID not found in database or new password is not valid, throws an Exception.
        /// </summary>
        /// <param name="userAccount">UserAccount that should reset password.</param>
        /// <param name="newPassword">New password value.</param>
        /// <returns>Returns true if process will succeed.</returns>
        bool ChangeUserAccountPassword(UserAccount userAccount, string newPassword);
    }
}
