using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Dialogs.Interop
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
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
            var result = new TASKDIALOGCONFIG
            {
                cbSize = (uint)Marshal.SizeOf(typeof(TASKDIALOGCONFIG)),
                hwndParent = IntPtr.Zero,
                hInstance = IntPtr.Zero,
                flags = TASKDIALOG_FLAGS.TDF_ALLOW_DIALOG_CANCELLATION,
                commonButtons = TASKDIALOG_COMMON_BUTTON_FLAGS.TDCBF_OK_BUTTON,
                mainIcon = new TASKDIALOGCONFIG_ICON(0),
                buttonCount = 0,
                buttons = IntPtr.Zero,
                defaultButtonIndex = 0,
                radioButtonCount = 0,
                radioButtons = IntPtr.Zero,
                defaultRadioButtonIndex = 0,
                verificationText = null,
                expandedInformation = null,
                expandedControlText = null,
                collapsedControlText = null,
                footerIcon = new TASKDIALOGCONFIG_ICON(0),
                footerText = null,
                cxWidth = 0
            };

            return result;
        }
    }
}