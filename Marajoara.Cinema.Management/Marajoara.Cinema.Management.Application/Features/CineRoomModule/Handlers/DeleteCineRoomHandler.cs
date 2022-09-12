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
    public class DeleteCineRoomHandler : IRequestHandler<DeleteCineRoomCommand, Result<Exception, bool>>
    {
        private readonly IMapper _mapper;
        private readonly ICineRoomService _cineRoomService;
        public DeleteCineRoomHandler(IMapper mapper, ICineRoomService cineRoomService)
        {
            _mapper = mapper;
            _cineRoomService = cineRoomService;
        }

        public Task<Result<Exception, bool>> Handle(DeleteCineRoomCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                return _cineRoomService.RemoveCineRoom(_mapper.Map<CineRoom>(request));
            });

            return Task.FromResult(result);
        }
    }
}
