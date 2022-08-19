using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ICineRoomService _cineRoomService;
        public AllCineRoomsHandler(IMapper mapper, ICineRoomService cineRoomService)
        {
            _mapper = mapper;
            _cineRoomService = cineRoomService;
        }

        public Task<Result<Exception, List<CineRoomModel>>> Handle(AllCineRoomsQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, List<CineRoomModel>> result = Result.Run(() =>
            {
                return _mapper.Map<List<CineRoomModel>>(_cineRoomService.GetAllCineRooms());
            });

            return Task.FromResult(result);
        }
    }
}
