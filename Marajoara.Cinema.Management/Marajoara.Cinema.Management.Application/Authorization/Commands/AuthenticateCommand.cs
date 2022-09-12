using Marajoara.Cinema.Management.Application.Authorization.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Authorization.Commands
{
    public class AuthenticateCommand : IRequest<Result<Exception, AuthenticatedUserAccountModel>>
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
