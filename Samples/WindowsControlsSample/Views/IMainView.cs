using System;
using System.Windows.Threading;

namespace WindowsControlsSample.Views
{
    public interface IMainView
    {
        DispatcherOperation ShowProgressViewAsync();
    }
}