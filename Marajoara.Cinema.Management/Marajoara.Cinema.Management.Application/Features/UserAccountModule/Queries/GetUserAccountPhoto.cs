using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Queries
{
    public class GetUserAccountPhoto : IRequestHandler<GetUserAccountPhotoQuery, Result<Exception, byte[]>>
    {
        private readonly IUserAccountService _userAccountService;
        public GetUserAccountPhoto(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public Task<Result<Exception, byte[]>> Handle(GetUserAccountPhotoQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, byte[]> result = Result.Run(() =>
            {
                return _userAccountService.GetUserAccountPhoto(request.UserAccountID);
            });

            return Task.FromResult(result);
        }
    }
}
