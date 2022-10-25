using FluentAssertions;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Tests.Unit.Domain
{
    [TestClass]

    public class UserAccountTests
    {
        [TestMethod]
        public void UserAccount_Validate_Should_Return_True_When_All_Rules_Are_OK()
        {
            UserAccount movie = GetUserToTest();
            movie.Validate().Should().BeTrue();
        }

        [TestMethod]
        public void UserAccount_Validate_Should_Throw_Exception_When_Name_Is_Empty()
        {
            UserAccount userAccount = GetUserToTest(1, string.Empty);

            Action action = () => userAccount.Validate();
            action.Should().Throw<Exception>().WithMessage("User Account name cannot be null or empty.");
        }


        [TestMethod]
        public void UserAccount_Validate_Should_Throw_Exception_When_Password_Is_Empty()
        {
            UserAccount userAccount = GetUserToTest();
            userAccount.Password = string.Empty;

            Action action = () => userAccount.Validate();
            action.Should().Throw<Exception>().WithMessage("User Account password cannot be null or empty.");
        }

        [TestMethod]
        public void UserAccount_Validate_Should_Throw_Exception_When_Email_Is_Invalid_1()
        {
            UserAccount userAccount = GetUserToTest();
            userAccount.Mail="a@a@a";

            Action action = () => userAccount.Validate();
            action.Should().Throw<Exception>().WithMessage("Invalid mail!");
        }

        [TestMethod]
        public void UserAccount_Validate_Should_Throw_Exception_When_Email_Is_Invalid_2()
        {
            UserAccount userAccount = GetUserToTest();
            userAccount.Mail = "aaa.com";

            Action action = () => userAccount.Validate();
            action.Should().Throw<Exception>().WithMessage("Invalid mail!");
        }

        [TestMethod]
        public void UserAccount_Validate_Should_Throw_Exception_When_Email_Is_Invalid_3()
        {
            UserAccount userAccount = GetUserToTest();
            userAccount.Mail = "a@a";

            Action action = () => userAccount.Validate();
            action.Should().Throw<Exception>().WithMessage("Invalid mail!");
        }


        protected UserAccount GetUserToTest(int userAccountID = 1,
                                      string name = "Nome do usuário",
                                      string mail  = "email@domain.com",
                                      string password = "123456",
                                      AccessLevel level =AccessLevel.Manager)
        {

            return new UserAccount
            {
                UserAccountID = userAccountID,
                Name = name,
                Mail = mail,
                Password = password,    
                Level = level,                
            };
        }
    }
}
