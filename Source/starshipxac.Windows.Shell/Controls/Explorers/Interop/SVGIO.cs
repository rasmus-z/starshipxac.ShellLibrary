using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum SVGIO
    {
        SVGIO_BACKGROUND = 0x00000000,
        SVGIO_SELECTION = 0x00000001,
        SVGIO_ALLVIEW = 0x00000002,
        SVGIO_CHECKED = 0x00000003,
        SVGIO_TYPE_MASK = 0x0000000F,
        SVGIO_FLAG_VIEWORDER = unchecked((int)0x80000000)
    }
}