using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using System;

namespace Marajoara.Cinema.Management.Domain.SessionModule
{
    public class Session
    {
        private Movie _movie;
        private CineRoom _cineRoom;

        public int SessionID { get; set; }
        public DateTime SessionDate { get; set; }
        public decimal Price { get; set; }
        public int CineRoomID { get; set; }
        public CineRoom CineRoom
        {
            get { return _cineRoom; }
            set
            {
                _cineRoom = value;
                if (_cineRoom == null)
                    CineRoomID = 0;
                else
                    CineRoomID = _cineRoom.CineRoomID;
            }
        }
        public int MovieID { get; set; }
        public Movie Movie
        {
            get { return _movie; }
            set
            {
                _movie = value;
                if (_movie == null)
                    MovieID = 0;
                else
                    MovieID = _movie.MovieID;
            }
        }
    }
}