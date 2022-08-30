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
    public class AddCustomerUserAccountHandler : IRequestHandler<AddCustomerUserAccountCommand, Result<Exception, int>>
    {
        private readonly IMapper _mapper;
        private readonly IUserAccountService _userAccountService;
        public AddCustomerUserAccountHandler(IMapper mapper, IUserAccountService userAccountService)
        {
            _mapper = mapper;
            _userAccountService = userAccountService;
        }

        public Task<Result<Exception, int>> Handle(AddCustomerUserAccountCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, int> result = Result.Run(() =>
            {
                return _userAccountService.AddCustomerUserAccount(_mapper.Map<UserAccount>(request));
            });

            return Task.FromResult(result);
        }
    }
}
