using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    [Flags]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum EXPLORERPANESTATE : uint
    {
        EPS_DONTCARE = 0x00000000,
        EPS_DEFAULT_ON = 0x00000001,
        EPS_DEFAULT_OFF = 0x00000002,
        EPS_STATEMASK = 0x0000ffff,
        EPS_INITIALSTATE = 0x00010000,
        EPS_FORCE = 0x00020000
    }
}