using System;

namespace starshipxac.Windows.Shell.Dialogs.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762483(v=vs.85).aspx
    /// </remarks>
    [Flags]
    internal enum CDCONTROLSTATEF
    {
        CDCS_INACTIVE = 0,
        CDCS_ENABLED = 0x1,
        CDCS_VISIBLE = 0x2,
        CDCS_ENABLEDVISIBLE = 0x3
    }
}