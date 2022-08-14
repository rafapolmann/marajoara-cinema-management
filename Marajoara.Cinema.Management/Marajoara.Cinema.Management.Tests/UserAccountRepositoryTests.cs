using FluentAssertions;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Tests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Marajoara.Cinema.Management.Tests
{
    [TestClass]
    public class UserAccountRepositoryTests : UnitOfWorkIntegrationBase
    {
        [TestInitialize]
        public void Initialize()
        {
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance();
        }

        [TestMethod]
        public void UnitOfWork_Should_Insert_New_UserAccount_On_Database()
        {
            UserAccount userAccountToAdd = GetUserAccountToTest();

            _marajoaraUnitOfWork.UserAccounts.Add(userAccountToAdd);
            _marajoaraUnitOfWork.Commit();
            int userAccountID = userAccountToAdd.UserAccountID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            UserAccount userAccountToAssert = _marajoaraUnitOfWork.UserAccounts.Retrieve(userAccountID);

            userAccountToAssert.Should().NotBeNull();
            userAccountToAssert.UserAccountID.Should().Be(userAccountID);
            userAccountToAssert.Name.Should().Be("FullName");
            userAccountToAssert.Mail.Should().Be("email");
            userAccountToAssert.Password.Should().Be("P@ssW0rd");
            userAccountToAssert.Level.Should().Be(AccessLevel.Manager);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Delete_Existing_UserAccount_On_Database()
        {
            UserAccount userAccountToAdd = GetUserAccountToTest();

            _marajoaraUnitOfWork.UserAccounts.Add(userAccountToAdd);
            _marajoaraUnitOfWork.Commit();
            int userAccountID = userAccountToAdd.UserAccountID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            UserAccount userAccountToDelete = _marajoaraUnitOfWork.UserAccounts.Retrieve(userAccountID);
            _marajoaraUnitOfWork.UserAccounts.Delete(userAccountToDelete);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            _marajoaraUnitOfWork.UserAccounts.Retrieve(userAccountID).Should().BeNull();
            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Update_All_Properties_Of_Existing_UserAccount_On_Database()
        {
            UserAccount userAccountToAdd = GetUserAccountToTest();
            _marajoaraUnitOfWork.UserAccounts.Add(userAccountToAdd);
            _marajoaraUnitOfWork.Commit();

            int userAccountID = userAccountToAdd.UserAccountID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            UserAccount userAccountToUpdate = _marajoaraUnitOfWork.UserAccounts.Retrieve(userAccountID);
            userAccountToUpdate.Name = "FullNameUpdated";
            userAccountToUpdate.Mail = "emailUpdated";
            userAccountToUpdate.Password = "P@ssW0rdUpdated";
            userAccountToUpdate.Level = AccessLevel.Customer;
            _marajoaraUnitOfWork.UserAccounts.Update(userAccountToUpdate);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            UserAccount userAccountToAssert = _marajoaraUnitOfWork.UserAccounts.Retrieve(userAccountID);
            userAccountToAssert.Should().NotBeNull();
            userAccountToAssert.UserAccountID.Should().Be(userAccountID);
            userAccountToAssert.Name.Should().Be("FullNameUpdated");
            userAccountToAssert.Mail.Should().Be("emailUpdated");
            userAccountToAssert.Password.Should().Be("P@ssW0rdUpdated");
            userAccountToAssert.Level.Should().Be(AccessLevel.Customer);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_Persisted_UserAccount_On_Database_By_AccountID()
        {
            UserAccount userAccountToAdd = GetUserAccountToTest();

            _marajoaraUnitOfWork.UserAccounts.Add(userAccountToAdd);
            _marajoaraUnitOfWork.Commit();
            int userAccountID = userAccountToAdd.UserAccountID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            UserAccount userAccountToAssert = _marajoaraUnitOfWork.UserAccounts.Retrieve(userAccountID);

            userAccountToAssert.Should().NotBeNull();
            userAccountToAssert.UserAccountID.Should().Be(userAccountID);
            userAccountToAssert.Name.Should().Be("FullName");
            userAccountToAssert.Mail.Should().Be("email");
            userAccountToAssert.Password.Should().Be("P@ssW0rd");
            userAccountToAssert.Level.Should().Be(AccessLevel.Manager);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_Persisted_UserAccount_On_Database_By_Name()
        {
            UserAccount userAccountToAdd = GetUserAccountToTest();
            string userAccountName = userAccountToAdd.Name;

            _marajoaraUnitOfWork.UserAccounts.Add(userAccountToAdd);
            _marajoaraUnitOfWork.Commit();
            int userAccountID = userAccountToAdd.UserAccountID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            UserAccount userAccountToAssert = _marajoaraUnitOfWork.UserAccounts.RetrieveByName(userAccountName);

            userAccountToAssert.Should().NotBeNull();
            userAccountToAssert.UserAccountID.Should().Be(userAccountID);
            userAccountToAssert.Name.Should().Be("FullName");
            userAccountToAssert.Mail.Should().Be("email");
            userAccountToAssert.Password.Should().Be("P@ssW0rd");
            userAccountToAssert.Level.Should().Be(AccessLevel.Manager);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_Persisted_UserAccount_On_Database_By_Mail()
        {
            UserAccount userAccountToAdd = GetUserAccountToTest();
            string userAccountMail = userAccountToAdd.Mail;

            _marajoaraUnitOfWork.UserAccounts.Add(userAccountToAdd);
            _marajoaraUnitOfWork.Commit();
            int userAccountID = userAccountToAdd.UserAccountID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            UserAccount userAccountToAssert = _marajoaraUnitOfWork.UserAccounts.RetrieveByMail(userAccountMail);

            userAccountToAssert.Should().NotBeNull();
            userAccountToAssert.UserAccountID.Should().Be(userAccountID);
            userAccountToAssert.Name.Should().Be("FullName");
            userAccountToAssert.Mail.Should().Be("email");
            userAccountToAssert.Password.Should().Be("P@ssW0rd");
            userAccountToAssert.Level.Should().Be(AccessLevel.Manager);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_All_Persisted_UserAccounts_From_Database()
        {
            UserAccount userAccountToAdd01 = GetUserAccountToTest("user01", "email01", "pass01", AccessLevel.Manager);
            UserAccount userAccountToAdd02 = GetUserAccountToTest("user02", "email02", "pass02", AccessLevel.Customer);
            UserAccount userAccountToAdd03 = GetUserAccountToTest("user03", "email03", "pass03", AccessLevel.Attendant);

            _marajoaraUnitOfWork.UserAccounts.Add(userAccountToAdd01);
            _marajoaraUnitOfWork.UserAccounts.Add(userAccountToAdd02);
            _marajoaraUnitOfWork.UserAccounts.Add(userAccountToAdd03);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            List<UserAccount> userAccountListToAssert = _marajoaraUnitOfWork.UserAccounts.RetrieveAll().ToList();

            userAccountListToAssert.Should().NotBeNull();
            userAccountListToAssert.Should().HaveCount(3);
            userAccountListToAssert.Find(us => us.Mail.Equals("email01")).Should().NotBeNull();
            userAccountListToAssert.Find(us => us.Mail.Equals("email02")).Should().NotBeNull();
            userAccountListToAssert.Find(us => us.Mail.Equals("email03")).Should().NotBeNull();

            _marajoaraUnitOfWork.Dispose();
        }
    }
}
