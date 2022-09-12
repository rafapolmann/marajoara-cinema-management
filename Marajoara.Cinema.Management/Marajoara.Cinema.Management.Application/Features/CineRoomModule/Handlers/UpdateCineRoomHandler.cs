using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Commands;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.CineRoomModule.Handlers
{
    public class UpdateCineRoomHandler : IRequestHandler<UpdateCineRoomCommand, Result<Exception, bool>>
    {
        private readonly IMapper _mapper;
        private readonly ICineRoomService _cineRoomService;
        public UpdateCineRoomHandler(IMapper mapper, ICineRoomService cineRoomService)
        {
            _mapper = mapper;
            _cineRoomService = cineRoomService;
        }

        public Task<Result<Exception, bool>> Handle(UpdateCineRoomCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                return _cineRoomService.UpdateCineRoom(_mapper.Map<CineRoom>(request));
            });

            return Task.FromResult(result);
        }
    }
}
