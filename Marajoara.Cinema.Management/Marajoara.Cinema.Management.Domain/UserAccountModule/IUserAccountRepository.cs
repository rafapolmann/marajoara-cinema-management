using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Domain.UserAccountModule
{
    public interface IUserAccountRepository
    {
        void Add(UserAccount userAccountToAdd);
        void Update(UserAccount userAccountToUpdate);
        void Delete(UserAccount userAccountToDelete);
        IEnumerable<UserAccount> RetriveAll();

        /// <summary>
        /// Retrieves a UserAccount with a given mail
        /// </summary>
        /// <param name="userAccountID"></param>
        /// <returns></returns>
        UserAccount RetrieveByMail(string userAccountMail);
    }
}
