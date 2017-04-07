using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrapperLib.Autotask.Net.Webservices;

namespace WrapperLib.Models
{
    public static class SoapCallingExtensions
    {
        public static Task<ATWSResponse> QueryAsyncTask(this ATWS client, string sXML)
        {
            var tcs = CreateSource<ATWSResponse>(null);
            client.queryCompleted += (sender, e) => TransferCompletion<ATWSResponse>(tcs, e, () => e.Result, null);
            client.queryAsync(sXML);
            return tcs.Task;
        }

        private static TaskCompletionSource<T> CreateSource<T>(object state)
        {
            return new TaskCompletionSource<T>(
                state, TaskCreationOptions.None);
        }

        private static void TransferCompletion<T>(
            TaskCompletionSource<T> tcs, AsyncCompletedEventArgs e,
            Func<T> getResult, Action unregisterHandler)
        {
            if (e.UserState == tcs)
            {
                if (e.Cancelled) tcs.TrySetCanceled();
                else if (e.Error != null) tcs.TrySetException(e.Error);
                else tcs.TrySetResult(getResult());
                if (unregisterHandler != null) unregisterHandler();
            }
        }
    }
}
