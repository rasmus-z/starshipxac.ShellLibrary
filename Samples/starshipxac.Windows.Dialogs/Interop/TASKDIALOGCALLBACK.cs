using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Dialogs.Interop
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal delegate int TASKDIALOGCALLBACK(
        IntPtr hwnd,
        uint message,
        IntPtr wparam,
        IntPtr lparam,
        IntPtr referenceData);
}