using FluentAssertions;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule;
using Marajoara.Cinema.Management.Domain.Common;
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
