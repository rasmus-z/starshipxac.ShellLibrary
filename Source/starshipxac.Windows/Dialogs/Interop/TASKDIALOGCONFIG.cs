using System;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Dialogs.Interop
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
    internal class TASKDIALOGCONFIG
    {
        internal uint cbSize;
        internal IntPtr hwndParent;
        internal IntPtr hInstance;
        internal TASKDIALOG_FLAGS flags;
        internal TASKDIALOG_COMMON_BUTTON_FLAGS commonButtons;

        [MarshalAs(UnmanagedType.LPWStr)]
        internal string windowTitle;

        internal TASKDIALOGCONFIG_ICON mainIcon;

        [MarshalAs(UnmanagedType.LPWStr)]
        internal string mainInstruction;

        [MarshalAs(UnmanagedType.LPWStr)]
        internal string content;

        internal uint buttonCount;
        internal IntPtr buttons;
        internal int defaultButtonIndex;

        internal uint radioButtonCount;
        internal IntPtr radioButtons;
        internal int defaultRadioButtonIndex;

        [MarshalAs(UnmanagedType.LPWStr)]
        internal string verificationText;

        [MarshalAs(UnmanagedType.LPWStr)]
        internal string expandedInformation;

        [MarshalAs(UnmanagedType.LPWStr)]
        internal string expandedControlText;

        [MarshalAs(UnmanagedType.LPWStr)]
        internal string collapsedControlText;

        internal TASKDIALOGCONFIG_ICON footerIcon;

        [MarshalAs(UnmanagedType.LPWStr)]
        internal string footerText;

        internal TASKDIALOGCALLBACK callback;
        internal IntPtr callbackData;
        internal uint cxWidth;

        internal static TASKDIALOGCONFIG Create()
        {
            var result = new TASKDIALOGCONFIG();

            result.cbSize = (uint)Marshal.SizeOf(typeof(TASKDIALOGCONFIG));
            result.hwndParent = IntPtr.Zero;
            result.hInstance = IntPtr.Zero;
            result.flags = TASKDIALOG_FLAGS.TDF_ALLOW_DIALOG_CANCELLATION;
            result.commonButtons = TASKDIALOG_COMMON_BUTTON_FLAGS.TDCBF_OK_BUTTON;

            result.mainIcon = new TASKDIALOGCONFIG_ICON(0);

            result.buttonCount = 0;
            result.buttons = IntPtr.Zero;
            result.defaultButtonIndex = 0;

            result.radioButtonCount = 0;
            result.radioButtons = IntPtr.Zero;
            result.defaultRadioButtonIndex = 0;

            result.verificationText = null;
            result.expandedInformation = null;
            result.expandedControlText = null;
            result.collapsedControlText = null;

            result.footerIcon = new TASKDIALOGCONFIG_ICON(0);
            result.footerText = null;

            result.cxWidth = 0;

            return result;
        }
    }
}