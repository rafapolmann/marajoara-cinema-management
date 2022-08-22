using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Commands;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Models;
using Marajoara.Cinema.Management.Domain.CineRoomModule;

namespace Marajoara.Cinema.Management.Application.Features.CineRoomModule
{
    public class CineRoomMappingProfile : Profile
    {
        public CineRoomMappingProfile()
        {
            CreateMap<CineRoom, CineRoomModel>();
            CreateMap<AddCineRoomCommand, CineRoom>();
            CreateMap<DeleteCineRoomCommand, CineRoom>();
            CreateMap<UpdateCineRoomCommand, CineRoom>();
        }
    }
}
