using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Devices.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/dd145064(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum MonitorFlags : uint
    {
        MONITOR_DEFAULTTONULL = 0x00000000,
        MONITOR_DEFAULTTOPRIMARY = 0x00000001,
        MONITOR_DEFAULTTONEAREST = 0x00000002
    }
}