using System;
using starshipxac.Windows.Dialogs.Interop;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// �^�X�N�_�C�A���O�W���{�^�����`���܂��B
    /// </summary>
    [Flags]
    public enum TaskDialogCommonButtons
    {
        /// <summary>
        /// ����`�̃{�^���B
        /// </summary>
        None = 0,

        /// <summary>
        /// OK�{�^���B
        /// </summary>
        Ok = TASKDIALOG_COMMON_BUTTON_FLAGS.TDCBF_OK_BUTTON,

        /// <summary>
        /// �u�͂��v�{�^���B
        /// </summary>
        Yes = TASKDIALOG_COMMON_BUTTON_FLAGS.TDCBF_YES_BUTTON,

        /// <summary>
        /// �u�������v�{�^���B
        /// </summary>
        No = TASKDIALOG_COMMON_BUTTON_FLAGS.TDCBF_NO_BUTTON,

        /// <summary>
        /// �L�����Z���{�^���B
        /// </summary>
        Cancel = TASKDIALOG_COMMON_BUTTON_FLAGS.TDCBF_CANCEL_BUTTON,

        /// <summary>
        /// �u�Ď��s�v�{�^���B
        /// </summary>
        Retry = TASKDIALOG_COMMON_BUTTON_FLAGS.TDCBF_RETRY_BUTTON,

        /// <summary>
        /// ����{�^���B
        /// </summary>
        Close = TASKDIALOG_COMMON_BUTTON_FLAGS.TDCBF_CLOSE_BUTTON,
    }
}