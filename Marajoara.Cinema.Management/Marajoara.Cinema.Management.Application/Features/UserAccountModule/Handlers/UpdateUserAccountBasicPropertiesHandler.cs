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
    public class UpdateUserAccountBasicPropertiesHandler : IRequestHandler<UpdateUserAccountBasicPropertiesCommand, Result<Exception, bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUserAccountService _userAccountService;
        public UpdateUserAccountBasicPropertiesHandler(IMapper mapper, IUserAccountService userAccountService)
        {
            _mapper = mapper;
            _userAccountService = userAccountService;
        }

        public Task<Result<Exception, bool>> Handle(UpdateUserAccountBasicPropertiesCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, bool> result = Result.Run(() =>
            {
                return _userAccountService.UpdateUserAccountBasicProperties(_mapper.Map<UserAccount>(request));
            });

            return Task.FromResult(result);
        }
    }
}
