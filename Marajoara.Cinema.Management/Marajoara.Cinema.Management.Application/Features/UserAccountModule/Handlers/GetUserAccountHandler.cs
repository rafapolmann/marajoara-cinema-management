using AutoMapper;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Models;
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
    public class GetUserAccountHandler : IRequestHandler<GetUserAccountQuery, Result<Exception, UserAccountModel>>
    {
        private readonly IMapper _mapper;
        private readonly IUserAccountService _userAccountService;
        public GetUserAccountHandler(IMapper mapper, IUserAccountService userAccountService)
        {
            _mapper = mapper;
            _userAccountService = userAccountService;
        }

        public Task<Result<Exception, UserAccountModel>> Handle(GetUserAccountQuery request, CancellationToken cancellationToken)
        {
            Result<Exception, UserAccountModel> result = Result.Run(() =>
            {
                return _mapper.Map<UserAccountModel>(_userAccountService.GetUserAccountByID(request.UserAccountID));
            });

            return Task.FromResult(result);
        }
    }
}
