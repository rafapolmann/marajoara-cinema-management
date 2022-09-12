using FluentAssertions;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule;
using Marajoara.Cinema.Management.Domain.Common;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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
