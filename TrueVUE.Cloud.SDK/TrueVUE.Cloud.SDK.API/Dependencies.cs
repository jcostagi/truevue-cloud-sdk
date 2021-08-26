using log4net;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using TrueVUE.Cloud.SDK.API.Endpoints;
using TrueVUE.Cloud.SDK.API.Handlers;
using TrueVUE.Cloud.SDK.API.Interfaces;
using TrueVUE.Cloud.SDK.API.Services;
using TrueVUE.Cloud.SDK.Common;
using TrueVUE.Cloud.SDK.Common.Interfaces;

namespace TrueVUE.Cloud.SDK.API
{
    public static class Dependencies
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Dependencies));

        public static void ConfigureApi(this IServiceCollection services)
        {
            try
            {

                _logger.Info("ConfigureApi");

                //var serviceProvider = services.BuildServiceProvider();

                services.AddTransient<IHttpService, HttpService>();
                services.AddSingleton<IApiService, ApiService>();
                services.AddTransient<IAuthHeaderHandler, AuthHeaderHandler>();
                services.AddTransient<IAuthTokenService, AuthTokenService>();
                services.AddTransient<INetworkService, NetworkService>();


                //services.AddTransient<ILazyDependency<IAuthenticationApi>>(x =>
                //    new LazyDependency<IAuthenticationApi>(() =>
                //    RestService.For<IAuthenticationApi>(((IHttpService)serviceProvider.GetRequiredService(typeof(IHttpService))).HttpClient, ((IHttpService)serviceProvider.GetRequiredService(typeof(IHttpService))).RefitSettings)));

                //services.AddTransient<ILazyDependency<IBusinessUnitApi>>(x =>
                //   new LazyDependency<IBusinessUnitApi>(() =>
                //   RestService.For<IBusinessUnitApi>(((IHttpService)serviceProvider.GetRequiredService(typeof(IHttpService))).HttpClient, ((IHttpService)serviceProvider.GetRequiredService(typeof(IHttpService))).RefitSettings)));

                //services.AddTransient<ILazyDependency<ICommissioningApi>>(x =>
                //    new LazyDependency<ICommissioningApi>(() =>
                //    RestService.For<ICommissioningApi>(((IHttpService)serviceProvider.GetRequiredService(typeof(IHttpService))).HttpClient, ((IHttpService)serviceProvider.GetRequiredService(typeof(IHttpService))).RefitSettings)));

                //services.AddTransient<ILazyDependency<IFacilityApi>>(x =>
                //    new LazyDependency<IFacilityApi>(() =>
                //    RestService.For<IFacilityApi>(((IHttpService)serviceProvider.GetRequiredService(typeof(IHttpService))).HttpClient, ((IHttpService)serviceProvider.GetRequiredService(typeof(IHttpService))).RefitSettings)));

                //services.AddTransient<ILazyDependency<IWriteTagApi>>(x =>
                //   new LazyDependency<IWriteTagApi>(() =>
                //   RestService.For<IWriteTagApi>(((IHttpService)serviceProvider.GetRequiredService(typeof(IHttpService))).HttpClient, ((IHttpService)serviceProvider.GetRequiredService(typeof(IHttpService))).RefitSettings)));

                //services.AddTransient<ILazyDependency<IProductApi>>(x =>
                //   new LazyDependency<IProductApi>(() =>
                //   RestService.For<IProductApi>(((IHttpService)serviceProvider.GetRequiredService(typeof(IHttpService))).HttpClient, ((IHttpService)serviceProvider.GetRequiredService(typeof(IHttpService))).RefitSettings)));

                //services.AddTransient<ILazyDependency<IPOSTransactionApi>>(x =>
                //   new LazyDependency<IPOSTransactionApi>(() =>
                //   RestService.For<IPOSTransactionApi>(((IHttpService)serviceProvider.GetRequiredService(typeof(IHttpService))).HttpClient, ((IHttpService)serviceProvider.GetRequiredService(typeof(IHttpService))).RefitSettings)));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}
