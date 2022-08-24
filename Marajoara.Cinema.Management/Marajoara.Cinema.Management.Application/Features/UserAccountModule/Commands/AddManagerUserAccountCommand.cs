using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands
{
    public class AddManagerUserAccountCommand : IRequest<Result<Exception, int>>
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
