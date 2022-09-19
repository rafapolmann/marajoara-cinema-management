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

        /// <summary>
        /// Generates a new jwt token for a given user.
        /// </summary>
        /// <param name="user">Logged UserAccount</param>
        /// <returns>jwt token string</returns>
        public string GenerateToken(UserAccount user);
    }
}
