using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Components.Interop;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Components.Internal
{
    /// <summary>
    ///     Define shell change notification.
    /// </summary>
    internal class ShellChangeNotify
    {
        /// <summary>
        ///     Initialize a instance of the <see cref="ShellChangeNotify"/> class.
        /// </summary>
        /// <param name="wParam"><c>WPARAM</c>.</param>
        /// <param name="lParam"><c>LPARAM</c>.</param>
        internal ShellChangeNotify(IntPtr wParam, IntPtr lParam)
        {
            var hwnd = wParam;
            var processId = (UInt32)lParam.ToInt64();
            IntPtr pidl;
            uint lEvent;
            var lockId = ShellWatcherNativeMethods.SHChangeNotification_Lock(hwnd, processId, out pidl, out lEvent);
            try
            {
                this.ChangeType = (ShellChangeTypes)lEvent;
                var notifyStruct = (ShellNotifyStruct)Marshal.PtrToStructure(pidl, typeof(ShellNotifyStruct));

                var guid = new Guid(ShellIID.IShellItem2);

                // dwItem1
                if (notifyStruct.item1 != IntPtr.Zero &&
                    (this.ChangeType & ShellChangeTypes.SystemImageUpdate) == ShellChangeTypes.None)
                {
                    var shellObject = CreateShellObject(notifyStruct.item1, ref guid);
                    if (shellObject != null)
                    {
                        this.ShellObject = shellObject;
                    }
                }
                else
                {
                    ImageIndex = notifyStruct.item1.ToInt32();
                }

                // dwItem2
                if (notifyStruct.item2 != IntPtr.Zero)
                {
                    var shellObject = CreateShellObject(notifyStruct.item2, ref guid);
                    if (shellObject != null)
                    {
                        this.ShellObject2 = shellObject;
                    }
                }
            }
            finally
            {
                if (lockId != IntPtr.Zero)
                {
                    ShellWatcherNativeMethods.SHChangeNotification_Unlock(lockId);
                }
            }
        }

        /// <summary>
        ///     Get the shell change type.
        /// </summary>
        public ShellChangeTypes ChangeType { get; }

        /// <summary>
        ///     Get a value that determines whether the event that occurred is a system event.
        /// </summary>
        public bool FromSystemInterrupt => (this.ChangeType & ShellChangeTypes.FromInterrupt) != ShellChangeTypes.None;

        /// <summary>
        ///     Get the <see cref="ShellObject" />.
        /// </summary>
        public ShellObject ShellObject { get; }

        /// <summary>
        ///     Get the second <see cref="ShellObject" />.
        /// </summary>
        public ShellObject ShellObject2 { get; }

        /// <summary>
        ///     Get the image index.
        /// </summary>
        public int ImageIndex { get; private set; }

        /// <summary>
        ///     Create a new instance of the <see cref="ShellObject" /> class.
        /// </summary>
        /// <param name="pidl"><c>PIDL</c>。</param>
        /// <param name="riid"><c>GUID</c>。</param>
        /// <returns></returns>
        private static ShellObject CreateShellObject(IntPtr pidl, ref Guid riid)
        {
            IShellItem2 shellItem2;
            var code = ShellNativeMethods.SHCreateItemFromIDList(pidl, ref riid, out shellItem2);
            if (HRESULT.Failed(code))
            {
                return null;
            }

            return ShellFactory.FromShellItem(new ShellItem(shellItem2));
        }
    }
}