using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Dialogs.Interop
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum TASKDIALOG_ICONS
    {
        TD_WARNING_ICON = 65535,
        TD_ERROR_ICON = 65534,
        TD_INFORMATION_ICON = 65533,
        TD_SHIELD_ICON = 65532
    }
}