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
