using Microsoft.Extensions.DependencyInjection;
using TrueVUE.Cloud.SDK.API.Interfaces;
using TrueVUE.Cloud.SDK.API.Services;

namespace TrueVUE.Cloud.SDK.API
{
    public static class Dependencies
    {
        public static void ConfigureApi(this IServiceCollection services)
        {
            services.AddTransient<INetworkService, NetworkService>();
            services.AddTransient<IAuthTokenService, AuthTokenService>();
            //services.AddTransient<IAuthHeaderHandler, Handlers.AuthHeaderHandler>();
            services.AddSingleton<IApiService, ApiService>();
            services.AddTransient<IHttpService, HttpService>();
        }
    }
}
