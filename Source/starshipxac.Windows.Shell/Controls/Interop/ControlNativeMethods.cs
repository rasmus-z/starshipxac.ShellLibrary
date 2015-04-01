using System;

namespace starshipxac.Windows.Shell.Controls.Interop
{
    internal static class ControlNativeMethods
    {
        internal static UInt32 INDEXTOOVERLAYMASK(UInt32 iOverlay)
        {
            return iOverlay << 8;
        }
    }
}