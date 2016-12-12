using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Components.Interop
{
    /// <summary>
    ///     Contains and receives information for change notifications.
    ///     This structure is used with the <see cref="ShellWatcherNativeMethods.SHChangeNotifyRegister(IntPtr, int, uint, uint, int, ref SHChangeNotifyEntry)"/> function
    ///     and the <c>SFVM_QUERYFSNOTIFY</c> notification.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb773405(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct SHChangeNotifyEntry
    {
        public IntPtr pidl;

        [MarshalAs(UnmanagedType.Bool)] public bool fRecursive;
    }
}