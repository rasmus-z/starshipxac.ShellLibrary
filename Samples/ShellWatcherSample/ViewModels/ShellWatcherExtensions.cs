using System;
using System.Reactive.Linq;
using starshipxac.Shell.Components;

namespace ShellWatcherSample.ViewModels
{
    public static class ShellWatcherExtensions
    {
        public static IObservable<ShellChangedEventArgs> CreatedAsObservable(this ShellWatcher watcher)
        {
            return Observable.FromEventPattern<ShellChangedEventArgs>(
                h => watcher.ItemCreated += h,
                h => watcher.ItemCreated -= h)
                .Select(x => x.EventArgs);
        }

        public static IObservable<ShellChangedEventArgs> DeletedAsObservable(this ShellWatcher watcher)
        {
            return Observable.FromEventPattern<ShellChangedEventArgs>(
                h => watcher.ItemDeleted += h,
                h => watcher.ItemDeleted -= h)
                .Select(x => x.EventArgs);
        }

        public static IObservable<ShellRenamedEventArgs> RenamtedAsObservable(this ShellWatcher watcher)
        {
            return Observable.FromEventPattern<ShellRenamedEventArgs>(
                h => watcher.ItemRenamed += h,
                h => watcher.ItemRenamed -= h)
                .Select(x => x.EventArgs);
        }
    }
}
