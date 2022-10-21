using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Queries
{
    public class GetUserAccountQuery: IRequest<Result<Exception, UserAccountModel>> 
    {
        public int UserAccountID { get; set; }
        public GetUserAccountQuery(int userAccountID)
        {
            UserAccountID = userAccountID;
        }
    }
}
