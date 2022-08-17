using FluentAssertions;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Tests.Unit.Application
{
    [TestClass]
    public class CineRoomServiceTests
    {
        private ICineRoomService _cineRoomService;
        private Mock<MarajoaraUnitOfWork> _unitOfWorkMock;
        private Mock<ICineRoomRepository> _cineRoomRepository;

        [TestInitialize]
        public void Initialize()
        {
            _cineRoomRepository = new Mock<ICineRoomRepository>();
            _unitOfWorkMock = new Mock<MarajoaraUnitOfWork>(null, _cineRoomRepository.Object, null, null, null, null);
            _cineRoomService = new CineRoomService(_unitOfWorkMock.Object);
        }

        [TestMethod]
        public void CineRoomService_RetrieveAll_Should_Returns_All_CineRooms()
        {
            _cineRoomRepository.Setup(api => api.RetrieveAll()).Returns(new List<CineRoom> { new CineRoom { Name = "CineRoom" } });
            _cineRoomService.RetrieveAll().Should().HaveCount(1);
        }
    }
}
