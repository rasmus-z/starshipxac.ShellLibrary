using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Shell.Controls.Interop
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class ControlNativeMethods
    {
        internal static UInt32 INDEXTOOVERLAYMASK(UInt32 iOverlay)
        {
            return iOverlay << 8;
        }
    }
}