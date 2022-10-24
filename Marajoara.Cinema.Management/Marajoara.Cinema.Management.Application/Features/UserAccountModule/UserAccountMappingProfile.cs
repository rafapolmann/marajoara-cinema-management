using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Models;
using Marajoara.Cinema.Management.Domain.UserAccountModule;

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
            CreateMap<UpdateUserAccountBasicPropertiesCommand, UserAccount>();
            CreateMap<ResetUserAccountPasswordCommand, UserAccount>();
            CreateMap<ChangeUserAccountPasswordCommand, UserAccount>();
        }
    }
}
