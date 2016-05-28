using System;
// ReSharper disable InconsistentNaming

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb776135(v=vs.85).aspx
    /// </remarks>
    internal static class CommDlgBrowser2ViewFlags
    {
        public const UInt32 CDB2GVF_SHOWALLFILES = 0x00000001;

        public const UInt32 CDB2GVF_ISFILESAVE = 0x00000002;

        public const UInt32 CDB2GVF_ALLOWPREVIEWPANE = 0x00000004;

        public const UInt32 CDB2GVF_NOSELECTVERB = 0x00000008;

        public const UInt32 CDB2GVF_NOINCLUDEITEM = 0x00000010;

        public const UInt32 CDB2GVF_ISFOLDERPICKER = 0x00000020;

        public const UInt32 CDB2GVF_ADDSHIELD = 0x00000040;
    }
}