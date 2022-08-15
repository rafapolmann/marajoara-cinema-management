using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.UserAccountModule
{
    public interface IUserAccountRepository
    {
        /// <summary>
        /// Add new register of UserAccount on database.
        /// </summary>
        /// <param name="userAccountToAdd">UserAccount that should be added.</param>
        void Add(UserAccount userAccountToAdd);

        /// <summary>
        /// Update UserAccount properties on database.
        /// </summary>
        /// <param name="userAccountToUpdate">An instance of UserAccount with all properties that will update on database. It should be linked with DBContext.</param>
        void Update(UserAccount userAccountToUpdate);

        /// <summary>
        /// Remove a given UserAccount on database and of the DBContext
        /// </summary>
        /// <param name="userAccountToDelete">An instance of UserAccount that will remove on database. It should be linked with DBContext.</param>
        void Delete(UserAccount userAccountToDelete);

        /// <summary>
        /// Retrieves a UserAccount with a given database ID
        /// </summary>
        /// <param name="userAccountID">ID used with condition for the search on database</param>
        /// <returns>Returns UserAccount persited on database or null.</returns>
        UserAccount Retrieve(int userAccountID);

        /// <summary>
        /// Retrieves a UserAccount with a given user name
        /// </summary>
        /// <param name="name">name used with condition for the search on database</param>
        /// <returns>Returns UserAccount persited on database or null.</returns>
        UserAccount RetrieveByName(string name);

        /// <summary>
        /// Retrieves a UserAccount with a given mail
        /// </summary>
        /// <param name="userAccountMail">e-mail used with condition for the search on database</param>
        /// <returns>Returns UserAccount persited on database or null.</returns>
        UserAccount RetrieveByMail(string userAccountMail);

        /// <summary>
        /// Retrieves the collection of UserAccounts
        /// </summary>
        /// <returns>Returns collection of the UserAccounts from database.</returns>
        IEnumerable<UserAccount> RetrieveAll();
    }
}
