using System;

namespace starshipxac.Windows.Dialogs.Interop
{
    internal delegate int TASKDIALOGCALLBACK(
        IntPtr hwnd,
        uint message,
        IntPtr wparam,
        IntPtr lparam,
        IntPtr referenceData);
}