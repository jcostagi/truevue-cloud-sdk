using Microsoft.Extensions.DependencyInjection;

namespace TrueVUE.Cloud.SDK.Epc
{
    public static class DependencyConfiguration
    {
        public static void Configure(this IServiceCollection services)
        {
            services.AddTransient<IEpcValidator>(_ => new EpcValidator
            {
                SGTIN96Enabled = true,
                SSCC96Enabled = true,
                SGLN96Enabled = true,
                GID96Enabled = true,
                VUESERIAL96Enabled = true,
                VUESERIALAUTORITY96Enabled = true,
            });

            services.AddTransient<IEpcSDK, EpcSDK>();
        }
    }
}
