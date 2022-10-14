using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands
{
    public class AddAttendantUserAccountCommand : AddUserAccountCommand, IRequest<Result<Exception, int>>
    {
    }
}
