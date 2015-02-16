using System;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Shell.Dialogs.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb773221(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct COMDLG_FILTERSPEC
    {
        internal COMDLG_FILTERSPEC(string name, string spec)
        {
            this.pszName = name;
            this.pszSpec = spec;
        }

        [MarshalAs(UnmanagedType.LPWStr)]
        internal string pszName;

        [MarshalAs(UnmanagedType.LPWStr)]
        internal string pszSpec;
    }
}