using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Application.Features.CineRoom
{
    public class CineRoomService : ICineRoomService
    {
        private readonly IMarajoaraUnitOfWork _unitOfWork;
        public CineRoomService(IMarajoaraUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddCineRoom(Domain.CineRoomModule.CineRoom cineRoom)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCineRoom(Domain.CineRoomModule.CineRoom cineRoom)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.CineRoomModule.CineRoom> RetrieveAll()
        {
            return _unitOfWork.CineRooms.RetrieveAll();
        }
    }
}
