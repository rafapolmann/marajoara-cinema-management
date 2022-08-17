using FluentAssertions;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Tests.Integration.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Marajoara.Cinema.Management.Tests.Integration.Repositories
{
    [TestClass]
    public class CineRoomRepositoryTests : UnitOfWorkIntegrationBase
    {

        [TestInitialize]
        public void Initialize()
        {
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance();
        }

        [TestMethod]
        public void UnitOfWork_Should_Insert_CineRoom_On_Database()
        {
            CineRoom cineRoomToAdd = GetCineRoomToTest();

            _marajoaraUnitOfWork.CineRooms.Add(cineRoomToAdd);
            _marajoaraUnitOfWork.Commit();
            int cineRoomID = cineRoomToAdd.CineRoomID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            CineRoom cineRoomAdded = _marajoaraUnitOfWork.CineRooms.Retrieve(cineRoomID);
            cineRoomAdded.Should().NotBeNull();
            cineRoomAdded.Name.Should().Be("CineRoomName");
            cineRoomAdded.SeatsRow.Should().Be(10);
            cineRoomAdded.SeatsColumn.Should().Be(20);
            cineRoomAdded.TotalSeats.Should().Be(200);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Delete_Existing_CineRoom_On_Database()
        {
            CineRoom cineRoomToAdd = GetCineRoomToTest();

            _marajoaraUnitOfWork.CineRooms.Add(cineRoomToAdd);
            _marajoaraUnitOfWork.Commit();
            int cineRoomID = cineRoomToAdd.CineRoomID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            CineRoom cineRoomToDelete = _marajoaraUnitOfWork.CineRooms.Retrieve(cineRoomID);
            _marajoaraUnitOfWork.CineRooms.Delete(cineRoomToDelete);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            _marajoaraUnitOfWork.CineRooms.Retrieve(cineRoomID).Should().BeNull();
            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Update_All_Properties_Of_Existing_CineRoom_On_Database()
        {
            CineRoom cineRoomToAdd = GetCineRoomToTest();
            _marajoaraUnitOfWork.CineRooms.Add(cineRoomToAdd);
            _marajoaraUnitOfWork.Commit();

            int cineRoomID = cineRoomToAdd.CineRoomID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            CineRoom cineRoomToUpdate = _marajoaraUnitOfWork.CineRooms.Retrieve(cineRoomID);
            cineRoomToUpdate.Name = "CineNameUpdated";
            cineRoomToUpdate.SeatsRow = 35;
            cineRoomToUpdate.SeatsColumn = 25;
            _marajoaraUnitOfWork.CineRooms.Update(cineRoomToUpdate);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            CineRoom cineRoomToAssert = _marajoaraUnitOfWork.CineRooms.Retrieve(cineRoomID);
            cineRoomToAssert.Should().NotBeNull();
            cineRoomToAssert.CineRoomID.Should().Be(cineRoomID);
            cineRoomToAssert.Name.Should().Be("CineNameUpdated");
            cineRoomToAssert.SeatsRow.Should().Be(35);
            cineRoomToAssert.SeatsColumn.Should().Be(25);
            cineRoomToAssert.TotalSeats.Should().Be(875);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_Persisted_CineRoom_On_Database_By_CineRoomID()
        {
            CineRoom cineRoomToAdd = GetCineRoomToTest();

            _marajoaraUnitOfWork.CineRooms.Add(cineRoomToAdd);
            _marajoaraUnitOfWork.Commit();
            int cineRoomID = cineRoomToAdd.CineRoomID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            CineRoom cineRoomToAssert = _marajoaraUnitOfWork.CineRooms.Retrieve(cineRoomID);

            cineRoomToAssert.Should().NotBeNull();
            cineRoomToAssert.CineRoomID.Should().Be(cineRoomID);
            cineRoomToAssert.Name.Should().Be("CineRoomName");
            cineRoomToAssert.SeatsColumn.Should().Be(20);
            cineRoomToAssert.SeatsRow.Should().Be(10);
            cineRoomToAssert.TotalSeats.Should().Be(200);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_Persisted_CineRoom_On_Database_By_Name()
        {
            CineRoom cineRoomToAdd = GetCineRoomToTest();
            string cineRoomName = cineRoomToAdd.Name;

            _marajoaraUnitOfWork.CineRooms.Add(cineRoomToAdd);
            _marajoaraUnitOfWork.Commit();
            int cineRoomID = cineRoomToAdd.CineRoomID;

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            CineRoom cineRoomToAssert = _marajoaraUnitOfWork.CineRooms.RetrieveByName(cineRoomName);

            cineRoomToAssert.Should().NotBeNull();
            cineRoomToAssert.CineRoomID.Should().Be(cineRoomID);
            cineRoomToAssert.Name.Should().Be("CineRoomName");
            cineRoomToAssert.SeatsColumn.Should().Be(20);
            cineRoomToAssert.SeatsRow.Should().Be(10);
            cineRoomToAssert.TotalSeats.Should().Be(200);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_All_CineRooms_From_Database()
        {
            CineRoom cineRoomToAdd01 = GetCineRoomToTest("cineRoom01", 50, 10);
            CineRoom cineRoomToAdd02 = GetCineRoomToTest("cineRoom02", 40, 30);
            CineRoom cineRoomToAdd03 = GetCineRoomToTest("cineRoom03", 30, 20);

            _marajoaraUnitOfWork.CineRooms.Add(cineRoomToAdd01);
            _marajoaraUnitOfWork.CineRooms.Add(cineRoomToAdd02);
            _marajoaraUnitOfWork.CineRooms.Add(cineRoomToAdd03);
            _marajoaraUnitOfWork.Commit();

            _marajoaraUnitOfWork.Dispose();
            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            List<CineRoom> allCineRoomsOnDB = _marajoaraUnitOfWork.CineRooms.RetrieveAll().ToList();
            allCineRoomsOnDB.Should().NotBeNullOrEmpty();
            allCineRoomsOnDB.Should().HaveCount(3);
            allCineRoomsOnDB.Find(us => us.Name.Equals("cineRoom01")).Should().NotBeNull();
            allCineRoomsOnDB.Find(us => us.Name.Equals("cineRoom02")).Should().NotBeNull();
            allCineRoomsOnDB.Find(us => us.Name.Equals("cineRoom03")).Should().NotBeNull();

            _marajoaraUnitOfWork.Dispose();
        }
    }
}
