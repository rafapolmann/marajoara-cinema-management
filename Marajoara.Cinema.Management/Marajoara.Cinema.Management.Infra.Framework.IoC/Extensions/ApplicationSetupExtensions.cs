using Marajoara.Cinema.Management.Application.Authorization;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule;
using Marajoara.Cinema.Management.Application.Features.MovieModule;
using Marajoara.Cinema.Management.Application.Features.SessionModule;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule;
using Marajoara.Cinema.Management.Domain.Authorization;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Ninject;

namespace Marajoara.Cinema.Management.Infra.Framework.IoC.Extensions
{
    public static class ApplicationSetupExtensions
    {
        public static void BindApplicationSetup(this IKernel kernel)
        {
            kernel.Bind<IUserAccountService>().To<UserAccountService>();
            kernel.Bind<ICineRoomService>().To<CineRoomService>();
            kernel.Bind<IMovieService>().To<MovieService>();
            kernel.Bind<ISessionService>().To<SessionService>();
            kernel.Bind<ITokenService>().To<TokenService>();
            kernel.Bind<IAuthorizationService>().To<AuthorizationService>();
        }
    }
}
