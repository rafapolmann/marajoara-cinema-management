using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IMarajoaraUnitOfWork _unitOfWork;

        public UserAccountService(IMarajoaraUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int AddAttendantUserAccount(UserAccount attendandtToAdd)
        {
            //Forces the access level to be "Attendant"
            attendandtToAdd.Level = AccessLevel.Attendant;
            return AddUserAccount(attendandtToAdd);
        }

        public int AddCustomerUserAccount(UserAccount customerToAdd)
        {
            //Forces the access level to be "Customer"
            customerToAdd.Level = AccessLevel.Customer;
            return AddUserAccount(customerToAdd);
        }

        public int AddManagerUserAccount(UserAccount managerToAdd)
        {
            //Forces the access level to be "Manager"
            managerToAdd.Level = AccessLevel.Manager;
            return AddUserAccount(managerToAdd);
        }

        private int AddUserAccount(UserAccount userAccount)
        {
            userAccount.Validate();
            _unitOfWork.UserAccounts.Add(userAccount);
            _unitOfWork.Commit();
            return userAccount.UserAccountID;
        }

        public bool RemoveUserAccount(UserAccount userAccount)
        {
            userAccount = RetrieveUserAccountByID(userAccount.UserAccountID);
            if (userAccount == null)
                throw new Exception("User account not found!");

            if (userAccount.Level == AccessLevel.Manager)
            { //If is a manager account, must check if it's the only one. Must always have 1 manager account in the DB
                var managerCount = _unitOfWork.UserAccounts.RetrieveByAccessLevel(AccessLevel.Manager).Count();
                if (managerCount == 1)
                    throw new Exception("Impossible to delete this manager!");
            }
            _unitOfWork.UserAccounts.Delete(userAccount);
            _unitOfWork.Commit();
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
