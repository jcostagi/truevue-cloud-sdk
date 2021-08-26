using System;
using System.Threading.Tasks;
using TrueVUE.Cloud.SDK.API.Interfaces;

namespace TrueVUE.Cloud.SDK.API.Services
{
    public class ApiService : IApiService
    {
        readonly INetworkService _networkService;

        public ApiService(INetworkService networkService)
        {
            _networkService = networkService;
        }

        #region With Retries

        public async Task<T> CallAndRetry<T>(Func<Task<T>> func, int retryCount, Func<Exception, int, Task> onRetry = null) where T : class
        {
            return await _networkService.Retry<T>(func, retryCount, onRetry);
        }

        #endregion
    }
}
