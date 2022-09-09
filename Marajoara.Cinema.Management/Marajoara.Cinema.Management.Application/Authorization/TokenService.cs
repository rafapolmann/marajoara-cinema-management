using Marajoara.Cinema.Management.Domain.Authorization;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Marajoara.Cinema.Management.Application.Authorization
{
    public class TokenService : ITokenService
    {
        
        private readonly byte[] _key = Encoding.ASCII.GetBytes("YOUSHALLNOTPASS!");//Todo: put this somewhere else
        public byte[] Key { get { return _key; } }


        public string GenerateToken(UserAccount user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserAccountID", user.UserAccountID.ToString()),
                    new Claim(ClaimTypes.Name, user.Name.ToString()),                    
                    new Claim(ClaimTypes.Role, user.Level.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
