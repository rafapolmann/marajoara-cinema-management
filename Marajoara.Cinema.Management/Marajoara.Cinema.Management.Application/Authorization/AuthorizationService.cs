using Marajoara.Cinema.Management.Domain.Authorization;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System;

namespace Marajoara.Cinema.Management.Application.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IMarajoaraUnitOfWork _unitOfWork;

        public AuthorizationService(IMarajoaraUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }

        public UserAccount Authorize(UserAccount userAccount)
        {
            var dbUser = _unitOfWork.UserAccounts.RetrieveByMail(userAccount.Mail);
            if (dbUser != null && dbUser.Password == userAccount.Password)
                return dbUser;

            throw new Exception("Invalid login info");            
        }
    }
}
