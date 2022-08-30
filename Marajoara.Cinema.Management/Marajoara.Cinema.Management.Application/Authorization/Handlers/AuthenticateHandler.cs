using AutoMapper;
using Marajoara.Cinema.Management.Application.Authorization.Commands;
using Marajoara.Cinema.Management.Application.Authorization.Models;
using Marajoara.Cinema.Management.Domain.Authorization;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Authorization.Handlers
{
    public class AuthenticateHandler : IRequestHandler<AuthenticateCommand, Result<Exception, AuthenticatedUserAccountModel>>
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IAuthorizationService _authService;

        public AuthenticateHandler(IMapper mapper, IAuthorizationService authService, ITokenService tokenService)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _authService = authService;
        }

        public Task<Result<Exception, AuthenticatedUserAccountModel>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            Result<Exception, AuthenticatedUserAccountModel> result = Result.Run(() =>
            {
                var user = _mapper.Map<UserAccount>(request);
                var dbUser = _authService.Authorize(user);   
                var authUser = _mapper.Map<AuthenticatedUserAccountModel>(dbUser);
                authUser.Token = _tokenService.GenerateToken(dbUser);
                
                return authUser;
            });

            return Task.FromResult(result);
        }
    }
}
