namespace Marajoara.Cinema.Management.Domain.UserModule
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public AccessLevel Level { set; get; }
    }
}
