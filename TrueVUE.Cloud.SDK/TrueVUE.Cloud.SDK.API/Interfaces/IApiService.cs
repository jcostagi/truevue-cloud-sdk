using System;
using System.Threading.Tasks;

namespace TrueVUE.Cloud.SDK.API.Interfaces
{
    public interface IApiService
    {
        Task<T> CallAndRetry<T>(Func<Task<T>> action, int retryCount, Func<Exception, int, Task> onRetry = null) where T : class;
    }
}
