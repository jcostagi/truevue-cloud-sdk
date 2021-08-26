using System.Net.Http;
using TrueVUE.Cloud.SDK.API.Endpoints;
using TrueVUE.Cloud.SDK.API.Interfaces;

namespace TrueVUE.Cloud.SDK.API.Services.Base
{
    public class ServiceBase
    {
        protected IApiService ApiService;
        protected IApiOptions Options;

        public ServiceBase(IApiService apiService, IApiOptions options)
        {
            ApiService = apiService;
            Options = options;
        }
    }
}
