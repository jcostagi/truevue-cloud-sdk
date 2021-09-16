using Refit;
using System;
using System.Threading.Tasks;
using TrueVUE.Cloud.SDK.API.Exceptions;
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

        public async Task<T> CallAndRetry<T>(ApiOptions apiOptions, Func<Task<T>> func, int retryCount, Func<Exception, int, Task> onRetry = null) where T : class
        {
            try
            {
                return await _networkService.Retry<T>(func, retryCount, onRetry);
            }
            catch (ApiException ex)
            {
                throw new ApiConnectionException(ex.Content, ex.StatusCode);
            }

        }

        public async Task<T> CallAndRetry<T>(Func<Task<T>> func, int retryCount, Func<Exception, int, Task> onRetry = null) where T : class
        {
            try
            {
                return await _networkService.Retry<T>(func, retryCount, onRetry);
            }
            catch (ApiException ex)
            {
                throw new ApiConnectionException(ex.Content, ex.StatusCode);
            }

        }

        #endregion
    }
}
