using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System;
using System.Collections.Generic;


namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IMarajoaraUnitOfWork _unitOfWork;

        public UserAccountService(IMarajoaraUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateUserAccount(string name, string email, string password, AccessLevel accessLevel)
        {
            var userToAdd = new UserAccount() { Name = name, Mail = email, Password = password, Level = accessLevel };
            _unitOfWork.UserAccounts.Add(userToAdd);
        }

        public bool RemoveUserAccount(UserAccount userAccount)
        {            
            _unitOfWork.UserAccounts.Delete(userAccount);
            //Todo: check if is a scenario that the return is not true. Otherwise, alter the method to be nonvalue-returning.
            return true;
        }

        public IEnumerable<UserAccount> RetrieveAll()
        {
            return _unitOfWork.UserAccounts.RetrieveAll();
        }

        public UserAccount RetrieveUserAccountByID(int userAccountID)
        {
            return _unitOfWork.UserAccounts.Retrieve(userAccountID);
        }
    }
}
