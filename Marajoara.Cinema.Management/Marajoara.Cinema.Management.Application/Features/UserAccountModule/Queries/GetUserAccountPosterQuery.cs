using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Queries
{
    public class GetUserAccountPosterQuery : IRequest<Result<Exception, byte[]>>
    {
        public int UserAccount { get; set; }
        public GetUserAccountPosterQuery(int userAccountID)
        {
            UserAccount = userAccountID;
        }
    }
}
