using Marajoara.Cinema.Management.Application.Features.CineRoom.Commands;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.CineRoom.Handlers
{
    public class DeleteCineRoomHandler : IRequestHandler<DeleteCineRoomCommand, Result<Exception, bool>>
    {
        private readonly ICineRoomService _cineRoomService;
        public DeleteCineRoomHandler(ICineRoomService cineRoomService)
        {
            _cineRoomService = cineRoomService;
        }

        public Task<Result<Exception, bool>> Handle(DeleteCineRoomCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                return _cineRoomService.RemoveCineRoom(new Domain.CineRoomModule.CineRoom
                {
                    Name = request.Name
                });
            });

            return Task.FromResult(result);
        }
    }
}
