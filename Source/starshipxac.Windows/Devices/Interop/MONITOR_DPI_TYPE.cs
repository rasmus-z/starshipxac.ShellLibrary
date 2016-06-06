using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Devices.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     https://msdn.microsoft.com/ja-JP/library/windows/desktop/dn280511.aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum MONITOR_DPI_TYPE
    {
        MDT_EFFECTIVE_DPI = 0,
        MDT_ANGULAR_DPI = 1,
        MDT_RAW_DPI = 2,
        MDT_DEFAULT = MDT_EFFECTIVE_DPI,
    }
}