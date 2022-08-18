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
        private readonly ICineRoomService _cineRoomService;
        public UpdateCineRoomHandler(ICineRoomService cineRoomService)
        {
            _cineRoomService = cineRoomService;
        }

        public Task<Result<Exception, bool>> Handle(UpdateCineRoomCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                return _cineRoomService.UpdateCineRoom(new CineRoom
                {
                    CineRoomID = request.CineRoomID,
                    Name = request.Name,
                    SeatsColumn = request.ColumnsNumber,
                    SeatsRow = request.RowsNumber
                });
            });

            return Task.FromResult(result);
        }
    }
}
