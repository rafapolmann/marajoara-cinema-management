
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Handlers
{
    public class DeleteUserAccountPhotoHandler : IRequestHandler<DeleteUserAccountPhotoCommand, Result<Exception, bool>>
    {
        private readonly IUserAccountService _userAccountService;
        public DeleteUserAccountPhotoHandler(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public Task<Result<Exception, bool>> Handle(DeleteUserAccountPhotoCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                return _userAccountService.DeleteUserAccountPhoto(request.UserAccountID);
            });

            return Task.FromResult(result);
        }
    }
}
