using Marajoara.Cinema.Management.Domain.Common;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IMarajoaraUnitOfWork _unitOfWork;
        private readonly IFileImageService _fileImageService;
        
        public UserAccountService(IMarajoaraUnitOfWork unitOfWork, IFileImageService  fileImageService)
        {
            _unitOfWork = unitOfWork;
            _fileImageService = fileImageService;
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
            userAccount = GetUserAccountByID(userAccount.UserAccountID);
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

        public IEnumerable<UserAccount> GetAll()
        {
            return _unitOfWork.UserAccounts.RetrieveAll();
        }

        public UserAccount GetUserAccountByID(int userAccountID)
        {
            return _unitOfWork.UserAccounts.Retrieve(userAccountID);
        }

        public bool UpdateUserAccountPhoto(int userAccountID, Stream stream)
        {
            UserAccount userAccountOnDB = _unitOfWork.UserAccounts.Retrieve(userAccountID);
            if (userAccountOnDB == null)
                throw new Exception($"UserAccount to update not found.");

            userAccountOnDB.Photo = _fileImageService.GetImageBytes(stream);

            _unitOfWork.UserAccounts.Update(userAccountOnDB);
            _unitOfWork.Commit();

            return true;
        }

        public byte[] GetUserAccountPhoto(int userAccountID)
        {
            UserAccount userAccountOnDB = _unitOfWork.UserAccounts.Retrieve(userAccountID);
            if (userAccountOnDB == null)
                throw new Exception($"UserAccount not found.");

            return userAccountOnDB.Photo;
        }

        public bool DeleteUserAccountPhoto(int userAccountID)
        {
            UserAccount userAccountOnDB = _unitOfWork.UserAccounts.Retrieve(userAccountID);
            if (userAccountOnDB == null)
                throw new Exception($"UserAccount to update not found.");

            userAccountOnDB.Photo = null;

            _unitOfWork.UserAccounts.Update(userAccountOnDB);
            _unitOfWork.Commit();

            return true;
        }
    }
}
