using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

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
            ValidateCineRoom(cineRoom);

            if (_unitOfWork.CineRooms.RetrieveByName(cineRoom.Name) != null)
                throw new Exception($"Already exists cine room with name {cineRoom.Name}.");

            _unitOfWork.CineRooms.Add(cineRoom);
            _unitOfWork.Commit();

            return cineRoom.CineRoomID;
        }

        public bool RemoveCineRoom(CineRoom cineRoom)
        {
            if (cineRoom == null)
                throw new ArgumentException("CineRoom parameter cannot be null.", nameof(cineRoom));

            CineRoom cineRoomToDelete = cineRoom.CineRoomID > 0 ?
                                        _unitOfWork.CineRooms.Retrieve(cineRoom.CineRoomID) :
                                        _unitOfWork.CineRooms.RetrieveByName(cineRoom.Name);

            if (cineRoomToDelete == null)
                throw new Exception($"Cine room not found.");
            if (_unitOfWork.Sessions.RetrieveByCineRoom(cineRoomToDelete).ToList().Count > 0)
                throw new Exception($"Cannot possible remove cine room {cineRoomToDelete.Name}. There are sessions linked with this cine room.");

            _unitOfWork.CineRooms.Delete(cineRoomToDelete);
            _unitOfWork.Commit();

            return true;
        }

        public IEnumerable<CineRoom> GetAllCineRooms()
        {
            return _unitOfWork.CineRooms.RetrieveAll();
        }

        public CineRoom GetCineRoom(int id)
        {
            return _unitOfWork.CineRooms.Retrieve(id);
        }

        public CineRoom GetCineRoom(string cineRoomName)
        {
            return _unitOfWork.CineRooms.RetrieveByName(cineRoomName);
        }

        public bool UpdateCineRoom(CineRoom cineRoom)
        {
            ValidateCineRoom(cineRoom);

            CineRoom cineRoomOnDB = _unitOfWork.CineRooms.Retrieve(cineRoom.CineRoomID);
            if (cineRoomOnDB == null)
                throw new Exception($"Cine room to update not found.");
            if (!cineRoomOnDB.Name.Equals(cineRoom.Name) && _unitOfWork.CineRooms.RetrieveByName(cineRoom.Name) != null)
                throw new Exception($"Already exists cine room with name {cineRoom.Name}.");

            cineRoom.CopyTo(cineRoomOnDB);

            _unitOfWork.CineRooms.Update(cineRoomOnDB);
            _unitOfWork.Commit();

            return true;
        }

        private void ValidateCineRoom(CineRoom cineRoom)
        {
            if (cineRoom == null)
                throw new ArgumentException("CineRoom parameter cannot be null.", nameof(cineRoom));

            cineRoom.Validate();
        }
    }
}
