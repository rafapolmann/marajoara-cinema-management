namespace Marajoara.Cinema.Management.Application.Features.CineRoomModule.Models
{
    public class CineRoomModel
    {
        public int CineRoomID { get; set; }
        public string Name { get; set; }
        public int SeatsRow { get; set; }
        public int SeatsColumn { get; set; }
        public int TotalSeats { get; set; }
    }
}
