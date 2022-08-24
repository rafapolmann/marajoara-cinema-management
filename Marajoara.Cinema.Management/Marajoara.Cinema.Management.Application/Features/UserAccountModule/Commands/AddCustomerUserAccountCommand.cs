using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands
{
    public class AddCustomerUserAccountCommand : IRequest<Result<Exception, int>>
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
