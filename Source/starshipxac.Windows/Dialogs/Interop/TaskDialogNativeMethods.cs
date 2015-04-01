using System;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Dialogs.Interop
{
    internal static class TaskDialogNativeMethods
    {
        [DllImport("Comctl32.dll", CharSet = CharSet.Unicode, SetLastError = true, PreserveSig = true)]
        internal static extern void TaskDialogIndirect(
            [In] TASKDIALOGCONFIG pTaskConfig,
            [Out] out int pnButton,
            [Out] out int pnRadioButton,
            [MarshalAs(UnmanagedType.Bool)] [Out] out bool pfVerificationFlagChecked);

        internal const int TaskDialogIdealWidth = 0;
        internal const int TaskDialogButtonShieldIcon = 1;

        internal const int NoDefaultButtonSpecified = 0;
    }
}