using Marajoara.Cinema.Management.Domain.UserAccountModule;

namespace Marajoara.Cinema.Management.Domain.Authorization
{
    public interface IAuthorizationService
    {
        /// <summary>
        /// Used for login. If valid password and mail, returns the Database useraccount. 
        /// If failed login, throws exception.
        /// </summary>
        /// <param name="userAccount">UserAccount with mail and password</param>
        /// <returns>UserAccount in database</returns>
        public UserAccount Authorize(UserAccount userAccount);
    }
}
