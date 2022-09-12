using System;

namespace Marajoara.Cinema.Management.Domain.CineRoomModule
{
    public class CineRoom
    {
        public int CineRoomID { get; set; }
        public string Name { get; set; }
        public int SeatsRow { get; set; }
        public int SeatsColumn { get; set; }
        public int TotalSeats
        {
            get { return SeatsColumn * SeatsRow; }
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                throw new Exception($"Cine room name cannot be null or empty.");
            if (TotalSeats <= 0)
                throw new Exception($"Seat number cannot be equals zero or negative.");
            if (TotalSeats > 100)
                throw new Exception($"Invalid seat number \"{TotalSeats}\". Max of seat per cine room is 100.");
            if (TotalSeats < 20)
                throw new Exception($"Invalid seat number \"{TotalSeats}\". Min of seat per cine room is 20.");
            
            return true;
        }


        public void CopyTo(CineRoom cineRoomToCopy)
        {
            ValidateCopyToArguments(cineRoomToCopy);
            
            cineRoomToCopy.Name = Name;
            cineRoomToCopy.SeatsRow = SeatsRow;
            cineRoomToCopy.SeatsColumn = SeatsColumn;
        }

        private void ValidateCopyToArguments(CineRoom cineRoomToCopy)
        {
            if (cineRoomToCopy == null)
                throw new ArgumentException("CineRoom parameter cannot be null.", nameof(cineRoomToCopy));
            if (cineRoomToCopy.Equals(this))
                throw new ArgumentException("Cine room to copy cannot be the same instance of the origin.", nameof(cineRoomToCopy));
        }
    }
}
