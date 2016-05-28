using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

// ReSharper disable InconsistentNaming

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb759784(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct SHELLEXECUTEINFO
    {
        public int cbSize;
        public uint fMask;
        public IntPtr hwnd;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpVerb;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpFile;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpParameters;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpDirectory;

        public int nShow;
        public IntPtr hInstApp;
        public int lpIDList;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpClass;

        public IntPtr hkeyClass;
        public uint dwHotKey;
        public IntPtr hIcon;
        public IntPtr hProcess;

        public static SHELLEXECUTEINFO Create()
        {
            var shei = new SHELLEXECUTEINFO();
            shei.cbSize = Marshal.SizeOf(shei);
            return shei;
        }

        public static SHELLEXECUTEINFO Create(Window parentWindow)
        {
            var shei = new SHELLEXECUTEINFO();
            shei.cbSize = Marshal.SizeOf(shei);

            if (parentWindow != null)
            {
                var windowHelper = new WindowInteropHelper(parentWindow);
                shei.hwnd = windowHelper.Handle;
            }

            return shei;
        }

        internal static SHELLEXECUTEINFO Create(IntPtr parentWindow)
        {
            var shei = new SHELLEXECUTEINFO();
            shei.cbSize = Marshal.SizeOf(shei);
            shei.hwnd = parentWindow;
            return shei;
        }
    }
}