using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using System;

namespace Marajoara.Cinema.Management.Domain.SessionModule
{
    public class Session
    {
        public int SessionID { get; set; }
        public DateTime SessionDate { get; set; }
        public DateTime SessionTime { get; set; }
        public CineRoom CineRoom { get; set; }
        public Movie Movie { get; set; }
        public decimal Price { get; set; }
    }
}