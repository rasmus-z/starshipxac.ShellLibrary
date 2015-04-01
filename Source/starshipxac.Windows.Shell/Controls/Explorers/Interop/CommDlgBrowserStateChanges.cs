using System;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb776144(v=vs.85).aspx
    /// </remarks>
    internal static class CommDlgBrowserStateChanges
    {
        public const UInt32 CDBOSC_SETFOCUS = 0;

        public const UInt32 CDBOSC_KILLFOCUS = 1;

        public const UInt32 CDBOSC_SELCHANGE = 2;

        public const UInt32 CDBOSC_RENAME = 3;

        public const UInt32 CDBOSC_STATECHANGE = 4;
    }
}