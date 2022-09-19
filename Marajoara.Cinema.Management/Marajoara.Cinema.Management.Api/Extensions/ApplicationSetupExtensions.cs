using Marajoara.Cinema.Management.Application.Authorization;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule;
using Marajoara.Cinema.Management.Application.Features.MovieModule;
using Marajoara.Cinema.Management.Application.Features.SessionModule;
using Marajoara.Cinema.Management.Application.Features.TicketModule;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule;
using Marajoara.Cinema.Management.Application.Features;
using Marajoara.Cinema.Management.Domain.Authorization;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.Common;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Microsoft.Extensions.DependencyInjection;

namespace Marajoara.Cinema.Management.Api.Extensions
{
    public static class ApplicationSetupExtensions
    {
        public static void AddApplicationSetup(this IServiceCollection services)
        {
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<ICineRoomService, CineRoomService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IFileImageService, FileImageService>();
        }
    }
}