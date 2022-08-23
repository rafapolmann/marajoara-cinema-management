using Marajoara.Cinema.Management.Domain.UserAccountModule;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Models
{
    public class UserAccountModel
    {
        public int UserAccountID { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public AccessLevel Level { set; get; }
    }
}
