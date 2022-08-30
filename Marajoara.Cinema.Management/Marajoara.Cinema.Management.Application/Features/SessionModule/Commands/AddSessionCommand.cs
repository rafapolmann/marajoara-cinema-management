using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Commands
{
    public class AddSessionCommand : IRequest<Result<Exception, int>>
    {
        public DateTime SessionDate { get; set; }
        public decimal Price { get; set; }
        public int CineRoomID { get; set; }
        public int MovieID { get; set; }
    }
}
