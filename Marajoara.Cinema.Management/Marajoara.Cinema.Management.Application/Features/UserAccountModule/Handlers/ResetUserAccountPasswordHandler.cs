using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Handlers
{
    public class ResetUserAccountPasswordHandler : IRequestHandler<ResetUserAccountPasswordCommand, Result<Exception, bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUserAccountService _userAccountService;
        public ResetUserAccountPasswordHandler(IMapper mapper, IUserAccountService userAccountService)
        {
            _mapper = mapper;
            _userAccountService = userAccountService;
        }

        public Task<Result<Exception, bool>> Handle(ResetUserAccountPasswordCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                return _userAccountService.ResetUserAccountPassword(_mapper.Map<UserAccount>(request));
            });

            return Task.FromResult(result);
        }
    }
}
