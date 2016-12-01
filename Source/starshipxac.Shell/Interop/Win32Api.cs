using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    internal static class Win32Api
    {
        /// <summary>
        ///     Destroys an icon and frees any memory the icon occupied. 
        /// </summary>
        /// <param name="hIcon"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/ms648063%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyIcon(IntPtr hIcon);

        /// <summary>
        ///     The DeleteObject function deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object.
        ///     After the object is deleted, the specified handle is no longer valid.
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/dd183539(v=vs.85).aspx
        /// </remarks>
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr handle);
    }
}
