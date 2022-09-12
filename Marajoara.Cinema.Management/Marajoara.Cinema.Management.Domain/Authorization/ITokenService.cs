using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Domain.Authorization
{
    public interface ITokenService
    {
        byte[] Key { get; }

        public string GenerateToken(UserAccount user);
    }
}
