using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Authorization.Models
{
    public class AuthenticatedUserAccountModel
    {
        public int UserAccountID { get; set; }
        public string Name { get; set; }
        public string Mail{ get; set; }
        public string Token { get; set; }

    }
}
