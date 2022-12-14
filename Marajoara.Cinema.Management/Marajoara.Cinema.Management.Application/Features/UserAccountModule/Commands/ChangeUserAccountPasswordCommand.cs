using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands
{
    public class ChangeUserAccountPasswordCommand : IRequest<Result<Exception, bool>>
    {
        public int UserAccountID { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
