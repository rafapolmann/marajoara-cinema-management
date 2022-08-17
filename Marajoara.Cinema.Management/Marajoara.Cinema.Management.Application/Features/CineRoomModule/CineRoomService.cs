using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Application.Features.CineRoomModule
{
    public class CineRoomService : ICineRoomService
    {
        private readonly IMarajoaraUnitOfWork _unitOfWork;
        public CineRoomService(IMarajoaraUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int AddCineRoom(CineRoom cineRoom)
        {
            int totalSeats = cineRoom.SeatsColumn * cineRoom.SeatsRow;
            if (totalSeats <= 0)
                throw new Exception($"Seat number cannot be equals zero or negative.");
            if (totalSeats > 100)
                throw new Exception($"Invalid seat number \"{totalSeats}\". Max of seat per cine room is 100.");
            if (_unitOfWork.CineRooms.RetrieveByName(cineRoom.Name) != null)
                throw new Exception($"Already exists cine room with name {cineRoom.Name}.");

            _unitOfWork.CineRooms.Add(cineRoom);
            _unitOfWork.Commit();

            return cineRoom.CineRoomID;
        }

        public bool RemoveCineRoom(CineRoom cineRoom)
        {
            CineRoom cineRoomToDelete = _unitOfWork.CineRooms.RetrieveByName(cineRoom.Name);
            if (cineRoomToDelete == null)
                throw new Exception($"Cine room \"{cineRoom.Name}\" not found.");

            _unitOfWork.CineRooms.Delete(cineRoomToDelete);
            _unitOfWork.Commit();

            return true;
        }

        public IEnumerable<CineRoom> RetrieveAll()
        {
            return _unitOfWork.CineRooms.RetrieveAll();
        }
    }
}
