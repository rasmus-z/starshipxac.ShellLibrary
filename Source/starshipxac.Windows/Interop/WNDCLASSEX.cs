using System;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Interop
{
    /// <summary>
    /// ウィンドウクラス構造体を定義します。
    /// </summary>
    /// <remarks>
    /// <c>http://msdn.microsoft.com/en-us/library/windows/desktop/ms633577(v=vs.85).aspx</c>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct WNDCLASSEX
    {
        public static WNDCLASSEX Create()
        {
            var result = new WNDCLASSEX
            {
                cbSize = (UInt32)Marshal.SizeOf(typeof(WNDCLASSEX))
            };
            return result;
        }

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 cbSize;

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 style;

        public WindowsNativeMethods.WndProc lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        public string lpszMenuName;
        public string lpszClassName;
        public IntPtr hIconSm;
    }
}