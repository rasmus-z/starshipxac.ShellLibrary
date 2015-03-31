using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using starshipxac.Windows.Dialogs.Controls;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// <see cref="TaskDialog.Closing"/>�C�x���g�f�[�^���`���܂��B
    /// </summary>
    public class TaskDialogClosingEventArgs : CancelEventArgs
    {
        private TaskDialogClosingEventArgs(TaskDialogSelectedButton dialogResult, int controlId, string controlName)
        {
            this.TaskDialogResult = dialogResult;
            this.ControlId = controlId;
            this.ControlName = controlName;
        }

        public static TaskDialogClosingEventArgs Create(TaskDialogCommonButtonId commonButtonId)
        {
            var dialogResult = CommonButtonIdToDialogResult(commonButtonId);
            return new TaskDialogClosingEventArgs(dialogResult, (int)commonButtonId, String.Empty);
        }

        public static TaskDialogClosingEventArgs Create(TaskDialogButtonBase control)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            return new TaskDialogClosingEventArgs(TaskDialogSelectedButton.CustomButtonClicked, control.Id, control.Name);
        }

        /// <summary>
        /// �^�X�N�_�C�A���O���s���ʂ��擾���܂��B
        /// </summary>
        public TaskDialogSelectedButton TaskDialogResult { get; internal set; }

        /// <summary>
        /// �N���b�N�����R���g���[���� ID���擾���܂��B
        /// </summary>
        public int ControlId { get; internal set; }

        /// <summary>
        /// �N���b�N�����R���g���[���̖��O���擾���܂��B
        /// </summary>
        public string ControlName { get; internal set; }

        private static TaskDialogSelectedButton CommonButtonIdToDialogResult(TaskDialogCommonButtonId commonButtonId)
        {
            switch (commonButtonId)
            {
                case TaskDialogCommonButtonId.Ok:
                    return TaskDialogSelectedButton.Ok;

                case TaskDialogCommonButtonId.Cancel:
                    return TaskDialogSelectedButton.Cancel;

                case TaskDialogCommonButtonId.Retry:
                    return TaskDialogSelectedButton.Retry;

                case TaskDialogCommonButtonId.Yes:
                    return TaskDialogSelectedButton.Yes;

                case TaskDialogCommonButtonId.No:
                    return TaskDialogSelectedButton.No;

                case TaskDialogCommonButtonId.Close:
                    return TaskDialogSelectedButton.Close;

                default:
                    return TaskDialogSelectedButton.None;
            }
        }
    }
}