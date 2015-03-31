using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Interop;
using starshipxac.Windows.Dialogs.Controls;
using starshipxac.Windows.Dialogs.Internal;
using starshipxac.Windows.Dialogs.Interop;
using starshipxac.Windows.Properties;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// �^�X�N�_�C�A���O�̊��N���X���`���܂��B
    /// </summary>
    public abstract class TaskDialogBase : IDisposable
    {
        private bool disposed = false;

        private string title = String.Empty;

        /// <summary>
        /// <see cref="TaskDialogBase"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        protected TaskDialogBase()
        {
            this.Cancelable = true;
            this.TaskDialogInternal = new TaskDialogInternal(this);
        }

        ~TaskDialogBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }

                if (this.DialogShowStates == DialogShowStates.Showing)
                {
                    CloseDialog(TaskDialogCommonButtons.Cancel);
                }

                this.TaskDialogInternal.Dispose();

                this.disposed = true;
            }
        }

        [ContractInvariantMethod]
        private void TaskDialogBaseInvariant()
        {
            Contract.Invariant(this.TaskDialogInternal != null);
        }

        /// <summary>
        /// �_�C�A���O�̕\����Ԃ��擾���܂��B
        /// </summary>
        protected DialogShowStates DialogShowStates
        {
            get
            {
                return this.TaskDialogInternal.DialogShowStates;
            }
        }

        /// <summary>
        /// �_�C�A���O���\�������ǂ����𔻒肷��l���擾���܂��B
        /// </summary>
        public bool DialogShowing
        {
            get
            {
                return this.TaskDialogInternal.DialogShowing;
            }
        }

        /// <summary>
        /// �_�C�A���O�̊J�n�\���ʒu���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public TaskDialogStartupLocation StartupLocation { get; private set; }

        /// <summary>
        /// �_�C�A���O���L�����Z���\���ǂ����𔻒肷��l���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public bool Cancelable { get; set; }

        private TaskDialogInternal TaskDialogInternal { get; set; }

        /// <summary>
        /// �e�E�B���h�E���w�肵�āA�_�C�A���O��\�����܂��B
        /// </summary>
        /// <param name="parentWindow">�e�E�B���h�E�B</param>
        /// <returns>�_�C�A���O�̎��s���ʁB</returns>
        protected TaskDialogResult ShowDialog(Window parentWindow)
        {
            var parentWindowHandle = IntPtr.Zero;

            if (parentWindow == null)
            {
                if (Application.Current != null && Application.Current.MainWindow != null)
                {
                    parentWindowHandle = (new WindowInteropHelper(Application.Current.MainWindow)).Handle;
                }
            }
            else
            {
                parentWindowHandle = (new WindowInteropHelper(parentWindow)).Handle;
            }

            return ShowDialog(parentWindowHandle);
        }

        /// <summary>
        /// �e�E�B���h�E�̃n���h�����w�肵�āA�_�C�A���O��\�����܂��B
        /// </summary>
        /// <param name="parentWindowHandle">�e�E�B���h�E�̃n���h���B</param>
        /// <returns>�_�C�A���O�̎��s���ʁB</returns>
        protected TaskDialogResult ShowDialog(IntPtr parentWindowHandle)
        {
            try
            {
                this.TaskDialogInternal.SetFlags(GetDialogOptions());

                return this.TaskDialogInternal.ShowDialog(parentWindowHandle);
            }
            catch (EntryPointNotFoundException e)
            {
                throw new NotSupportedException(DialogErrorMessages.TaskDialogNotSupportedMethod, e);
            }
        }

        /// <summary>
        /// �蓮�Ń_�C�A���O����܂��B
        /// </summary>
        protected void CloseDialog(TaskDialogCommonButtons commonButton)
        {
            this.TaskDialogInternal.CloseDialog(commonButton);
        }

        protected void CloseDialog(int buttonId)
        {
            this.TaskDialogInternal.CloseDialog(buttonId);
        }

        protected internal virtual TaskDialogOptions GetDialogOptions()
        {
            var result = TaskDialogOptions.None;

            if (this.StartupLocation == TaskDialogStartupLocation.CenterOwner)
            {
                result |= (TaskDialogOptions)TASKDIALOG_FLAGS.TDF_POSITION_RELATIVE_TO_WINDOW;
            }
            if (this.Cancelable)
            {
                result |= TaskDialogOptions.Cancelable;
            }

            return result;
        }

        protected internal virtual TaskDialogButtonBase FindButton(int buttonId)
        {
            return null;
        }

        protected internal virtual void RaiseOpenedEvent()
        {
        }

        protected internal virtual bool RaiseClosingEvent(int buttonId)
        {
            return false;
        }

        protected internal virtual void RaiseNavigatedEvent()
        {
        }

        protected internal virtual void RaiseButtonClickedEvent(int buttonId)
        {
        }

        protected internal virtual void RaiseRadioButtonClickedEvent(int radioButtonId)
        {
        }

        protected internal virtual void RaiseHyperlinkClickedEvent(string link)
        {
        }

        protected internal virtual void RaiseVerificationClickedEvent(bool verificationChecked)
        {
        }

        protected internal virtual void RaiseExpandoButtonClickedEvent(bool expanded)
        {
        }

        protected internal virtual void RaiseHelpEvent()
        {
        }

        protected internal virtual void RaiseTimerEvent(int ticks)
        {
        }

        #region Control Methods

        protected void SetTitle(string title)
        {
            this.TaskDialogInternal.SetTitle(title);
        }

        protected void SetMainInstructionText(string text)
        {
            this.TaskDialogInternal.SetMainInstructionText(text);
        }

        protected void SetContentText(string text)
        {
            this.TaskDialogInternal.SetContentText(text);
        }

        protected void SetFooterText(string text)
        {
            this.TaskDialogInternal.SetFooterText(text);
        }

        protected void SetExpandedText(string text)
        {
            this.TaskDialogInternal.SetExpandedText(text);
        }

        protected void SetExpandedControlText(string text)
        {
            this.TaskDialogInternal.SetExpandedControlText(text);
        }

        protected void SetCollapsedControlText(string text)
        {
            this.TaskDialogInternal.SetCollapsedControlText(text);
        }

        protected void SetVerificationChecked(bool verificationChecked)
        {
            this.TaskDialogInternal.SetVerificationChecked(verificationChecked);
        }

        protected void SetVerificationText(string text)
        {
            this.TaskDialogInternal.SetVerificationText(text);
        }

        protected void SetMainIcon(TaskDialogIcon mainIcon)
        {
            this.TaskDialogInternal.SetMainIcon(mainIcon);
        }

        protected void SetFooterIcon(TaskDialogIcon footerIcon)
        {
            this.TaskDialogInternal.SetFooterIcon(footerIcon);
        }

        protected void SetCommonButtons(TaskDialogCommonButtons commonButtons)
        {
            this.TaskDialogInternal.SetCommonButtons(commonButtons);
        }

        protected void SetCustomButtons(TaskDialogControlCollection<TaskDialogButton> customButtons)
        {
            this.TaskDialogInternal.SetCustomButtons(customButtons);
        }

        protected void SetCommandLinks(TaskDialogControlCollection<TaskDialogCommandLink> commandLinks)
        {
            this.TaskDialogInternal.SetCommandLinks(commandLinks);
        }

        protected void SetRadioButtons(TaskDialogControlCollection<TaskDialogRadioButton> radioButtons)
        {
            this.TaskDialogInternal.SetRadioButtons(radioButtons);
        }

        protected void SetProgressBar(TaskDialogProgressBar progressBar)
        {
            this.TaskDialogInternal.SetProgressBar(progressBar);
        }

        internal void SetProgressBarState(TaskDialogProgressBar progressBar, TaskDialogProgressBarState state)
        {
            this.TaskDialogInternal.SetProgressBarState(progressBar, state);
        }

        internal void SetProgressBarPosition(TaskDialogProgressBar progressBar, int position)
        {
            this.TaskDialogInternal.SetProgressBarPosition(progressBar, position);
        }

        internal void SetProgressBarRange(TaskDialogProgressBar progressBar, int minimum, int maximum)
        {
            this.TaskDialogInternal.SetProgressBarRange(progressBar, minimum, maximum);
        }

        internal void SetButtonElevationRequiredState(TaskDialogButtonBase button, bool evelationRequired)
        {
            this.TaskDialogInternal.SetButtonElevationRequiredState(button, evelationRequired);
        }

        internal void SetButtonEnabled(TaskDialogButtonBase button, bool enabled)
        {
            this.TaskDialogInternal.SetButtonEnabled(button, enabled);
        }

        internal void SetRadioButtonEnabled(TaskDialogRadioButton button, bool enabled)
        {
            this.TaskDialogInternal.SetRadioButtonEnabled(button, enabled);
        }

        #endregion

        #region IDialogControlHost Members

        //bool IDialogControlHost.IsCollectionChangeAllowed()
        //{
        //    return !this.DialogShowing;
        //}

        //void IDialogControlHost.ApplyCollectionChanged()
        //{
        //}

        //bool IDialogControlHost.IsControlPropertyChangeAllowed(string propertyName, DialogControl control)
        //{
        //    bool result;
        //    if (!this.DialogShowing)
        //    {
        //        switch (propertyName)
        //        {
        //            case "Enabled":
        //                result = false;
        //                break;

        //            default:
        //                result = true;
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        switch (propertyName)
        //        {
        //            case "Text":
        //            case "Default":
        //                result = false;
        //                break;

        //            case "ShowElevationIcon":
        //            case "Enabled":
        //                result = true;
        //                break;

        //            default:
        //                result = false;
        //                break;
        //        }
        //    }
        //    return result;
        //}

        //void IDialogControlHost.ApplyControlPropertyChange(string propertyName, DialogControl control)
        //{
        //    this.TaskDialogInternal.ApplyControlPropertyChange(propertyName, control);
        //}

        #endregion
    }
}