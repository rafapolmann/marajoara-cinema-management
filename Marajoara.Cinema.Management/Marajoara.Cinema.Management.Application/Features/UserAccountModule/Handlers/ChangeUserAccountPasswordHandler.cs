using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;
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
    internal class ChangeUserAccountPasswordHandler : IRequestHandler<ChangeUserAccountPasswordCommand, Result<Exception, bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUserAccountService _userAccountService;
        public ChangeUserAccountPasswordHandler(IMapper mapper, IUserAccountService userAccountService)
        {
            _mapper = mapper;
            _userAccountService = userAccountService;
        }

        public Task<Result<Exception, bool>> Handle(ChangeUserAccountPasswordCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                return _userAccountService.ChangeUserAccountPassword(_mapper.Map<UserAccount>(request), request.NewPassword);
            });

            return Task.FromResult(result);
        }
    }
}
