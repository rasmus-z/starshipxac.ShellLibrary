using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;

namespace starshipxac.Shell.Resources.Interop
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class ResourceMethods
    {
        internal const UInt32 DONT_RESOLVE_DLL_REFERENCES = 0x00000001;
        internal const UInt32 LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010;
        internal const UInt32 LOAD_LIBRARY_AS_DATAFILE = 0x00000002;
        internal const UInt32 LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040;
        internal const UInt32 LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020;
        internal const UInt32 LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008;

        internal const UInt32 IMAGE_BITMAP = 0;
        internal const UInt32 IMAGE_ICON = 1;
        internal const UInt32 IMAGE_CURSOR = 2;

        internal const UInt32 LR_CREATEDIBSECTION = 0x00002000;
        internal const UInt32 LR_DEFAULTCOLOR = 0x00000000;
        internal const UInt32 LR_DEFAULTSIZE = 0x00000040;
        internal const UInt32 LR_LOADFROMFILE = 0x00000010;
        internal const UInt32 LR_LOADMAP3DCOLORS = 0x00001000;
        internal const UInt32 LR_LOADTRANSPARENT = 0x00000020;
        internal const UInt32 LR_MONOCHROME = 0x00000001;
        internal const UInt32 LR_SHARED = 0x00008000;
        internal const UInt32 LR_VGACOLOR = 0x00000080;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, UInt32 dwFlags);

        internal static IntPtr LoadLibraryEx(string lpFileName, UInt32 dwFlags)
        {
            return LoadLibraryEx(lpFileName, IntPtr.Zero, dwFlags);
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int LoadString(IntPtr instanceHandle, int id, StringBuilder buffer, int bufferSize);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr LoadImage(IntPtr hInstance, string lpszName, UInt32 uType,
            int cxDesired, int cyDesired, UInt32 fuLoad);

        internal static IntPtr LoadIconImage(IntPtr hInstance, string lpszName, int cxDesired, int cyDesired, uint fuLoad)
        {
            return LoadImage(hInstance, lpszName, IMAGE_ICON, cxDesired, cyDesired, fuLoad);
        }
    }
}