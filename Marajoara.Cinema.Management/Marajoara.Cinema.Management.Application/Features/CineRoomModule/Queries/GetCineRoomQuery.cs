using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.CineRoomModule.Queries
{
    public class GetCineRoomQuery : IRequest<Result<Exception, CineRoomModel>>
    {
        public int CineRoomID { get; set; }
        public string Name { get; set; }

        public GetCineRoomQuery(string name)
        {
            Name = name;
        }

        public GetCineRoomQuery(int id)
        {
            CineRoomID = id;
        }
    }
}
