using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Queries;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Handlers
{
    public class GetUserAccountPhotoHandler : IRequestHandler<GetUserAccountPhotoQuery, Result<Exception, byte[]>>
    {
        private readonly IUserAccountService _userAccountService;
        public GetUserAccountPhotoHandler(IUserAccountService userAccountService)
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
