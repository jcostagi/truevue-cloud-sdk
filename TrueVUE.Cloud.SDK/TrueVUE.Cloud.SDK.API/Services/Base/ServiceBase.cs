using System.Net.Http;
using TrueVUE.Cloud.SDK.API.Endpoints;
using TrueVUE.Cloud.SDK.API.Interfaces;

namespace TrueVUE.Cloud.SDK.API.Services.Base
{
    public class ServiceBase
    {
        protected IApiService ApiService;
        protected IServiceOptions Options;

        public ServiceBase(IApiService apiService, IServiceOptions options)
        {
            ApiService = apiService;
            Options = options;
        }
    }
}
