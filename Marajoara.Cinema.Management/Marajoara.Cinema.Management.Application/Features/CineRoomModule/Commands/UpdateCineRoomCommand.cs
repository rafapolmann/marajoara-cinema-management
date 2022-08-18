using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.CineRoomModule.Commands
{
    public class UpdateCineRoomCommand : IRequest<Result<Exception, bool>>
    {
        public int CineRoomID { get; set; }
        public string Name { get; set; }
        public int ColumnsNumber { get; set; }
        public int RowsNumber { get; set; }
    }
}
