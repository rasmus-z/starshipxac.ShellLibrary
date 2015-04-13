using System;
using System.Threading;
using System.Threading.Tasks;

namespace starshipxac.Shell.TestTools
{
    /// <summary>
    /// 指定した<see cref="Action"/>をシングルスレッドアパートメントで実行します。
    /// </summary>
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