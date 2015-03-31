using System;

namespace starshipxac.Windows.Dialogs.Interop
{
    /// <summary>
    /// <see cref="TaskDialog"/>フラグを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb787473(v=vs.85).aspx
    /// </remarks>
    [Flags]
    internal enum TASKDIALOG_FLAGS
    {
        None = 0,

        TDF_ENABLE_HYPERLINKS = 0x0001,
        TDF_USE_HICON_MAIN = 0x0002,
        TDF_USE_HICON_FOOTER = 0x0004,
        TDF_ALLOW_DIALOG_CANCELLATION = 0x0008,
        TDF_USE_COMMAND_LINKS = 0x0010,
        TDF_USE_COMMAND_LINKS_NO_ICON = 0x0020,
        TDF_EXPAND_FOOTER_AREA = 0x0040,
        TDF_EXPANDED_BY_DEFAULT = 0x0080,
        TDF_VERIFICATION_FLAG_CHECKED = 0x0100,
        TDF_SHOW_PROGRESS_BAR = 0x0200,
        TDF_SHOW_MARQUEE_PROGRESS_BAR = 0x0400,
        TDF_CALLBACK_TIMER = 0x0800,
        TDF_POSITION_RELATIVE_TO_WINDOW = 0x1000,
        TDF_RTL_LAYOUT = 0x2000,
        TDF_NO_DEFAULT_RADIO_BUTTON = 0x4000,
        TDF_CAN_BE_MINIMIZED = 0x8000,

        TDF_NO_SET_FOREGROUND = 0x00010000,
        TDF_SIZE_TO_CONTENT = 0x01000000
    }
}