using System;
using System.Threading;
using System.Threading.Tasks;

namespace starshipxac.Shell.TestTools
{
    public static class STATask
    {
        public static Task Run(Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            var thread = new Thread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(new object());
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }
    }
}