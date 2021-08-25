/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;
using System.Threading.Tasks;

namespace TrueVUE.Cloud.SDK.Extensions
{
    public static class TaskExtensions
    {
        public static Task OnException<TException>(this Task task, Action<TException> action) where TException : Exception
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            if (task.Exception != null)
            {
                task.Exception.Handle(e => true);
            }

            task.ContinueWith(delegate
            {
                if (task.IsFaulted)
                {
                    TException concernedException = task.Exception.InnerException as TException;
                    if (concernedException == null)
                    {
                        tcs.TrySetException(task.Exception.InnerExceptions);
                    }
                    else
                    {
                        try
                        {
                            action?.Invoke(concernedException);
                            tcs.SetResult(null);
                        }
                        catch (Exception e)
                        {
                            tcs.TrySetException(e);
                        }
                    }
                }
            });

            return tcs.Task;
        }

        public static async void RunAndForget(this Task task, Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception e)
            {
                onException?.Invoke(e);
            }
        }
    }
}
