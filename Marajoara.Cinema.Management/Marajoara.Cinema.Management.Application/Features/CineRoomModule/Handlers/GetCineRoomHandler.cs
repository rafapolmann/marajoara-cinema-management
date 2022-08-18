using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Models;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Queries;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.CineRoomModule.Handlers
{
    public class GetCineRoomHandler : IRequestHandler<GetCineRoomQuery, Result<Exception, CineRoomModel>>
    {
        private readonly ICineRoomService _cineRoomService;
        public GetCineRoomHandler(ICineRoomService cineRoomService)
        {
            _cineRoomService = cineRoomService;
        }

        public Task<Result<Exception, CineRoomModel>> Handle(GetCineRoomQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, CineRoomModel> result = Result.Run(() =>
            {
                if (request.CineRoomID > 0)
                    return ConvertToCineRoomModel(_cineRoomService.GetCineRoom(request.CineRoomID));
                else
                    return ConvertToCineRoomModel(_cineRoomService.GetCineRoom(request.Name));
            });

            return Task.FromResult(result);
        }

        private CineRoomModel ConvertToCineRoomModel(CineRoom cineRoom)
        {
            if (cineRoom == null)
                return null;

            return new CineRoomModel
            {
                CineRoomID = cineRoom.CineRoomID,
                Name = cineRoom.Name,
                SeatNumbers = cineRoom.TotalSeats
            };
        }
    }
}
