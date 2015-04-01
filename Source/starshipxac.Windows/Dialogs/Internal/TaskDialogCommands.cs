using System;
using System.Diagnostics.Contracts;
using starshipxac.Windows.Dialogs.Controls;
using starshipxac.Windows.Dialogs.Interop;
using starshipxac.Windows.Interop;

namespace starshipxac.Windows.Dialogs.Internal
{
    /// <summary>
    /// タスクダイアログコマンドメソッドを定義します。
    /// </summary>
    internal class TaskDialogCommands : IDisposable
    {
        private bool disposed = false;

        public TaskDialogCommands()
        {
            this.TextElements = new TaskDialogTextElements();
        }

        ~TaskDialogCommands()
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
                this.disposed = true;

                if (disposing)
                {
                }

                if (this.TextElements != null)
                {
                    this.TextElements.FreeAllText();
                    this.TextElements = null;
                }
            }
        }

        public IntPtr WindowHandle { get; internal set; }

        private TaskDialogTextElements TextElements { get; set; }

        public void NatigatePageCommand()
        {
            SendCommandMessage(TASKDIALOG_MESSAGES.TDM_NAVIGATE_PAGE, IntPtr.Zero, IntPtr.Zero);
        }

        public void ClickButtonCommand(int buttonId)
        {
            var wParam = (IntPtr)buttonId;
            SendCommandMessage(TASKDIALOG_MESSAGES.TDM_CLICK_BUTTON, wParam, IntPtr.Zero);
        }

        public void SetMarqueeProgressBarCommand(bool marquee)
        {
            var wParam = (IntPtr)((marquee) ? 1 : 0);
            SendCommandMessage(TASKDIALOG_MESSAGES.TDM_SET_MARQUEE_PROGRESS_BAR, wParam, IntPtr.Zero);
        }

        public void SetProgressBarStateCommand(TaskDialogProgressBarState state)
        {
            var wParam = (IntPtr)(int)state;
            SendCommandMessage(TASKDIALOG_MESSAGES.TDM_SET_PROGRESS_BAR_STATE, wParam, IntPtr.Zero);
        }

        public void SetProgressBarRangeCommand(int minRange, int maxRange)
        {
            var lParam = LPARAM.MakeLPARAM(minRange, maxRange);
            SendCommandMessage(TASKDIALOG_MESSAGES.TDM_SET_PROGRESS_BAR_RANGE, IntPtr.Zero, lParam);
        }

        public void SetProgressBarPosCommand(int pos)
        {
            var wParam = (IntPtr)pos;
            SendCommandMessage(TASKDIALOG_MESSAGES.TDM_SET_PROGRESS_BAR_POS, wParam, IntPtr.Zero);
        }

        public void SetProgressBarMarqueeCommand(bool start, int speed)
        {
            var wParam = (IntPtr)((start) ? 1 : 0);
            var lParam = (IntPtr)(speed);
            SendCommandMessage(TASKDIALOG_MESSAGES.TDM_SET_PROGRESS_BAR_MARQUEE, wParam, lParam);
        }

        private void SetElementTextCommand(TASKDIALOG_ELEMENTS element, string elementText)
        {
            this.TextElements.FreeText(element);

            var wParam = (IntPtr)(uint)element;
            var lParam = this.TextElements.CreateText(element, elementText);
            SendCommandMessage(TASKDIALOG_MESSAGES.TDM_SET_ELEMENT_TEXT, wParam, lParam);
        }

        public void SetContentText(string contentText)
        {
            SetElementTextCommand(TASKDIALOG_ELEMENTS.TDE_CONTENT, contentText);
        }

        public void SetInstructionText(string instructionText)
        {
            SetElementTextCommand(TASKDIALOG_ELEMENTS.TDE_MAIN_INSTRUCTION, instructionText);
        }

        public void SetFooterText(string footerText)
        {
            SetElementTextCommand(TASKDIALOG_ELEMENTS.TDE_FOOTER, footerText);
        }

        public void SetExpandedText(string expandedText)
        {
            SetElementTextCommand(TASKDIALOG_ELEMENTS.TDE_EXPANDED_INFORMATION, expandedText);
        }

        public void SetMainIcon(TaskDialogIcon mainIcon)
        {
            UpdateIconCommand(TASKDIALOG_ICON_ELEMENTS.TDIE_ICON_MAIN, (int)mainIcon);
        }

        public void SetFooterIcon(TaskDialogIcon footerIcon)
        {
            UpdateIconCommand(TASKDIALOG_ICON_ELEMENTS.TDIE_ICON_FOOTER, (int)footerIcon);
        }

        public void ClickRadioButtonCommand(int radioButtonId)
        {
            var wParam = (IntPtr)radioButtonId;
            SendCommandMessage(TASKDIALOG_MESSAGES.TDM_CLICK_RADIO_BUTTON, wParam, IntPtr.Zero);
        }

        public void EnableButtonCommand(int buttonId, bool enabled)
        {
            var wParam = (IntPtr)buttonId;
            var lParam = (IntPtr)(enabled ? 1 : 0);
            SendCommandMessage(TASKDIALOG_MESSAGES.TDM_ENABLE_BUTTON, wParam, lParam);
        }

        public void EnableRadioButtonCommand(int radioButtonId, bool enabled)
        {
            var wParam = (IntPtr)radioButtonId;
            var lParam = (IntPtr)(enabled ? 1 : 0);
            SendCommandMessage(TASKDIALOG_MESSAGES.TDM_ENABLE_RADIO_BUTTON, wParam, lParam);
        }

        public void ClickVerificationCommand(bool radioButtonChecked, bool focused)
        {
            var wParam = (IntPtr)(radioButtonChecked ? 1 : 0);
            var lParam = (IntPtr)(focused ? 1 : 0);
            SendCommandMessage(TASKDIALOG_MESSAGES.TDM_CLICK_VERIFICATION, wParam, lParam);
        }

        public void SetButtonElevationRequiredStateCommand(TaskDialogButtonBase control, bool required)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            var wParam = (IntPtr)control.Id;
            var lParam = (IntPtr)(required ? 1 : 0);
            SendCommandMessage(TASKDIALOG_MESSAGES.TDM_SET_BUTTON_ELEVATION_REQUIRED_STATE, wParam, lParam);
        }

        public void UpdateIconCommand(TASKDIALOG_ICON_ELEMENTS iconElement, int icon)
        {
            var wParam = (IntPtr)(uint)iconElement;
            var lParam = (IntPtr)icon;
            SendCommandMessage(TASKDIALOG_MESSAGES.TDM_UPDATE_ICON, wParam, lParam);
        }

        public void SendCommandMessage(TASKDIALOG_MESSAGES message, IntPtr wParam, IntPtr lParam)
        {
            if (this.WindowHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException("windowHandle == null");
            }

            WindowsNativeMethods.SendMessage(this.WindowHandle, (uint)message, wParam, lParam);
        }
    }
}