using Marajoara.Cinema.Management.Application.Features.CineRoom.Commands;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.CineRoom.Handlers
{
    public class AddCineRoomHandler : IRequestHandler<AddCineRoomCommand, Result<Exception, int>>
    {
        private readonly ICineRoomService _cineRoomService;
        public AddCineRoomHandler(ICineRoomService cineRoomService)
        {
            _cineRoomService = cineRoomService;
        }

        public Task<Result<Exception, int>> Handle(AddCineRoomCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, int> result = Result.Run(() =>
            {
                return _cineRoomService.AddCineRoom(new Domain.CineRoomModule.CineRoom
                {
                    Name = request.Name,
                    SeatsColumn = request.ColumnsNumber,
                    SeatsRow = request.RowsNumber
                });
            });

            return Task.FromResult(result);
        }
    }
}
