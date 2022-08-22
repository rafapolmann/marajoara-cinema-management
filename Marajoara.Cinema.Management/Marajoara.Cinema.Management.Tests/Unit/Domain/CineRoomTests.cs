using FluentAssertions;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Marajoara.Cinema.Management.Tests.Unit.Domain
{
    [TestClass]
    public class CineRoomTests
    {
        #region CopyTo
        [TestMethod]
        public void CineRoom_CopyTo_Should_Copy_All_Properties_Except_CineRoomID()
        {
            int originalID = 3;

            CineRoom cineRoom = GetCineRoomToTest();
            CineRoom cineRoomToCopy = new CineRoom { CineRoomID = originalID };

            cineRoom.CopyTo(cineRoomToCopy);

            cineRoomToCopy.Name.Should().Be(cineRoom.Name);
            cineRoomToCopy.SeatsColumn.Should().Be(cineRoom.SeatsColumn);
            cineRoomToCopy.SeatsRow.Should().Be(cineRoom.SeatsRow);
            cineRoomToCopy.TotalSeats.Should().Be(cineRoom.TotalSeats);
            cineRoomToCopy.CineRoomID.Should().NotBe(cineRoom.CineRoomID);
            cineRoomToCopy.CineRoomID.Should().Be(originalID);
        }

        [TestMethod]
        public void CineRoom_CopyTo_Should_Not_Copy_CineRoomID()
        {
            int originalID = 3;

            CineRoom cineRoom = GetCineRoomToTest();
            CineRoom cineRoomToCopy = new CineRoom { CineRoomID = originalID };

            cineRoom.CopyTo(cineRoomToCopy);

            cineRoomToCopy.CineRoomID.Should().NotBe(cineRoom.CineRoomID);
            cineRoomToCopy.CineRoomID.Should().Be(originalID);
        }

        [TestMethod]
        public void CineRoom_CopyTo_Should_Should_Throw_ArgumentException_When_CineRoomToCopy_Is_The_Same_Instance()
        {
            CineRoom cineRoom = GetCineRoomToTest();

            Action action = () => cineRoom.CopyTo(cineRoom);
            action.Should().Throw<ArgumentException>().WithMessage("Cine room to copy cannot be the same instance of the origin. (Parameter 'cineRoomToCopy')");
        }

        [TestMethod]
        public void CineRoom_CopyTo_Should_Should_Throw_ArgumentException_When_CineRoomToCopy_Is_Null()
        {
            CineRoom cineRoom = GetCineRoomToTest();

            Action action = () => cineRoom.CopyTo(null);
            action.Should().Throw<ArgumentException>().WithMessage("CineRoom parameter cannot be null. (Parameter 'cineRoomToCopy')");
        }
        #endregion CopyTo

        #region Validate
        [TestMethod]
        public void CineRoom_Validate_Should_Return_True_When_All_Rules_Are_OK()
        {
            CineRoom cineRoom = GetCineRoomToTest();
            cineRoom.Validate().Should().BeTrue();
        }

        [TestMethod]
        public void CineRoom_Validate_Should_Throw_Exception_When_CineRoom_Name_Is_Empty()
        {
            CineRoom cineRoom = GetCineRoomToTest(1, string.Empty);

            Action action = () => cineRoom.Validate();
            action.Should().Throw<Exception>().WithMessage("Cine room name cannot be null or empty.");
        }

        [TestMethod]
        public void CineRoom_Validate_Should_Throw_Exception_When_CineRoom_Name_Is_Null()
        {
            CineRoom cineRoom = GetCineRoomToTest(1, null);

            Action action = () => cineRoom.Validate();
            action.Should().Throw<Exception>().WithMessage("Cine room name cannot be null or empty.");
        }

        [TestMethod]
        public void CineRoom_Validate_Should_Throw_Exception_When_CineRoom_Total_Seats_Is_Zero()
        {
            CineRoom cineRoom = GetCineRoomToTest(1, "CineRoom", 0, 0);

            Action action = () => cineRoom.Validate();
            action.Should().Throw<Exception>().WithMessage("Seat number cannot be equals zero or negative.");
        }

        [TestMethod]
        public void CineRoom_Validate_Should_Throw_Exception_When_CineRoom_Total_Seats_Is_Negative()
        {
            CineRoom cineRoom = GetCineRoomToTest(1, "CineRoom", 10, -1);

            Action action = () => cineRoom.Validate();
            action.Should().Throw<Exception>().WithMessage("Seat number cannot be equals zero or negative.");
        }

        [TestMethod]
        public void CineRoom_Validate_Should_Throw_Exception_When_CineRoom_Total_Seats_Is_Greater_Then_100()
        {
            CineRoom cineRoom = GetCineRoomToTest(1, "CineRoom", 10, 11);

            Action action = () => cineRoom.Validate();
            action.Should().Throw<Exception>().WithMessage($"Invalid seat number \"{cineRoom.TotalSeats}\". Max of seat per cine room is 100.");
        }

        [TestMethod]
        public void CineRoom_Validate_Should_Throw_Exception_When_CineRoom_Total_Seats_Is_Greater_Less_20()
        {
            CineRoom cineRoom = GetCineRoomToTest(1, "CineRoom", 9, 2);

            Action action = () => cineRoom.Validate();
            action.Should().Throw<Exception>().WithMessage($"Invalid seat number \"{cineRoom.TotalSeats}\". Min of seat per cine room is 20.");
        }
        #endregion Validate

        private CineRoom GetCineRoomToTest(int cineRoomID = 1,
                                   string name = "CineRoomName",
                                   int seatsColumn = 5,
                                   int seatsRow = 10)
        {
            return new CineRoom
            {
                CineRoomID = cineRoomID,
                Name = name,
                SeatsColumn = seatsColumn,
                SeatsRow = seatsRow
            };
        }
    }
}
