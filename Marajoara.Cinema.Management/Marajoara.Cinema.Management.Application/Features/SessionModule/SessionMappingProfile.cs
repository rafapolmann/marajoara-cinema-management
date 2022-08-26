using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Commands;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Models;
using Marajoara.Cinema.Management.Domain.SessionModule;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule
{
    public class SessionMappingProfile: Profile
    {
        public SessionMappingProfile()
        {
            CreateMap<Session, SessionModel>();
            CreateMap<AddSessionCommand, Session>();
            CreateMap<DeleteSessionCommand, Session>(); 
        }
    }
}
