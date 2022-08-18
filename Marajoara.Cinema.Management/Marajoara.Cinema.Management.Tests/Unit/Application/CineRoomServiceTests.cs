using FluentAssertions;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Tests.Unit.Application
{
    [TestClass]
    public class CineRoomServiceTests
    {
        private ICineRoomService _cineRoomService;
        private Mock<IMarajoaraUnitOfWork> _unitOfWorkMock;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWorkMock = new Mock<IMarajoaraUnitOfWork>();
            _cineRoomService = new CineRoomService(_unitOfWorkMock.Object);
        }

        #region Gets_CineRoom
        [TestMethod]
        public void CineRoomService_GetCineRoom_Should_Return_CineRoom_When_CineRoom_Name_Exists()
        {
            string cineRoomNameToRetrive = "CineRoomName";

            CineRoom cineRoomOnDB = GetCineRoomToTest();
            _unitOfWorkMock.Setup(uow => uow.CineRooms.RetrieveByName(cineRoomOnDB.Name)).Returns(cineRoomOnDB);

            _cineRoomService.GetCineRoom(cineRoomNameToRetrive).Should().NotBeNull();
            _unitOfWorkMock.Verify(uow => uow.CineRooms.RetrieveByName(cineRoomNameToRetrive), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_GetCineRoom_Should_Return_Null_When_CineRoom_Name_Not_Exists()
        {
            string cineRoomNameToRetrive = "NotExistsName";

            CineRoom cineRoomOnDB = GetCineRoomToTest();
            _unitOfWorkMock.Setup(uow => uow.CineRooms.RetrieveByName(cineRoomOnDB.Name)).Returns(cineRoomOnDB);

            _cineRoomService.GetCineRoom(cineRoomNameToRetrive).Should().BeNull();
            _unitOfWorkMock.Verify(uow => uow.CineRooms.RetrieveByName(cineRoomNameToRetrive), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_GetCineRoom_Should_Return_Null_When_CineRoom_Name_Parameter_Is_Null()
        {
            string cineRoomNameToRetrive = null;

            CineRoom cineRoomOnDB = GetCineRoomToTest();
            _unitOfWorkMock.Setup(uow => uow.CineRooms.RetrieveByName(cineRoomOnDB.Name)).Returns(cineRoomOnDB);

            _cineRoomService.GetCineRoom(cineRoomNameToRetrive).Should().BeNull();
            _unitOfWorkMock.Verify(uow => uow.CineRooms.RetrieveByName(cineRoomNameToRetrive), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_GetCineRoom_Should_Return_CineRoom_When_CineRoom_ID_Exists()
        {
            int cineRoomToRetriveID = 1;

            CineRoom cineRoomOnDB = GetCineRoomToTest();
            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomOnDB.CineRoomID)).Returns(cineRoomOnDB);

            _cineRoomService.GetCineRoom(cineRoomToRetriveID).Should().NotBeNull();
            _unitOfWorkMock.Verify(uow => uow.CineRooms.Retrieve(cineRoomToRetriveID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_GetCineRoom_Should_Return_Null_When_CineRoom_ID_Not_Exists()
        {
            int cineRoomToRetriveID = 2;

            CineRoom cineRoomOnDB = GetCineRoomToTest();
            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomOnDB.CineRoomID)).Returns(cineRoomOnDB);

            _cineRoomService.GetCineRoom(cineRoomToRetriveID).Should().BeNull();
            _unitOfWorkMock.Verify(uow => uow.CineRooms.Retrieve(cineRoomToRetriveID), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_GetAllCineRooms_Should_Return_All_CineRooms()
        {
            _unitOfWorkMock.Setup(uow => uow.CineRooms.RetrieveAll()).Returns(new List<CineRoom> { GetCineRoomToTest() });

            _cineRoomService.GetAllCineRooms().Should().HaveCount(1);
            _unitOfWorkMock.Verify(uow => uow.CineRooms.RetrieveAll(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_GetAllCineRooms_Should_Return_Empty_Collection_When_There_Are_No_CineRooms()
        {
            _unitOfWorkMock.Setup(uow => uow.CineRooms.RetrieveAll()).Returns(new List<CineRoom>());

            _cineRoomService.GetAllCineRooms().Should().BeEmpty();
            _unitOfWorkMock.Verify(uow => uow.CineRooms.RetrieveAll(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion Gets_CineRoom

        #region RemoveCineRoom
        [TestMethod]
        public void CineRoomService_RemoveCineRoom_Should_Remove_A_Given_CineRoom_When_CineRoomID_Exists()
        {
            CineRoom cineRoomOnDB = GetCineRoomToTest();
            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomOnDB.CineRoomID)).Returns(cineRoomOnDB);

            _cineRoomService.RemoveCineRoom(new CineRoom { CineRoomID = 1 }).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Delete(cineRoomOnDB));
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void CineRoomService_RemoveCineRoom_Should_Remove_A_Given_CineRoom_When_CineRoom_Name_Exists()
        {
            CineRoom cineRoomOnDB = GetCineRoomToTest();
            _unitOfWorkMock.Setup(uow => uow.CineRooms.RetrieveByName(cineRoomOnDB.Name)).Returns(cineRoomOnDB);

            _cineRoomService.RemoveCineRoom(new CineRoom { CineRoomID = 0, Name = "CineRoomName" }).Should().BeTrue();

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Delete(cineRoomOnDB));
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void CineRoomService_RemoveCineRoom_Should_Throw_Exception_When_CineRoom_Name_And_CineRoomID_Not_Exists()
        {
            CineRoom cineRoomOnDB = GetCineRoomToTest();
            _unitOfWorkMock.Setup(uow => uow.CineRooms.RetrieveByName(cineRoomOnDB.Name)).Returns(cineRoomOnDB);
            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomOnDB.CineRoomID)).Returns(cineRoomOnDB);

            Action action = () => _cineRoomService.RemoveCineRoom(new CineRoom { CineRoomID = 15, Name = "NotExists" });
            action.Should().Throw<Exception>().WithMessage("Cine room not found.");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Delete(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_RemoveCineRoom_Should_Throw_ArgumentException_When_CineRoom_Parameter_Is_Null()
        {
            Action action = () => _cineRoomService.RemoveCineRoom(null);
            action.Should().Throw<ArgumentException>().WithMessage("CineRoom parameter cannot be null. (Parameter 'cineRoom')");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Delete(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion RemoveCineRoom

        #region AddCineRoom
        [TestMethod]
        public void CineRoomService_AddCineRoom_Should_Add_New_CineRoom()
        {
            CineRoom cineRoomOnDB = GetCineRoomToTest(1, "CineRoomName");
            string cineRoomName = cineRoomOnDB.Name;
            int dbCurrentID = cineRoomOnDB.CineRoomID;

            _unitOfWorkMock.Setup(uow => uow.CineRooms.RetrieveByName(cineRoomOnDB.Name)).Returns(cineRoomOnDB);

            CineRoom cineRoomToAdd = GetCineRoomToTest(dbCurrentID + 1, "NewCineRoom");
            _cineRoomService.AddCineRoom(cineRoomToAdd).Should().Be(dbCurrentID + 1);

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Add(cineRoomToAdd), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void CineRoomService_AddCineRoom_Should_Throw_ArgumentException_When_CineRoom_Parameter_Is_Null()
        {
            Action action = () => _cineRoomService.AddCineRoom(null);
            action.Should().Throw<ArgumentException>().WithMessage("CineRoom parameter cannot be null. (Parameter 'cineRoom')");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Add(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_AddCineRoom_Should_Throw_Exception_When_CineRoom_Name_Is_Empty()
        {
            CineRoom cineRoomToAdd = GetCineRoomToTest(0, string.Empty);
            Action action = () => _cineRoomService.AddCineRoom(cineRoomToAdd);
            action.Should().Throw<Exception>().WithMessage("Cine room name cannot be null or empty.");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Add(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_AddCineRoom_Should_Throw_Exception_When_CineRoom_Name_Is_Null()
        {
            CineRoom cineRoomToAdd = GetCineRoomToTest(0, null);
            Action action = () => _cineRoomService.AddCineRoom(cineRoomToAdd);
            action.Should().Throw<Exception>().WithMessage("Cine room name cannot be null or empty.");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Add(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_AddCineRoom_Should_Throw_Exception_When_CineRoom_To_Add_Has_Total_Seats_Equal_Zero()
        {
            Action action = () => _cineRoomService.AddCineRoom(GetCineRoomToTest(0, "CineRoomName", 0, 0));
            action.Should().Throw<Exception>().WithMessage("Seat number cannot be equals zero or negative.");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Add(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_AddCineRoom_Should_Throw_Exception_When_CineRoom_To_Add_Has_Total_Seats_Is_Negative()
        {
            Action action = () => _cineRoomService.AddCineRoom(GetCineRoomToTest(0, "CineRoomName", -1));
            action.Should().Throw<Exception>().WithMessage("Seat number cannot be equals zero or negative.");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Add(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_AddCineRoom_Should_Throw_Exception_When_CineRoom_To_Add_Has_Total_Seats_Higher_Than_100()
        {
            CineRoom cineRoomToAdd = GetCineRoomToTest(0, "CineRoomName", 11, 11);
            int totalSeats = cineRoomToAdd.TotalSeats;
            Action action = () => _cineRoomService.AddCineRoom(cineRoomToAdd);
            action.Should().Throw<Exception>().WithMessage($"Invalid seat number \"{totalSeats}\". Max of seat per cine room is 100.");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Add(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_AddCineRoom_Should_Throw_Exception_When_CineRoom_To_Add_Name_Already_Exists()
        {
            CineRoom cineRoomOnDB = GetCineRoomToTest(0, "CineRoomName");
            string cineRoomName = cineRoomOnDB.Name;

            _unitOfWorkMock.Setup(uow => uow.CineRooms.RetrieveByName(cineRoomOnDB.Name)).Returns(cineRoomOnDB);

            Action action = () => _cineRoomService.AddCineRoom(GetCineRoomToTest(0, cineRoomName));
            action.Should().Throw<Exception>().WithMessage($"Already exists cine room with name {cineRoomName}.");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Add(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion AddCineRoom

        #region UpdateCineRoom
        [TestMethod]
        public void CineRoomService_UpdateCineRoom_Should_Update_CineRoom_Register()
        {
            CineRoom otherCineRoomOnDB = GetCineRoomToTest(1, "CineRoomName", 5, 5);
            CineRoom cineRoomOnBDToUpdate = GetCineRoomToTest(2, "OldCineRoomName", 10, 10);

            CineRoom cineRoomToUpdate = GetCineRoomToTest(2, "NewCineRoomName", 6, 6);

            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomOnBDToUpdate.CineRoomID)).Returns(cineRoomOnBDToUpdate);
            _unitOfWorkMock.Setup(uow => uow.CineRooms.RetrieveByName(otherCineRoomOnDB.Name)).Returns(otherCineRoomOnDB);

            _cineRoomService.UpdateCineRoom(cineRoomToUpdate);

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Update(cineRoomOnBDToUpdate), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CineRooms.Update(It.Is<CineRoom>(c => c.Name.Equals(cineRoomToUpdate.Name) &&
                                                                                    c.SeatsRow.Equals(cineRoomToUpdate.SeatsRow) &&
                                                                                    c.SeatsColumn.Equals(cineRoomToUpdate.SeatsColumn))), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [TestMethod]
        public void CineRoomService_UpdateCineRoom_Should_Throw_Exception_When_CineRoom_ID_Not_Exists()
        {
            CineRoom cineRoomOnDB = GetCineRoomToTest(1, "CineRoomName", 5, 5);
            CineRoom cineRoomUpdated = GetCineRoomToTest(2, "NewCineRoomName", 10, 10);

            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomOnDB.CineRoomID)).Returns(cineRoomOnDB);

            Action action = () => _cineRoomService.UpdateCineRoom(cineRoomUpdated);
            action.Should().Throw<Exception>().WithMessage("Cine room to update not found.");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Update(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_UpdateCineRoom_Should_Throw_Exception_When_CineRoom_Name_Exists_In_Other_Register()
        {
            CineRoom otherCineRoomOnDB = GetCineRoomToTest(1, "CineRoomName", 5, 5);
            CineRoom cineRoomOnBDToUpdate = GetCineRoomToTest(2, "OldCineRoomName", 10, 10);
            CineRoom cineRoomToUpdate = GetCineRoomToTest(2, "CineRoomName", 10, 10);

            _unitOfWorkMock.Setup(uow => uow.CineRooms.Retrieve(cineRoomOnBDToUpdate.CineRoomID)).Returns(cineRoomOnBDToUpdate);
            _unitOfWorkMock.Setup(uow => uow.CineRooms.RetrieveByName(otherCineRoomOnDB.Name)).Returns(otherCineRoomOnDB);

            Action action = () => _cineRoomService.UpdateCineRoom(cineRoomToUpdate);
            action.Should().Throw<Exception>().WithMessage($"Already exists cine room with name {cineRoomToUpdate.Name}.");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Update(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_UpdateCineRoom_Should_Throw_Exception_When_CineRoom_Name_Is_Empty()
        {
            CineRoom cineRoomUpdated = GetCineRoomToTest(2, string.Empty);
            Action action = () => _cineRoomService.UpdateCineRoom(cineRoomUpdated);
            action.Should().Throw<Exception>().WithMessage("Cine room name cannot be null or empty.");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Update(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_UpdateCineRoom_Should_Throw_Exception_When_CineRoom_Name_Is_Null()
        {
            CineRoom cineRoomUpdated = GetCineRoomToTest(2, null);
            Action action = () => _cineRoomService.UpdateCineRoom(cineRoomUpdated);
            action.Should().Throw<Exception>().WithMessage("Cine room name cannot be null or empty.");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Update(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_UpdateCineRoom_Should_Throw_Exception_When_CineRoom_To_Add_Has_Total_Seats_Equal_Zero()
        {
            Action action = () => _cineRoomService.UpdateCineRoom(GetCineRoomToTest(1, "CineRoomName", 0, 0));
            action.Should().Throw<Exception>().WithMessage("Seat number cannot be equals zero or negative.");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Update(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_UpdateCineRoom_Should_Throw_Exception_When_CineRoom_To_Add_Has_Total_Seats_Is_Negative()
        {
            Action action = () => _cineRoomService.UpdateCineRoom(GetCineRoomToTest(1, "CineRoomName", -1));
            action.Should().Throw<Exception>().WithMessage("Seat number cannot be equals zero or negative.");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Update(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_UpdateCineRoom_Should_Throw_Exception_When_CineRoom_To_Add_Has_Total_Seats_Higher_Than_100()
        {
            CineRoom cineRoomToAdd = GetCineRoomToTest(0, "CineRoomName", 11, 11);
            int totalSeats = cineRoomToAdd.TotalSeats;
            Action action = () => _cineRoomService.UpdateCineRoom(cineRoomToAdd);
            action.Should().Throw<Exception>().WithMessage($"Invalid seat number \"{totalSeats}\". Max of seat per cine room is 100.");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Update(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

        [TestMethod]
        public void CineRoomService_UpdateCineRoom_Should_Throw_ArgumentException_When_CineRoom_Parameter_Is_Null()
        {
            Action action = () => _cineRoomService.UpdateCineRoom(null);
            action.Should().Throw<ArgumentException>().WithMessage("CineRoom parameter cannot be null. (Parameter 'cineRoom')");

            _unitOfWorkMock.Verify(uow => uow.CineRooms.Update(It.IsAny<CineRoom>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
        #endregion UpdateCineRoom

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
