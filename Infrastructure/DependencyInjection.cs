using Infrastructure.Helper;
using Infrastructure.Helper.DtoRefresh;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            return services;
        }
    }
}
