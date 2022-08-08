using System.IO;

namespace Marajoara.Cinema.Management.Domain.UserAccountModule
{
    public class UserAccount
    {
        public int UserAccountID { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public AccessLevel Level { set; get; }
        public byte[] Photo { get; set; }
    }
}
