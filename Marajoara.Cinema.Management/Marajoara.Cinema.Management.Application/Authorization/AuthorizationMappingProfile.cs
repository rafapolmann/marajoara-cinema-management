using AutoMapper;
using Marajoara.Cinema.Management.Application.Authorization.Commands;
using Marajoara.Cinema.Management.Application.Authorization.Models;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Authorization
{
    public class AuthorizationMappingProfile : Profile
    {
        public AuthorizationMappingProfile()
        {
            CreateMap<AuthenticateCommand, UserAccount>();
            CreateMap< UserAccount, AuthenticatedUserAccountModel>();
            
        }
    }
}
