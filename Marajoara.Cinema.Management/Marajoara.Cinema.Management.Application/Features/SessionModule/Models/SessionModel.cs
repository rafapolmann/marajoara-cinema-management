using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Models;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Models;
using System;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Models
{
    public class SessionModel
    {
        public int SessionID { get; set; }
        public DateTime SessionDate { get; set; }
        public DateTime EndSession { get; set; }
        public decimal Price { get; set; }
        public CineRoomModel CineRoom { get; set; }
        public MovieModel Movie { get; set; }
    }
}
