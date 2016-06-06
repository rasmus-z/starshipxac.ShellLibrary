using System;
using starshipxac.Windows.Dialogs.Interop;

namespace starshipxac.Windows.Dialogs
{
    [Flags]
    public enum TaskDialogOptions
    {
        None = TASKDIALOG_FLAGS.None,

        VerificationChecked = TASKDIALOG_FLAGS.TDF_VERIFICATION_FLAG_CHECKED,
        HyperlinksEnabled = TASKDIALOG_FLAGS.TDF_ENABLE_HYPERLINKS,
        Cancelable = TASKDIALOG_FLAGS.TDF_ALLOW_DIALOG_CANCELLATION,
        Expanded = TASKDIALOG_FLAGS.TDF_EXPANDED_BY_DEFAULT,
        ExpandFooterArea = TASKDIALOG_FLAGS.TDF_EXPAND_FOOTER_AREA,
        EnableTimer = TASKDIALOG_FLAGS.TDF_CALLBACK_TIMER,
    }
}