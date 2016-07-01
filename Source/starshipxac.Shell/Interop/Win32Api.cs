using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    internal static class Win32Api
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyIcon(IntPtr hIcon);
    }
}
