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
    }
}
