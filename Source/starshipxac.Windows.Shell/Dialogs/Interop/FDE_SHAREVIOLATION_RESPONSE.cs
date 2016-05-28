using System;
// ReSharper disable InconsistentNaming

namespace starshipxac.Windows.Shell.Dialogs.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762504(v=vs.85).aspx
    /// </remarks>
    internal enum FDE_SHAREVIOLATION_RESPONSE
    {
        FDESVR_DEFAULT = 0x00000000,
        FDESVR_ACCEPT = 0x00000001,
        FDESVR_REFUSE = 0x00000002
    }
}