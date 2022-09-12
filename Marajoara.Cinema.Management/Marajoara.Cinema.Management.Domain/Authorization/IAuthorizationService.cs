using Marajoara.Cinema.Management.Domain.UserAccountModule;

namespace Marajoara.Cinema.Management.Domain.Authorization
{
    public interface IAuthorizationService
    {
        public UserAccount Authorize(UserAccount userAccount);
    }
}
