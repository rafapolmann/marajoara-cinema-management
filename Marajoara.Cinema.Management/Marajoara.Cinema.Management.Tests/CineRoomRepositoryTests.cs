using FluentAssertions;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Tests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Marajoara.Cinema.Management.Tests
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
        public void UnitOfWork_Should_Insert_CineRoom_In_Database()
        {
            _marajoaraUnitOfWork.CineRooms.Add(new CineRoom { Name = "cineRoom01", SeatsColumn = 50, SeatsRow = 30 });
            _marajoaraUnitOfWork.Commit();
            _marajoaraUnitOfWork.Dispose();

            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            var cineAdded = _marajoaraUnitOfWork.CineRooms.RetriveByName("cineRoom01");
            cineAdded.Should().NotBeNull();
            cineAdded.Name.Should().Be("cineRoom01");
            cineAdded.CineRoomID.Should().NotBe(0);

            _marajoaraUnitOfWork.Dispose();
        }

        [TestMethod]
        public void UnitOfWork_Should_Return_All_CineRooms_Inserted_In_Database()
        {
            _marajoaraUnitOfWork.CineRooms.Add(new CineRoom { Name = "cineRoom01", SeatsColumn = 50, SeatsRow = 10 });
            _marajoaraUnitOfWork.CineRooms.Add(new CineRoom { Name = "cineRoom02", SeatsColumn = 40, SeatsRow = 30 });
            _marajoaraUnitOfWork.CineRooms.Add(new CineRoom { Name = "cineRoom03", SeatsColumn = 20, SeatsRow = 20 });
            _marajoaraUnitOfWork.Commit();
            _marajoaraUnitOfWork.Dispose();

            _marajoaraUnitOfWork = GetNewEmptyUnitOfWorkInstance(false);

            var allCineRoomsOnDB = _marajoaraUnitOfWork.CineRooms.RetriveAll();
            allCineRoomsOnDB.Should().NotBeNullOrEmpty();
            allCineRoomsOnDB.Should().HaveCount(3);

            _marajoaraUnitOfWork.Dispose();
        }
    }
}
