using Microsoft.Extensions.DependencyInjection;
using TrueVUE.Cloud.SDK.Barcode;
using TrueVUE.Cloud.SDK.Barcode.Interfaces;
using TrueVUE.Cloud.SDK.Epc;

namespace TrueVUE.Cloud.SDK
{
    public static class Dependencies
    {
        public static void ConfigureSDK(this IServiceCollection services)
        {
            services.AddTransient<IEpcSDK, EpcSDK>();
            services.AddTransient<IBarcodeSDK, BarcodeSDK>();
        }
    }
}
