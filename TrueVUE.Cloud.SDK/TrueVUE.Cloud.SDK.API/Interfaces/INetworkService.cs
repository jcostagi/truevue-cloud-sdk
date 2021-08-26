using System;
using System.Threading.Tasks;

namespace TrueVUE.Cloud.SDK.API.Interfaces
{
    public interface INetworkService
    {
        Task<T> Retry<T>(Func<Task<T>> func);
        Task<T> Retry<T>(Func<Task<T>> func, int retryCount);
        Task<T> Retry<T>(Func<Task<T>> func, int retryCount, Func<Exception, int, Task> onRetry);
        Task<T> RetryWithRefreshToken<T>(Func<Task<T>> func, int retryCount, Func<Exception, int, Task> onRetryAsync, string refreshToken);
        Task<T> WaitAndRetry<T>(Func<Task<T>> func, Func<int, TimeSpan> sleepDurationProvider);
        Task<T> WaitAndRetry<T>(Func<Task<T>> func, Func<int, TimeSpan> sleepDurationProvider, int retryCount);
        Task<T> WaitAndRetry<T>(Func<Task<T>> func, Func<int, TimeSpan> sleepDurationProvider, int retryCount, Func<Exception, TimeSpan, Task> onRetryAsync);
    }
}
