using System.Threading.Tasks;
using System;

namespace Thundire.Helpers
{
    public static class TaskExtensions
    {
        public static async void SafeFireAndForget(this Task task, Action<Exception>? onException = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex) when (onException is not null)
            {
                onException.Invoke(ex);
            }
        }
    }
}