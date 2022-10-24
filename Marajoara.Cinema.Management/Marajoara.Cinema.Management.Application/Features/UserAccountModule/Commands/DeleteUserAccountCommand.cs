using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands
{
    public class DeleteUserAccountCommand : IRequest<Result<Exception, bool>>
    {
        public int UserAccountID{ get; set; }

        public DeleteUserAccountCommand(int userAccountID)
        {
            UserAccountID = userAccountID;  
        }
    }
}
