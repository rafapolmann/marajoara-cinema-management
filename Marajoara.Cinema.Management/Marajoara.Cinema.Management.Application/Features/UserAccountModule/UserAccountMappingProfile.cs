using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Models;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule
{
    public class UserAccountMappingProfile : Profile
    {
        public UserAccountMappingProfile()
        {
            CreateMap<UserAccount, UserAccountModel>();
            CreateMap<AddCustomerUserAccountCommand, UserAccount>();
            CreateMap<AddAttendantUserAccountCommand, UserAccount>();
            CreateMap<AddManagerUserAccountCommand, UserAccount>();
            CreateMap<DeleteUserAccountCommand, UserAccount>();
        }
    }
}
