using Marajoara.Cinema.Management.Application.Features.CineRoom.Models;
using Marajoara.Cinema.Management.Application.Features.CineRoom.Queries;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.CineRoom.Handlers
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
                foreach (Domain.CineRoomModule.CineRoom cineRoom in _cineRoomService.RetrieveAll())
                {
                    cineRoomsToReturn.Add(new CineRoomModel
                    {
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
