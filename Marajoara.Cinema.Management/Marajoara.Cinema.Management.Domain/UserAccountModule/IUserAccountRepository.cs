using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.UserAccountModule
{
    public interface IUserAccountRepository
    {
        void Add(UserAccount userAccountToAdd);
        void Update(UserAccount userAccountToUpdate);
        void Delete(UserAccount userAccountToDelete);
        UserAccount RetriveByFullName(string fullName);
        UserAccount RetriveByMail(string email);
        IEnumerable<UserAccount> RetriveAll();
    }
}
