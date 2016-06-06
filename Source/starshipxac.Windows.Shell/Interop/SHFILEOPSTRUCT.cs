using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb759795(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct SHFILEOPSTRUCT
    {
        public IntPtr hwnd;
        public UInt32 wFunc;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string pFrom;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string pTo;

        public FILEOP_FLAGS fFlags;

        [MarshalAs(UnmanagedType.Bool)]
        public bool fAnyOperationsAborted;

        public IntPtr hNameMappings;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszProgressTitle;

        public static SHFILEOPSTRUCT Create(UInt32 function)
        {
            var result = new SHFILEOPSTRUCT
            {
                wFunc = function,
                fAnyOperationsAborted = false,
                hNameMappings = IntPtr.Zero
            };
            return result;
        }

        public static SHFILEOPSTRUCT Create(Window window, UInt32 function)
        {
            var result = new SHFILEOPSTRUCT();
            if (window != null)
            {
                var windowHelper = new WindowInteropHelper(window);
                result.hwnd = windowHelper.Handle;
            }
            result.wFunc = function;
            result.fAnyOperationsAborted = false;
            result.hNameMappings = IntPtr.Zero;
            return result;
        }

        public static SHFILEOPSTRUCT Create(IntPtr hwnd, UInt32 function)
        {
            var result = new SHFILEOPSTRUCT
            {
                hwnd = hwnd,
                wFunc = function,
                fAnyOperationsAborted = false,
                hNameMappings = IntPtr.Zero
            };
            return result;
        }
    }
}