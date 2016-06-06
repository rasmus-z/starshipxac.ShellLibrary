using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb776138(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class CommDlgBrowserNotifyTypes
    {
        public const UInt32 CDB2N_CONTEXTMENU_DONE = 0x00000001;

        public const UInt32 CDB2N_CONTEXTMENU_START = 0x00000002;
    }
}