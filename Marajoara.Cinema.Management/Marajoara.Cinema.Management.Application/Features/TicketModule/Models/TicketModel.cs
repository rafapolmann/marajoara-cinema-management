using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule.Models
{
    public class TicketModel
    {
        public int TicketID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Guid Code { get; set; }
        public decimal Price { get; set; }
        public int SeatNumber { get; set; }

        public bool Used { get; set; }

        public int UserAccountID { get; set; }
        public string UserAccountName { get; set; }
        
        
        public int SessionID { get; set; }        
        public DateTime SessionDate { get; set; }

        public int SessionMovieID { get; set; }
        public string SessionMovieTitle { get; set; }
        public bool SessionMovieIs3D { get; set; }
        public bool SessionMovieIsOriginalAudio { get; set; }

        public int SessionCineRoomID { get; set; }
        public string SessionCineRoomName { get; set; }

    }
}
