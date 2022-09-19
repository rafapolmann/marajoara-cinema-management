using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Marajoara.Cinema.Management.Api.Extensions
{
    public static class CorsExtensions
    {
        private const string MARAJOARA_CORS_POLICY = "MarojoaraCorsPolicy";

        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MARAJOARA_CORS_POLICY, builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyMethod();
                });
            });
        }

        public static void UseCorsPolicy(this IApplicationBuilder application)
        {
            application.UseCors(MARAJOARA_CORS_POLICY);
        }
    }
}
