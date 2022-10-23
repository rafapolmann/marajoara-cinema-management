using Marajoara.Cinema.Management.Application.Authorization.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Authorization.Commands
{
    public class AuthenticateCommand : IRequest<Result<Exception, AuthenticatedUserAccountModel>>
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
