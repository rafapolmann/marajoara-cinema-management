using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Models;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Queries;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.CineRoomModule.Handlers
{
    public class AllCineRoomsHandler : IRequestHandler<AllCineRoomsQuery, Result<Exception, List<CineRoomModel>>>
    {
        private readonly ICineRoomService _cineRoomService;
        public AllCineRoomsHandler(ICineRoomService cineRoomService)
        {
            _cineRoomService = cineRoomService;
        }

        public Task<Result<Exception, List<CineRoomModel>>> Handle(AllCineRoomsQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, List<CineRoomModel>> result = Result.Run(() =>
            {
                List<CineRoomModel> cineRoomsToReturn = new List<CineRoomModel>();
                foreach (CineRoom cineRoom in _cineRoomService.RetrieveAll())
                {
                    cineRoomsToReturn.Add(new CineRoomModel
                    {
                        CineRoomID = cineRoom.CineRoomID,
                        Name = cineRoom.Name,
                        SeatNumbers = cineRoom.TotalSeats
                    });
                }
                return cineRoomsToReturn;
            });

            return Task.FromResult(result);
        }
    }
}
