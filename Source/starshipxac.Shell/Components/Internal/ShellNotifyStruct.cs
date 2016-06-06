using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Components.Internal
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ShellNotifyStruct
    {
        public IntPtr item1;
        public IntPtr item2;
    }
}