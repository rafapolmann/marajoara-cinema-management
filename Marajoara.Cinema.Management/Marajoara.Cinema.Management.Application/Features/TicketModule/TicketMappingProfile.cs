using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Commands;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Models;
using Marajoara.Cinema.Management.Domain.TicketModule;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule
{
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile()
        {
            CreateMap<Ticket, TicketModel>();
            CreateMap<Ticket, TicketSeatModel>();
            //CreateMap<Ticket, AddTicketCommand>().ReverseMap().ForPath(cmd => cmd.SessionID, a => a.MapFrom(t => t.SessionID));
            CreateMap< AddTicketCommand, Ticket>();
            CreateMap<DeleteTicketCommand, Ticket>();

        }
    }
}
