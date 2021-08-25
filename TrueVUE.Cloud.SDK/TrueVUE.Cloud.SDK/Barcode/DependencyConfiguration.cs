using Microsoft.Extensions.DependencyInjection;
using TrueVUE.Cloud.SDK.Barcode.Interfaces;

namespace TrueVUE.Cloud.SDK.Barcode
{
    public static class DependencyConfiguration
    {
        public static void Configure(this IServiceCollection services)
        {
            services.AddTransient<IBarcodeSDK, BarcodeSDK>();
        }
    }
}
