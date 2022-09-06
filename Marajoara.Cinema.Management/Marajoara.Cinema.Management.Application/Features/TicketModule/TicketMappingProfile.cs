using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Commands;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Models;
using Marajoara.Cinema.Management.Domain.TicketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule
{
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile()
        {
            CreateMap<Ticket, TicketModel>();
            CreateMap<Ticket, TicketSeatModel>();
            CreateMap<Ticket, AddTicketCommand>().ReverseMap().ForPath(cmd => cmd.SessionID, a => a.MapFrom(t => t.SessionID));
        }
    }
}
