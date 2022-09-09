using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Queries
{
    public class GetUserAccountPhotoQuery : IRequest<Result<Exception, byte[]>>
    {
        public int UserAccountID { get; set; }
        public GetUserAccountPhotoQuery(int userAccountID)
        {
            UserAccountID = userAccountID;
        }
    }
}
