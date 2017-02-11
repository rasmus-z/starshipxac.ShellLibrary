using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using starshipxac.Shell.Internal;
using starshipxac.Shell.Properties;

namespace starshipxac.Shell.Components.Internal
{
    /// <summary>
    ///     Define notification window of the <see cref="ShellWatcher" />.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class ShellWatcherManager
    {
        private static readonly ConcurrentDictionary<uint, ShellWatcher> Listeners = new ConcurrentDictionary<uint, ShellWatcher>();

        private const UInt32 WM_APP = 0x8000;
        private const UInt32 MaxAppMessage = 0xBFFF;

        private static readonly IntPtr HWND_MESSAGE = (IntPtr)(-3);

        static ShellWatcherManager()
        {
            Window = CreateWindow();
        }

        public static WindowSource Window { get; set; }

        /// <summary>
        ///     Register <see cref="ShellWatcher" /> in the message listener.
        /// </summary>
        /// <param name="shellObject">Register <see cref="ShellObject" />.</param>
        /// <param name="recursive">Specify whether to monitor recursively.</param>
        /// <returns>Created <see cref="ShellWatcher" />.</returns>
        public static async Task<ShellWatcher> RegisterAsync(ShellObject shellObject, bool recursive)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);

            var message = await FindWindowMessageAsync();
            if (message < WM_APP)
            {
                throw new ShellException(ErrorMessages.ShellWatcherManagerFilterUnableToRegister);
            }

            // Create ShellWatcher.
            var result = new ShellWatcher(shellObject, Window, message, recursive);

            // Register ShellWatcher.
            Listeners.TryAdd(message, result);

            return result;
        }

        /// <summary>
        ///     Unregister <see cref="ShellWatcher" /> from the message listener.
        /// </summary>
        /// <param name="shellWatcher">Unregister <see cref="ShellWatcher" />.</param>
        /// <returns></returns>
        public static Task UnregisterAsync(ShellWatcher shellWatcher)
        {
            Contract.Requires<ArgumentNullException>(shellWatcher != null);

            ShellWatcher temp;
            Listeners.TryRemove(shellWatcher.Message, out temp);

            return Task.FromResult(0);
        }

        /// <summary>
        ///     Create a message notification window.
        /// </summary>
        /// <returns>Created <see cref="WindowSource" />.</returns>
        private static WindowSource CreateWindow()
        {
            Contract.Ensures(Contract.Result<WindowSource>() != null);

            var param = new WindowSourceParameters("ShellWatcherNotify")
            {
                ParentWindow = HWND_MESSAGE,
                WindowSourceHook = NotifyWindowProc
            };
            return new WindowSource(param);
        }

        /// <summary>
        ///     Asynchronously search for available window messages.
        /// </summary>
        /// <returns></returns>
        private static Task<uint> FindWindowMessageAsync()
        {
            return Task.Run(() =>
            {
                for (var msg = WM_APP; msg < MaxAppMessage; ++msg)
                {
                    if (!Listeners.ContainsKey(msg))
                    {
                        // Unused messages.
                        return msg;
                    }
                }
                // No unused message.
                return 0U;
            });
        }

        /// <summary>
        ///     Window procedure for message notification window.
        /// </summary>
        /// <param name="hwnd">Window handle.</param>
        /// <param name="msg">Window message.</param>
        /// <param name="wParam"><c>WPARAM</c>.</param>
        /// <param name="lParam"><c>LPARAM</c>.</param>
        /// <param name="handled"></param>
        /// <returns></returns>
        private static IntPtr NotifyWindowProc(IntPtr hwnd, UInt32 msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            ShellWatcher shellWatcher;
            Listeners.TryGetValue(msg, out shellWatcher);
            if (shellWatcher != null)
            {
                var args = CreateEventArgs(wParam, lParam);
                shellWatcher.ChangeNotificationEvent(args);
                handled = true;
            }

            return IntPtr.Zero;
        }

        /// <summary>
        ///     Create a instance of the <see cref="ShellNotificationEventArgs" /> class.
        /// </summary>
        /// <param name="wParam"><c>WPARAM</c>.</param>
        /// <param name="lParam"><c>LPARAM</c>.</param>
        /// <returns>作成した<see cref="ShellNotificationEventArgs" />。</returns>
        private static ShellNotificationEventArgs CreateEventArgs(IntPtr wParam, IntPtr lParam)
        {
            ShellNotificationEventArgs result;

            var changeNotify = new ShellChangeNotify(wParam, lParam);
            switch (changeNotify.ChangeType)
            {
                case ShellChangeTypes.DirectoryRename:
                case ShellChangeTypes.ItemRename:
                    result = new ShellRenamedEventArgs(changeNotify);
                    break;

                case ShellChangeTypes.SystemImageUpdate:
                    result = new SystemImageUpdatedEventArgs(changeNotify);
                    break;

                case ShellChangeTypes.ExtendedEvent:
                case ShellChangeTypes.FreeSpace:
                    result = new ShellNotificationEventArgs(changeNotify);
                    break;

                default:
                    result = new ShellChangedEventArgs(changeNotify);
                    break;
            }

            return result;
        }
    }
}