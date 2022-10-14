using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands
{
    public class UpdateUserAccountBasicPropertiesCommand : IRequest<Result<Exception, bool>>
    {
        public int UserAccountID { get; set; }
        public string Name { get; set; }
        public AccessLevel Level { get; set; }
    }
}
