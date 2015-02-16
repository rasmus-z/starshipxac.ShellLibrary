using System;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb773217(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct CMINVOKECOMMANDINFOEX
    {
        public UInt32 cbSize;
        public UInt32 fMask;
        public IntPtr hwnd;
        public string lpVerb;
        public string lpParameters;
        public string lpDirectory;
        public Int32 nShow;
        public UInt32 dwHotKey;
        public IntPtr hIcon;
        public string lpTitle;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpVerbW;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpParametersW;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpDirectoryW;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpTitleW;

        public POINT ptInvoke;

        public static CMINVOKECOMMANDINFOEX Create()
        {
            var result = new CMINVOKECOMMANDINFOEX();
            result.cbSize = (uint)Marshal.SizeOf(result);
            return result;
        }
    }
}