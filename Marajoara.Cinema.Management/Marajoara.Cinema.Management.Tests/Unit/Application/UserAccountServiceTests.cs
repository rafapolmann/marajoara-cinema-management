using FluentAssertions;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule;
using Marajoara.Cinema.Management.Domain.Common;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Marajoara.Cinema.Management.Tests.Unit.Application
{
    [TestClass]
    public class UserAccountServiceTests
    {
        private Mock<IFileImageService> _fileImageServiceMock;
        private Mock<IMarajoaraUnitOfWork> _unitOfWorkMock;
        private IUserAccountService _userAccountService;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWorkMock = new Mock<IMarajoaraUnitOfWork>();
            _fileImageServiceMock = new Mock<IFileImageService>();
            _userAccountService = new UserAccountService(_unitOfWorkMock.Object, _fileImageServiceMock.Object);
        }

        #region AddUserAccount
        [TestMethod]
        public void UserAccountService_AddAttendantUserAccount_Should_Add_New_UserAccount_With_Dafault_Password_And_AccessLevel_Attendant()
        {
            UserAccount userAccountOnDB = GetUserAccountToTest();
            int dbCurrentID = userAccountOnDB.UserAccountID;

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.RetrieveByMail(userAccountOnDB.Mail)).Returns(userAccountOnDB);

            string accountName = "AttendantToAdd";
            string defaultPass = string.Concat(accountName.ToLower().Replace(" ", ""), "P@ssW0rd");

            UserAccount userAccountToAdd = GetUserAccountToTest(dbCurrentID + 1, accountName, "attendant@email.com", "passwordToIgnore", AccessLevel.Customer);

            _userAccountService.AddAttendantUserAccount(userAccountToAdd).Should().Be(dbCurrentID + 1);

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.RetrieveByMail(userAccountToAdd.Mail), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Add(userAccountToAdd), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Add(It.Is<UserAccount>(ua => ua.Name.Equals(userAccountToAdd.Name) &&
                                                                                        ua.Mail.Equals(userAccountToAdd.Mail) &&
                                                                                        ua.Password.Equals(defaultPass) &&
                                                                                        ua.Photo == null &&
                                                                                        ua.Level.Equals(AccessLevel.Attendant))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void UserAccountService_AddAttendantUserAccount_Should_Throw_Exception_When_UserAccount_To_Add_Mail_Already_Exists()
        {
            string userMail = "userMail@mail.com";
            UserAccount userAccountToAdd = GetUserAccountToTest(0, "newAttendant", userMail);
            UserAccount userAccountOnDB = GetUserAccountToTest(1, "userOndDB", userMail);

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.RetrieveByMail(userAccountOnDB.Mail)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.AddAttendantUserAccount(userAccountToAdd);
            action.Should().Throw<Exception>().WithMessage($"Already exists User Account with e-mail address: {userMail}.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.RetrieveByMail(userAccountToAdd.Mail), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Add(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }


        [TestMethod]
        public void UserAccountService_AddCustomerUserAccount_Should_Add_New_UserAccount_With_Dafault_Password_And_AccessLevel_Customer()
        {
            UserAccount userAccountOnDB = GetUserAccountToTest();
            int dbCurrentID = userAccountOnDB.UserAccountID;

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.RetrieveByMail(userAccountOnDB.Mail)).Returns(userAccountOnDB);

            string accountName = "CustomerToAdd";
            string defaultPass = string.Concat(accountName.ToLower().Replace(" ", ""), "P@ssW0rd");

            UserAccount userAccountToAdd = GetUserAccountToTest(dbCurrentID + 1, accountName, "customer@email.com", "passwordToIgnore", AccessLevel.Attendant);

            _userAccountService.AddCustomerUserAccount(userAccountToAdd).Should().Be(dbCurrentID + 1);

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.RetrieveByMail(userAccountToAdd.Mail), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Add(userAccountToAdd), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Add(It.Is<UserAccount>(ua => ua.Name.Equals(userAccountToAdd.Name) &&
                                                                                        ua.Mail.Equals(userAccountToAdd.Mail) &&
                                                                                        ua.Password.Equals(defaultPass) &&
                                                                                        ua.Photo == null &&
                                                                                        ua.Level.Equals(AccessLevel.Customer))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void UserAccountService_AddCustomerUserAccount_Should_Throw_Exception_When_UserAccount_To_Add_Mail_Already_Exists()
        {
            string userMail = "userMail@mail.com";
            UserAccount userAccountToAdd = GetUserAccountToTest(0, "newCustomer", userMail);
            UserAccount userAccountOnDB = GetUserAccountToTest(1, "userOndDB", userMail);

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.RetrieveByMail(userAccountOnDB.Mail)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.AddCustomerUserAccount(userAccountToAdd);
            action.Should().Throw<Exception>().WithMessage($"Already exists User Account with e-mail address: {userMail}.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.RetrieveByMail(userAccountToAdd.Mail), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Add(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_AddManagerUserAccount_Should_Add_New_UserAccount_With_Dafault_Password_And_AccessLevel_Manager()
        {
            UserAccount userAccountOnDB = GetUserAccountToTest();
            int dbCurrentID = userAccountOnDB.UserAccountID;

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.RetrieveByMail(userAccountOnDB.Mail)).Returns(userAccountOnDB);

            string accountName = "ManagerToAdd";
            string defaultPass = string.Concat(accountName.ToLower().Replace(" ", ""), "P@ssW0rd");

            UserAccount userAccountToAdd = GetUserAccountToTest(dbCurrentID + 1, accountName, "manager@email.com", "passwordToIgnore", AccessLevel.Attendant);

            _userAccountService.AddManagerUserAccount(userAccountToAdd).Should().Be(dbCurrentID + 1);

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.RetrieveByMail(userAccountToAdd.Mail), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Add(userAccountToAdd), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Add(It.Is<UserAccount>(ua => ua.Name.Equals(userAccountToAdd.Name) &&
                                                                                        ua.Mail.Equals(userAccountToAdd.Mail) &&
                                                                                        ua.Password.Equals(defaultPass) &&
                                                                                        ua.Photo == null &&
                                                                                        ua.Level.Equals(AccessLevel.Manager))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void UserAccountService_AddManagerUserAccount_Should_Throw_Exception_When_UserAccount_To_Add_Mail_Already_Exists()
        {
            string userMail = "userMail@mail.com";
            UserAccount userAccountToAdd = GetUserAccountToTest(0, "newManager", userMail);
            UserAccount userAccountOnDB = GetUserAccountToTest(1, "userOndDB", userMail);

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.RetrieveByMail(userAccountOnDB.Mail)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.AddManagerUserAccount(userAccountToAdd);
            action.Should().Throw<Exception>().WithMessage($"Already exists User Account with e-mail address: {userMail}.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.RetrieveByMail(userAccountToAdd.Mail), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Add(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion AddUserAccount

        #region Gets_UserAccount
        [TestMethod]
        public void UserAccountService_GetUserAccountByID_Should_Return_UserAccount_When_UserAccount_ID_Exists()
        {
            int userAccountToRetriveID = 1;

            UserAccount userAccountOnDB = GetUserAccountToTest();
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            _userAccountService.GetUserAccountByID(userAccountToRetriveID).Should().NotBeNull();
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountToRetriveID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_GetUserAccountByID_Should_Return_Null_When_UserAccount_ID_Not_Exists()
        {
            int userAccountToRetriveID = 2;

            UserAccount userAccountOnDB = GetUserAccountToTest();
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            _userAccountService.GetUserAccountByID(userAccountToRetriveID).Should().BeNull();
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountToRetriveID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_GetAll_Should_Return_All_UserAccounts()
        {
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.RetrieveAll()).Returns(new List<UserAccount> { GetUserAccountToTest() });

            _userAccountService.GetAll().Should().HaveCount(1);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.RetrieveAll(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_GetAll_Should_Return_Empty_Collection_When_There_Are_No_UserAccounts()
        {
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.RetrieveAll()).Returns(new List<UserAccount>());

            _userAccountService.GetAll().Should().BeEmpty();
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.RetrieveAll(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion Gets_UserAccount

        #region RemoveUserAccount
        [TestMethod]
        public void UserAccountService_RemoveUserAccount_Should_Remove_A_Given_UserAccount_When_UserAccountID_Exists()
        {
            UserAccount userAccountToDelete = new UserAccount { UserAccountID = 1 };
            UserAccount userAccountOnDB = GetUserAccountToTest(1, "userAccount", "user@mail.com", "pass", AccessLevel.Customer);

            _unitOfWorkMock.Setup(uow => uow.Tickets.RetrieveByUserAccount(userAccountOnDB)).Returns(new List<Ticket>());
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            _userAccountService.RemoveUserAccount(userAccountToDelete).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.Tickets.RetrieveByUserAccount(userAccountOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountToDelete.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Delete(userAccountOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void UserAccountService_RemoveUserAccount_Should_Remove_A_Given_Manager_UserAccount_Verifying_If_Is_The_Last_Manager_When_UserAccountID_Exists()
        {
            UserAccount userAccountToDelete = new UserAccount { UserAccountID = 1 };
            UserAccount userAccountManagerOnDB = GetUserAccountToTest(1, "userAccount", "user@mail.com", "pass", AccessLevel.Manager);
            UserAccount otherUserAccountManagerOnDB = GetUserAccountToTest(2, "otherAccount", "other@mail.com", "pass", AccessLevel.Manager);

            _unitOfWorkMock.Setup(uow => uow.Tickets.RetrieveByUserAccount(userAccountManagerOnDB)).Returns(new List<Ticket>());
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountManagerOnDB.UserAccountID)).Returns(userAccountManagerOnDB);
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.RetrieveByAccessLevel(AccessLevel.Manager)).Returns(new List<UserAccount> { userAccountManagerOnDB, otherUserAccountManagerOnDB });

            _userAccountService.RemoveUserAccount(userAccountToDelete).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.Tickets.RetrieveByUserAccount(userAccountManagerOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.RetrieveByAccessLevel(AccessLevel.Manager), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountToDelete.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Delete(userAccountManagerOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void UserAccountService_RemoveUserAccount_Should_Throw_Exception_When_UserAccount_To_Delete_Is_The_Last_Manager()
        {
            UserAccount userAccountToDelete = new UserAccount { UserAccountID = 1 };
            UserAccount userAccountManagerOnDB = GetUserAccountToTest(1, "userAccount", "user@mail.com", "pass", AccessLevel.Manager);

            _unitOfWorkMock.Setup(uow => uow.Tickets.RetrieveByUserAccount(userAccountManagerOnDB)).Returns(new List<Ticket>());
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountManagerOnDB.UserAccountID)).Returns(userAccountManagerOnDB);
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.RetrieveByAccessLevel(AccessLevel.Manager)).Returns(new List<UserAccount> { userAccountManagerOnDB });

            Action action = () => _userAccountService.RemoveUserAccount(userAccountToDelete);
            action.Should().Throw<Exception>().WithMessage(@"Will not be possible to change level or delete the account before create another manager account.");

            _unitOfWorkMock.Verify(uow => uow.Tickets.RetrieveByUserAccount(userAccountManagerOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.RetrieveByAccessLevel(AccessLevel.Manager), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountToDelete.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Delete(userAccountManagerOnDB), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_RemoveUserAccount_Should_Throw_Exception_When_UserAccountID_Not_Exists()
        {
            UserAccount userAccountToDelete = new UserAccount { UserAccountID = 2 };
            UserAccount userAccountOnDB = GetUserAccountToTest(1);

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.RemoveUserAccount(userAccountToDelete);
            action.Should().Throw<Exception>().WithMessage("User account not found!");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountToDelete.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Delete(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_RemoveUserAccount_Should_Throw_Exception_When_UserAccount_Is_Linked_With_Some_Ticket()
        {
            UserAccount userAccountToDelete = new UserAccount { UserAccountID = 1 };
            UserAccount userAccountOnDB = GetUserAccountToTest(1);

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);
            _unitOfWorkMock.Setup(uow => uow.Tickets.RetrieveByUserAccount(userAccountOnDB)).Returns(new List<Ticket> { new Ticket() });

            Action action = () => _userAccountService.RemoveUserAccount(userAccountToDelete);
            action.Should().Throw<Exception>().WithMessage($"Cannot possible remove user account {userAccountOnDB.Name}. There are tickets linked with this account.");

            _unitOfWorkMock.Verify(uow => uow.Tickets.RetrieveByUserAccount(userAccountOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountToDelete.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Delete(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion RemoveUserAccount

        #region UpdateUserAccountBasicProperties
        [TestMethod]
        public void UserAccountService_UpdateUserAccountBasicProperties_Should_Update_Name_And_AccessLevel()
        {
            UserAccount userToUpdate = GetUserAccountToTest(1, "UserNewName", "username@email.com", "password", AccessLevel.Customer);
            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserNameAttendant", "username@email.com", "password", AccessLevel.Attendant);

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            _userAccountService.UpdateUserAccountBasicProperties(userToUpdate).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userToUpdate.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(userAccountOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.Is<UserAccount>(ua => ua.Name.Equals(userToUpdate.Name) &&
                                                                                           ua.Mail.Equals(userAccountOnDB.Mail) &&
                                                                                           ua.Password.Equals(userAccountOnDB.Password) &&
                                                                                           ua.Photo == userAccountOnDB.Photo &&
                                                                                           ua.Level.Equals(userToUpdate.Level))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void UserAccountService_UpdateUserAccountBasicProperties_Should_Not_Update_Password()
        {
            string newPassword = "NEW_password";
            UserAccount userToUpdate = GetUserAccountToTest(1, "UserNewName", "username@email.com", newPassword, AccessLevel.Customer);
            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserNameAttendant", "username@email.com", "password", AccessLevel.Attendant);

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            _userAccountService.UpdateUserAccountBasicProperties(userToUpdate).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userToUpdate.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(userAccountOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.Is<UserAccount>(ua => ua.Password.Equals(userAccountOnDB.Password) &&
                                                                                           !ua.Password.Equals(newPassword))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void UserAccountService_UpdateUserAccountBasicProperties_Should_Not_Update_Mail()
        {
            string newMail = "NEW_mail@marajoara.com";
            UserAccount userToUpdate = GetUserAccountToTest(1, "UserNewName", newMail, "password", AccessLevel.Customer);
            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserNameAttendant", "username@email.com", "password", AccessLevel.Attendant);

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            _userAccountService.UpdateUserAccountBasicProperties(userToUpdate).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userToUpdate.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(userAccountOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.Is<UserAccount>(ua => ua.Mail.Equals(userAccountOnDB.Mail) &&
                                                                                           !ua.Mail.Equals(newMail))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void UserAccountService_UpdateUserAccountBasicProperties_Should_Not_Update_Photo()
        {
            UserAccount userToUpdate = GetUserAccountToTest(1, "UserNewName", "username@email.com", "password", AccessLevel.Customer);

            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserNameAttendant", "username@email.com", "password", AccessLevel.Attendant);
            userAccountOnDB.Photo = new byte[] { 0, 0, 0 };

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            _userAccountService.UpdateUserAccountBasicProperties(userToUpdate).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userToUpdate.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(userAccountOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.Is<UserAccount>(ua => ua.Photo.Equals(userAccountOnDB.Photo) &&
                                                                                           !ua.Photo.Equals(userToUpdate.Photo))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void UserAccountService_UpdateUserAccountBasicProperties_Should_Throw_Exception_When_UserAccount_ID_Not_Exists()
        {
            int userAccountID = 2;
            UserAccount userToUpdate = GetUserAccountToTest(userAccountID, "UserNewName");
            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserName");

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.UpdateUserAccountBasicProperties(userToUpdate);

            action.Should().Throw<Exception>().WithMessage("UserAccount to update not found.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_UpdateUserAccountBasicProperties_Should_Throw_Exception_When_UserAccount_Level_Changed_And_Is_The_Last_Manager()
        {
            UserAccount userToUpdate = GetUserAccountToTest(1, "UserNewName", "username@email.com", "password", AccessLevel.Customer);
            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserName");

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.RetrieveByAccessLevel(AccessLevel.Manager)).Returns(new List<UserAccount> { userAccountOnDB });

            Action action = () => _userAccountService.UpdateUserAccountBasicProperties(userToUpdate);

            action.Should().Throw<Exception>().WithMessage(@"Will not be possible to change level or delete the account before create another manager account.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userToUpdate.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.RetrieveByAccessLevel(AccessLevel.Manager), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion UpdateUserAccountBasicProperties

        #region UserPhoto
        [TestMethod]
        public void UserAccountService_UpdateUserAccountPhoto_Should_Throw_Exception_When_UserAccount_ID_Not_Exists()
        {
            int userAccountID = 2;
            Stream photoStream = null;//Paser Stream is tested in IFileImageService

            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserName");

            _fileImageServiceMock.Setup(ps => ps.GetImageBytes(It.IsAny<Stream>())).Returns(new byte[0]);
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.UpdateUserAccountPhoto(userAccountID, photoStream);

            action.Should().Throw<Exception>().WithMessage("UserAccount to update not found.");

            _fileImageServiceMock.Verify(ps => ps.GetImageBytes(It.IsAny<Stream>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_UpdateUserAccountPhoto_Should_Update_UserAccount_Photo_Property()
        {
            byte[] imageBytes = { 01, 02, 03 };
            int userAccountID = 1;
            Stream photoStream = null;//Paser Stream is tested in IFileImageService

            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserName");

            _fileImageServiceMock.Setup(ps => ps.GetImageBytes(It.IsAny<Stream>())).Returns(imageBytes);
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            _userAccountService.UpdateUserAccountPhoto(userAccountID, photoStream).Should().BeTrue();

            _fileImageServiceMock.Verify(ps => ps.GetImageBytes(photoStream), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(userAccountOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.Is<UserAccount>(ua => ua.Name.Equals(userAccountOnDB.Name) &&
                                                                                           ua.Mail.Equals(userAccountOnDB.Mail) &&
                                                                                           ua.Password.Equals(userAccountOnDB.Password) &&
                                                                                           ua.Photo == imageBytes &&
                                                                                           ua.Level.Equals(userAccountOnDB.Level))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void UserAccountService_GetUserAccountPhoto_Should_Throw_Exception_When_UserAccount_ID_Not_Exists()
        {
            int userAccountID = 2;

            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserName");
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.GetUserAccountPhoto(userAccountID);
            action.Should().Throw<Exception>().WithMessage("UserAccount not found.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_GetUserAccountPhoto_Should_Return_Image_Bytes_From_DB_When_UserAccount_Has_Photo()
        {
            byte[] imageBytes = { 01, 02, 03 };

            int userAccountID = 1;

            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserName");
            userAccountOnDB.Photo = imageBytes;

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            byte[] bytesToReturn = _userAccountService.GetUserAccountPhoto(userAccountID);

            bytesToReturn.Should().BeEquivalentTo(imageBytes);

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_GetUserAccountPhoto_Should_Return_Null_When_UserAccount_Does_Not_Have_Photo()
        {
            int userAccountID = 1;

            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserName");
            userAccountOnDB.Photo = null;

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            _userAccountService.GetUserAccountPhoto(userAccountID).Should().BeNull();

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_DeleteUserAccountPhoto_Should_Throw_Exception_When_UserAccount_ID_Not_Exists()
        {
            int userAccountID = 2;

            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserName");
            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.DeleteUserAccountPhoto(userAccountID);
            action.Should().Throw<Exception>().WithMessage("UserAccount to update not found.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_DeleteUserAccountPhoto_Should_Update_UserAccount_Photo_Property_To_Null()
        {
            int userAccountID = 1;

            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserName");
            userAccountOnDB.Photo = new byte[] { 01, 02, 03 };

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);
            _userAccountService.DeleteUserAccountPhoto(userAccountID).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(userAccountOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.Is<UserAccount>(ua => ua.Name.Equals(userAccountOnDB.Name) &&
                                                                                           ua.Mail.Equals(userAccountOnDB.Mail) &&
                                                                                           ua.Password.Equals(userAccountOnDB.Password) &&
                                                                                           ua.Photo == null &&
                                                                                           ua.Level.Equals(userAccountOnDB.Level))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }
        #endregion UserPhoto

        #region ResetUserAccountPassword
        [TestMethod]
        public void UserAccountService_ResetUserAccountPassword_Should_Update_UserAccount_Password_To_System_Deafult()
        {
            string resetedPassword = "usernameP@ssW0rd";
            int userAccountID = 1;
            UserAccount userToResetPass = GetUserAccountToTest(userAccountID, "UserName", "username@mail.com");
            UserAccount userAccountOnDB = GetUserAccountToTest(userAccountID, "UserName", "username@mail.com");

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            _userAccountService.ResetUserAccountPassword(userToResetPass).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userToResetPass.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(userAccountOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.Is<UserAccount>(ua => ua.Password.Equals(resetedPassword) &&
                                                                                           ua.UserAccountID.Equals(userAccountOnDB.UserAccountID) &&
                                                                                           ua.Level.Equals(userAccountOnDB.Level) &&
                                                                                           ua.Mail.Equals(userAccountOnDB.Mail) &&
                                                                                           ua.Photo == null &&
                                                                                           ua.Name.Equals(userAccountOnDB.Name))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void UserAccountService_ResetUserAccountPassword_Should_Should_Throw_Exception_When_UserAccount_ID_Not_Exists()
        {
            int userAccountID = 2;
            UserAccount userToResetPass = GetUserAccountToTest(userAccountID);
            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserName");

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.ResetUserAccountPassword(userToResetPass);

            action.Should().Throw<Exception>().WithMessage("UserAccount to update not found.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_ResetUserAccountPassword_Should_Throw_Exception_When_UserAccount_To_Reset_Password_Has_Mail_Different_On_DB()
        {
            int userAccountID = 1;
            UserAccount userToResetPass = GetUserAccountToTest(userAccountID, "UserName", "different@mail.com");
            UserAccount userAccountOnDB = GetUserAccountToTest(userAccountID, "UserName", "username@mail.com");

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.ResetUserAccountPassword(userToResetPass);

            action.Should().Throw<Exception>().WithMessage($"Invalid UserAccount login: {userToResetPass.Mail}.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion ResetUserAccountPassword

        #region ChangeUserAccountPassword
        [TestMethod]
        public void UserAccountService_ChangeUserAccountPassword_Should_Update_UserAccount_Password()
        {
            string passwordToUpdate = "newP@ssW0rd123";
            int userAccountID = 1;
            UserAccount userAccountToUpdate = GetUserAccountToTest(userAccountID, "UserName", "username@mail.com");
            UserAccount userAccountOnDB = GetUserAccountToTest(userAccountID, "UserName", "username@mail.com");

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            _userAccountService.ChangeUserAccountPassword(userAccountToUpdate, passwordToUpdate).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountToUpdate.UserAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(userAccountOnDB), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.Is<UserAccount>(ua => ua.Password.Equals(passwordToUpdate) &&
                                                                                           ua.UserAccountID.Equals(userAccountOnDB.UserAccountID) &&
                                                                                           ua.Level.Equals(userAccountOnDB.Level) &&
                                                                                           ua.Mail.Equals(userAccountOnDB.Mail) &&
                                                                                           ua.Photo == null &&
                                                                                           ua.Name.Equals(userAccountOnDB.Name))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void UserAccountService_ChangeUserAccountPassword_Should_Throw_Exception_When_New_Password_Is_Null()
        {
            string newPassword = null;
            int userAccountID = 1;
            UserAccount userToResetPass = GetUserAccountToTest(userAccountID, "UserName", "username@mail.com");
            UserAccount userAccountOnDB = GetUserAccountToTest(userAccountID, "UserName", "username@mail.com");

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.ChangeUserAccountPassword(userToResetPass, newPassword);

            action.Should().Throw<Exception>().WithMessage($"The new password does not attend the security criterias.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_ChangeUserAccountPassword_Should_Throw_Exception_When_New_Password_Is_Empty()
        {
            string newPassword = string.Empty;
            int userAccountID = 1;
            UserAccount userToResetPass = GetUserAccountToTest(userAccountID, "UserName", "username@mail.com");
            UserAccount userAccountOnDB = GetUserAccountToTest(userAccountID, "UserName", "username@mail.com");

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.ChangeUserAccountPassword(userToResetPass, newPassword);

            action.Should().Throw<Exception>().WithMessage($"The new password does not attend the security criterias.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_ChangeUserAccountPassword_Should_Throw_Exception_When_New_Password_Length_Is_Less_Than_Six_Characters()
        {
            string newPassword = "12345";
            int userAccountID = 1;
            UserAccount userToResetPass = GetUserAccountToTest(userAccountID, "UserName", "username@mail.com");
            UserAccount userAccountOnDB = GetUserAccountToTest(userAccountID, "UserName", "username@mail.com");

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.ChangeUserAccountPassword(userToResetPass, newPassword);

            action.Should().Throw<Exception>().WithMessage($"The new password does not attend the security criterias.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_ChangeUserAccountPassword_Should_Throw_Exception_When_UserAccount_ID_Not_Exists()
        {
            string newPassword = "newP@ssW0rd123";
            int userAccountID = 2;
            UserAccount userToResetPass = GetUserAccountToTest(userAccountID);
            UserAccount userAccountOnDB = GetUserAccountToTest(1, "UserName");

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.ChangeUserAccountPassword(userToResetPass, newPassword);

            action.Should().Throw<Exception>().WithMessage("UserAccount to update not found.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_ChangeUserAccountPassword_Should_Throw_Exception_When_UserAccount_To_Update_Password_Has_Mail_Different_On_DB()
        {
            string newPassword = "newP@ssW0rd123";
            int userAccountID = 1;
            UserAccount userToResetPass = GetUserAccountToTest(userAccountID, "UserName", "different@mail.com");
            UserAccount userAccountOnDB = GetUserAccountToTest(userAccountID, "UserName", "username@mail.com");

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.ChangeUserAccountPassword(userToResetPass, newPassword);

            action.Should().Throw<Exception>().WithMessage($"Invalid UserAccount login: {userToResetPass.Mail}.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void UserAccountService_ChangeUserAccountPassword_Should_Throw_Exception_When_UserAccount_To_Update_Password_Has_Password_To_Validate_Different_On_DB()
        {
            string newPassword = "newP@ssW0rd123";
            int userAccountID = 1;
            UserAccount userToResetPass = GetUserAccountToTest(userAccountID, "UserName", "username@mail.com", "passwordToValidate");
            UserAccount userAccountOnDB = GetUserAccountToTest(userAccountID, "UserName", "username@mail.com", "passwordOnDB");

            _unitOfWorkMock.Setup(uow => uow.UserAccounts.Retrieve(userAccountOnDB.UserAccountID)).Returns(userAccountOnDB);

            Action action = () => _userAccountService.ChangeUserAccountPassword(userToResetPass, newPassword);

            action.Should().Throw<Exception>().WithMessage($"Invalid UserAccount login: {userToResetPass.Mail}.");

            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Retrieve(userAccountID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.UserAccounts.Update(It.IsAny<UserAccount>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion ChangeUserAccountPassword

        private UserAccount GetUserAccountToTest(int userAccountID = 1,
                                                 string name = "UserName",
                                                 string mail = "username@mail.com",
                                                 string password = "password",
                                                 AccessLevel level = AccessLevel.Manager)
        {
            return new UserAccount
            {
                UserAccountID = userAccountID,
                Name = name,
                Mail = mail,
                Password = password,
                Level = level
            };
        }
    }
}
