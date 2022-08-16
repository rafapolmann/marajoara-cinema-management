using Marajoara.Cinema.Management.Application.Features.CineRoom.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Application.Features.CineRoom.Queries
{
    public class AllCineRoomsQuery : IRequest<Result<Exception, List<CineRoomModel>>> { }
}
