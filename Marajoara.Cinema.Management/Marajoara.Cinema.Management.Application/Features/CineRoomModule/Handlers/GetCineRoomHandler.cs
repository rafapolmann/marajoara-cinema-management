using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ICineRoomService _cineRoomService;
        public GetCineRoomHandler(IMapper mapper, ICineRoomService cineRoomService)
        {
            _mapper = mapper;
            _cineRoomService = cineRoomService;
        }

        public Task<Result<Exception, CineRoomModel>> Handle(GetCineRoomQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, CineRoomModel> result = Result.Run(() =>
            {
                if (request.CineRoomID > 0)
                    return _mapper.Map<CineRoomModel>(_cineRoomService.GetCineRoom(request.CineRoomID));
                else
                    return _mapper.Map<CineRoomModel>(_cineRoomService.GetCineRoom(request.Name));
            });

            return Task.FromResult(result);
        }
    }
}
