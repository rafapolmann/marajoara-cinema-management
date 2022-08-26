using Marajoara.Cinema.Management.Application.Features.SessionModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Queries
{
    public class GetSessionsByCineRoomQuery : IRequest<Result<Exception, List<SessionModel>>>
    {
        public int CineRoomID { get; set; }
        public GetSessionsByCineRoomQuery(int cineRoomID)
        {
            CineRoomID = cineRoomID;
        }
    }
}
