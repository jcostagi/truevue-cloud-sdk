using log4net;
using Polly;
using Polly.Retry;
using Refit;
using System;
using System.Net;
using System.Threading.Tasks;
using TrueVUE.Cloud.SDK.API.Interfaces;

namespace TrueVUE.Cloud.SDK.API.Services
{
    public class NetworkService : INetworkService
    {
        //readonly IInternetAccessService _internetAccessService;
        private readonly ILog Logger = LogManager.GetLogger(typeof(NetworkService));

        //public NetworkService(IInternetAccessService internetAccessService)
        //{
        //    _internetAccessService = internetAccessService;
        //}
        public async Task<T> Retry<T>(Func<Task<T>> func)
        {
            //if (_internetAccessService.IsNotConnected)
            //{
            //    Logger.Error(Constants.MESSAGE_INTERNET_NOT_CONNECTED);
            //    throw new NotConnectedException(Constants.MESSAGE_INTERNET_NOT_CONNECTED);
            //}

            return await RetryInner(func);
        }

        public async Task<T> Retry<T>(Func<Task<T>> func, int retryCount)
        {
            //if (_internetAccessService.IsNotConnected)
            //{
            //    Logger.Error(Constants.MESSAGE_INTERNET_NOT_CONNECTED);
            //    throw new NotConnectedException(Constants.MESSAGE_INTERNET_NOT_CONNECTED);
            //}

            return await RetryInner(func, retryCount);
        }

        public async Task<T> Retry<T>(Func<Task<T>> func, int retryCount, Func<Exception, int, Task> onRetry)
        {
            //if (_internetAccessService.IsNotConnected)
            //{
            //    Logger.Error(Constants.MESSAGE_INTERNET_NOT_CONNECTED);
            //    throw new NotConnectedException(Constants.MESSAGE_INTERNET_NOT_CONNECTED);
            //}

            return await RetryInner(func, retryCount, onRetry);
        }

        public async Task<T> RetryWithRefreshToken<T>(Func<Task<T>> func, int retryCount, Func<Exception, int, Task> onRetryAsync, string refreshToken)
        {
            return await RetryInnerWithRefreshToken(func, retryCount, onRetryAsync, refreshToken);
        }

        public async Task<T> WaitAndRetry<T>(Func<Task<T>> func, Func<int, TimeSpan> sleepDurationProvider)
        {
            //if (_internetAccessService.IsNotConnected)
            //{
            //    Logger.Error(Constants.MESSAGE_INTERNET_NOT_CONNECTED);
            //    throw new NotConnectedException(Constants.MESSAGE_INTERNET_NOT_CONNECTED);
            //}

            return await WaitAndRetryInner<T>(func, sleepDurationProvider);
        }

        public async Task<T> WaitAndRetry<T>(Func<Task<T>> func, Func<int, TimeSpan> sleepDurationProvider, int retryCount)
        {
            //if (_internetAccessService.IsNotConnected)
            //{
            //    Logger.Error(Constants.MESSAGE_INTERNET_NOT_CONNECTED);
            //    throw new NotConnectedException(Constants.MESSAGE_INTERNET_NOT_CONNECTED);
            //}

            return await WaitAndRetryInner<T>(func, sleepDurationProvider, retryCount);
        }

        public async Task<T> WaitAndRetry<T>(Func<Task<T>> func, Func<int, TimeSpan> sleepDurationProvider, int retryCount, Func<Exception, TimeSpan, Task> onRetryAsync)
        {
            //if (_internetAccessService.IsNotConnected)
            //{
            //    Logger.Error(Constants.MESSAGE_INTERNET_NOT_CONNECTED);
            //    throw new NotConnectedException(Constants.MESSAGE_INTERNET_NOT_CONNECTED);
            //}

            return await WaitAndRetryInner<T>(func, sleepDurationProvider, retryCount, onRetryAsync);
        }

        #region Inner Methods

        internal async Task<T> RetryInner<T>(Func<Task<T>> func, int retryCount = 1, Func<Exception, int, Task> onRetry = null)
        {
            var onRetryInner = new Func<Exception, int, Task>((e, i) =>
            {
                return Task.Factory.StartNew(() => {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine($"Retry #{i} due to exception '{(e.InnerException ?? e).Message}'");
#endif
                });
            });

            return await CommomRetryPolicy(retryCount).ExecuteAsync<T>(func);
        }

        internal async Task<T> RetryInnerWithRefreshToken<T>(Func<Task<T>> func, int retryCount, Func<Exception, int, Task> onRetryAsync, string refreshToken)
        {
            var refreshTokenPolicy = Policy
                .HandleInner<ApiException>(ex => ex.StatusCode == HttpStatusCode.Unauthorized)
                .RetryAsync(retryCount, onRetryAsync);

            return await
                CommomRetryPolicy(retryCount)
                .WrapAsync(refreshTokenPolicy)
                .ExecuteAsync<T>(func);
        }

        internal async Task<T> WaitAndRetryInner<T>(Func<Task<T>> func, Func<int, TimeSpan> sleepDurationProvider, int retryCount = 1, Func<Exception, TimeSpan, Task> onRetryAsync = null)
        {
            var onRetryInner = new Func<Exception, TimeSpan, Task>((e, t) =>
            {
                return Task.Factory.StartNew(() => {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine($"Retrying in {t.ToString("g")} due to exception '{(e.InnerException ?? e).Message}'");
#endif
                });
            });

            return await Policy.Handle<Exception>().WaitAndRetryAsync(retryCount, sleepDurationProvider, onRetryAsync ?? onRetryInner).ExecuteAsync<T>(func);
        }
        #endregion

        private AsyncRetryPolicy CommomRetryPolicy(int retryCount)
        {
            return Policy
                .HandleInner<ApiException>()
                .RetryAsync(retryCount, (ex, count) =>
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine($"Retry #{count} due to exception '{(ex.InnerException ?? ex).Message}'");
#endif
                    Console.WriteLine($"Retrying #{count}. ERROR: {ex.Message}");
                    Logger.Error($"Retrying #{count}. ERROR: {ex.Message}");
                });
        }
    }
}
