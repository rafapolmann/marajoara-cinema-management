using FluentAssertions;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Marajoara.Cinema.Management.Tests.Unit.Domain.CineRoomModule
{
    [TestClass]
    public class CineRoomTests
    {
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
