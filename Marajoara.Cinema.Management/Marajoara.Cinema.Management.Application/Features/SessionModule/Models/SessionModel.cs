using System;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Models
{
    public class SessionModel
    {
        public int SessionID { get; set; }
        public DateTime SessionDate { get; set; }
        public decimal Price { get; set; }
        public int CineRoomID { get; set; }
        public int MovieID { get; set; }
    }
}
