using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using starshipxac.Windows.Dialogs.Controls;
using starshipxac.Windows.Dialogs.Interop;
using starshipxac.Windows.Properties;

namespace starshipxac.Windows.Dialogs.Internal
{
    /// <summary>
    /// �^�X�N�_�C�A���O�̐ݒ���`���܂��B
    /// </summary>
    internal sealed class TaskDialogInternal : IDisposable
    {
        private bool firstRadioButtonClicked = false;

        private const int S_OK = 0;
        private const int S_FALSE = 1;
        private const int Ignored = S_OK;

        /// <summary>
        /// <see cref="TaskDialogInternal"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="dialog"><see cref="TaskDialogBase"/>�B</param>
        public TaskDialogInternal(TaskDialogBase dialog)
        {
            Contract.Requires<ArgumentNullException>(dialog != null);

            this.Dialog = dialog;
            this.DialogShowStates = DialogShowStates.PreShow;
            this.Commands = new TaskDialogCommands();

            this.Flags = TASKDIALOG_FLAGS.None;
        }

        ~TaskDialogInternal()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.CustomButtons != null)
                {
                    this.CustomButtons.Dispose();
                    this.CustomButtons = null;
                }

                if (this.CommandLinks != null)
                {
                    this.CommandLinks.Dispose();
                    this.CommandLinks = null;
                }

                if (this.RadioButtons != null)
                {
                    this.RadioButtons.Dispose();
                    this.RadioButtons = null;
                }

                if (this.Commands != null)
                {
                    this.Commands.Dispose();
                    this.Commands = null;
                }
            }
        }

        [ContractInvariantMethod]
        private void TaskDialogConfigurationInvariant()
        {
            Contract.Invariant(this.Dialog != null);
        }

        /// <summary>
        /// <see cref="TaskDialogBase"/>���擾���܂��B
        /// </summary>
        private TaskDialogBase Dialog { get; set; }

        /// <summary>
        /// �_�C�A���O�̕\����Ԃ��擾���܂��B
        /// </summary>
        public DialogShowStates DialogShowStates { get; internal set; }

        /// <summary>
        /// �_�C�A���O���\�������ǂ����𔻒肷��l���擾���܂��B
        /// </summary>
        public bool DialogShowing
        {
            get
            {
                return this.DialogShowStates == DialogShowStates.Showing ||
                       this.DialogShowStates == DialogShowStates.Closing;
            }
        }

        /// <summary>
        /// �^�X�N�_�C�A���O�t���O���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        private TASKDIALOG_FLAGS Flags { get; set; }

        /// <summary>
        /// �_�C�A���O�̃^�C�g�����擾�܂��͐ݒ肵�܂��B
        /// </summary>
        private string WindowTitle { get; set; }

        /// <summary>
        /// ���������擾�܂��͐ݒ肵�܂��B
        /// </summary>
        private string InstructionText { get; set; }

        /// <summary>
        /// �{�����擾�܂��͐ݒ肵�܂��B
        /// </summary>
        private string ContentText { get; set; }

        /// <summary>
        /// �t�b�^�[�e�L�X�g���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        private string FooterText { get; set; }

        private string ExpandedText { get; set; }

        private string ExpandedControlText { get; set; }

        private string CollapsedControlText { get; set; }

        private bool? VerificationChecked { get; set; }

        private string VerificationText { get; set; }

        /// <summary>
        /// ���C���A�C�R�����擾�܂��͐ݒ肵�܂��B
        /// </summary>
        private TaskDialogIcon MainIcon { get; set; }

        /// <summary>
        /// �t�b�^�[�A�C�R�����擾�܂��͐ݒ肵�܂��B
        /// </summary>
        private TaskDialogIcon FooterIcon { get; set; }

        /// <summary>
        /// �W���{�^���t���O���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        private TASKDIALOG_COMMON_BUTTON_FLAGS CommonButtons { get; set; }

        /// <summary>
        /// �J�X�^���{�^���̃R���N�V�������擾�܂��͐ݒ肵�܂��B
        /// </summary>
        private TaskDialogControlCollectionInternal<TaskDialogButton> CustomButtons { get; set; }

        /// <summary>
        /// �R�}���h�����N�̃R���N�V�������擾�܂��͐ݒ肵�܂��B
        /// </summary>
        private TaskDialogControlCollectionInternal<TaskDialogCommandLink> CommandLinks { get; set; }

        /// <summary>
        /// ���W�I�{�^���̃R���N�V�������擾�܂��͐ݒ肵�܂��B
        /// </summary>
        private TaskDialogControlCollectionInternal<TaskDialogRadioButton> RadioButtons { get; set; }

        /// <summary>
        /// �v���O���X�o�[���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        private TaskDialogProgressBar ProgressBar { get; set; }

        private TaskDialogCommands Commands { get; set; }

        /// <summary>
        /// �^�X�N�_�C�A���O��\�����܂��B
        /// </summary>
        /// <param name="parentWindowHandle">�e�E�B���h�E�̃n���h���B</param>
        public TaskDialogResult ShowDialog(IntPtr parentWindowHandle)
        {
            this.DialogShowStates = DialogShowStates.Showing;

            var config = CreateConfig(parentWindowHandle);

            int selectedButtonId;
            int selectedRadioButtonId;
            bool verificationChecked;
            TaskDialogNativeMethods.TaskDialogIndirect(
                config,
                out selectedButtonId,
                out selectedRadioButtonId,
                out verificationChecked);

            this.DialogShowStates = DialogShowStates.Closed;

            return TaskDialogResult.Create(selectedButtonId, selectedRadioButtonId, verificationChecked);
        }

        /// <summary>
        /// �蓮�Ń_�C�A���O����܂��B
        /// </summary>
        /// <param name="commonButtonId">�����ꂽ�Ɖ��肷��{�^����ID�B</param>
        public void CloseDialog(TaskDialogCommonButtons commonButtonId)
        {
            this.DialogShowStates = DialogShowStates.Closing;

            this.Commands.ClickButtonCommand((int)TaskDialogCommonButtons.Cancel);
            this.Dialog.RaiseClosingEvent((int)commonButtonId);
        }

        /// <summary>
        /// �蓮�Ń_�C�A���O����܂��B
        /// </summary>
        /// <param name="buttonId">�����ꂽ�Ɖ��肷��{�^����ID�B</param>
        public void CloseDialog(int buttonId)
        {
            this.DialogShowStates = DialogShowStates.Closing;

            this.Commands.ClickButtonCommand((int)TaskDialogCommonButtons.Cancel);
            this.Dialog.RaiseClosingEvent(buttonId);
        }

        #region Create Native Config

        /// <summary>
        /// <see cref="TASKDIALOGCONFIG"/>���쐬���܂��B
        /// </summary>
        /// <param name="parentWindowHandle">�e�E�B���h�E�̃n���h���B</param>
        /// <returns>�쐬����<see cref="TASKDIALOGCONFIG"/>�B</returns>
        private TASKDIALOGCONFIG CreateConfig(IntPtr parentWindowHandle)
        {
            var result = TASKDIALOGCONFIG.Create();
            result.hwndParent = parentWindowHandle;
            result.flags = this.Flags;
            result.commonButtons = this.CommonButtons;
            result.windowTitle = this.WindowTitle;
            result.mainIcon = new TASKDIALOGCONFIG_ICON((int)this.MainIcon);
            result.mainInstruction = this.InstructionText;
            result.content = this.ContentText;

            result.verificationText = this.VerificationText;
            result.expandedInformation = this.ExpandedText;
            result.expandedControlText = this.ExpandedControlText;
            result.collapsedControlText = this.CollapsedControlText;

            result.footerIcon = new TASKDIALOGCONFIG_ICON((int)this.FooterIcon);
            result.footerText = this.FooterText;

            result.callback = this.DialogProc;

            // Buttons
            if (this.CustomButtons != null)
            {
                result.buttonCount = (uint)this.CustomButtons.Count;
                result.buttons = this.CustomButtons.Handle;
                result.defaultButtonIndex = FindDefaultButtonId(this.CustomButtons);
            }
            else if (this.CommandLinks != null)
            {
                result.buttonCount = (uint)this.CommandLinks.Count;
                result.buttons = this.CommandLinks.Handle;
                result.flags |= TASKDIALOG_FLAGS.TDF_USE_COMMAND_LINKS;
                result.defaultButtonIndex = FindDefaultButtonId(this.CommandLinks);
            }

            // Radio Buttons
            if (this.RadioButtons != null)
            {
                result.radioButtonCount = (uint)this.RadioButtons.Count;
                result.radioButtons = this.RadioButtons.Handle;
                result.defaultRadioButtonIndex = FindDefaultButtonId(this.RadioButtons);
                if (result.defaultRadioButtonIndex == TaskDialogNativeMethods.NoDefaultButtonSpecified)
                {
                    result.flags |= TASKDIALOG_FLAGS.TDF_NO_DEFAULT_RADIO_BUTTON;
                }
            }

            // Progress Bar
            if (this.ProgressBar != null)
            {
                if (this.ProgressBar.State == TaskDialogProgressBarState.Marquee)
                {
                    result.flags |= TASKDIALOG_FLAGS.TDF_SHOW_MARQUEE_PROGRESS_BAR;
                }
                else
                {
                    result.flags |= TASKDIALOG_FLAGS.TDF_SHOW_PROGRESS_BAR;
                }
            }
            return result;
        }

        /// <summary>
        /// �f�t�H���g�R���g���[���� ID���������܂��B
        /// </summary>
        /// <param name="controls">�R���g���[���̃R���N�V�����B</param>
        /// <returns>
        /// �f�t�H���g�R���g���[���� ID�B
        /// �f�t�H���g�R���g���[�������݂��Ȃ��ꍇ�́A<see cref="TaskDialogNativeMethods.NoDefaultButtonSpecified"/>�B
        /// </returns>
        private static int FindDefaultButtonId(IEnumerable<TaskDialogButtonBase> controls)
        {
            Contract.Requires<ArgumentNullException>(controls != null);

            var defaults = controls.Where(x => x.Default).ToArray();
            if (defaults.Length > 1)
            {
                throw new InvalidOperationException(DialogErrorMessages.TaskDialogOnlyOneDefaultControl);
            }

            if (defaults.Length == 1)
            {
                return defaults[0].Id;
            }
            return TaskDialogNativeMethods.NoDefaultButtonSpecified;
        }

        #endregion

        /// <summary>
        /// �w�肵���t���O���ݒ肳��Ă��邩�ǂ����𔻒肷��l���擾���܂��B
        /// </summary>
        /// <param name="checkFlag">���肷��t���O�B</param>
        /// <returns>�t���O���ݒ肳��Ă���ꍇ��<c>true</c>�B����Ă��Ȃ��ꍇ��<c>false</c>�B</returns>
        internal bool GetFlag(TASKDIALOG_FLAGS checkFlag)
        {
            return (this.Flags & checkFlag) == checkFlag;
        }

        /// <summary>
        /// �t���O��ݒ肵�܂��B
        /// </summary>
        /// <param name="options"></param>
        internal void SetFlags(TaskDialogOptions options)
        {
            this.Flags = (TASKDIALOG_FLAGS)options;
        }

        /// <summary>
        /// �_�C�A���O�̃^�C�g����ݒ肵�܂��B
        /// </summary>
        /// <param name="title">�_�C�A���O�̃^�C�g���B</param>
        public void SetTitle(string title)
        {
            this.WindowTitle = title ?? String.Empty;
        }

        /// <summary>
        /// ��������ݒ肵�܂��B
        /// </summary>
        /// <param name="instructionText">�������B</param>
        public void SetMainInstructionText(string instructionText)
        {
            this.InstructionText = instructionText ?? String.Empty;
            if (this.DialogShowing)
            {
                this.Commands.SetInstructionText(this.InstructionText);
            }
        }

        /// <summary>
        /// �{����ݒ肵�܂��B
        /// </summary>
        /// <param name="contentText">�{���B</param>
        public void SetContentText(string contentText)
        {
            this.ContentText = contentText ?? String.Empty;
            if (this.DialogShowing)
            {
                this.Commands.SetContentText(this.ContentText);
            }
        }

        /// <summary>
        /// �t�b�^�[�e�L�X�g��ݒ肵�܂��B
        /// </summary>
        /// <param name="footerText">�t�b�^�[�e�L�X�g�B</param>
        public void SetFooterText(string footerText)
        {
            this.FooterText = footerText ?? String.Empty;
            if (this.DialogShowing)
            {
                this.Commands.SetFooterText(this.FooterText);
            }
        }

        public void SetExpandedText(string expandedText)
        {
            this.ExpandedText = expandedText ?? String.Empty;
            if (this.DialogShowing)
            {
                this.Commands.SetExpandedText(this.ExpandedText);
            }
        }

        public void SetExpandedControlText(string expandedControlText)
        {
            this.ExpandedControlText = expandedControlText ?? String.Empty;
        }

        public void SetCollapsedControlText(string collapsedControlText)
        {
            this.CollapsedControlText = collapsedControlText ?? String.Empty;
        }

        public void SetVerificationChecked(bool verificationChecked)
        {
            this.VerificationChecked = verificationChecked;
            if (this.DialogShowing)
            {
                this.Commands.ClickVerificationCommand(this.VerificationChecked.Value, true);
            }
        }

        public void SetVerificationText(string verificationText)
        {
            this.VerificationText = verificationText ?? String.Empty;
        }

        /// <summary>
        /// ���C���A�C�R����ݒ肵�܂��B
        /// </summary>
        /// <param name="mainIcon">���C���A�C�R���B</param>
        public void SetMainIcon(TaskDialogIcon mainIcon)
        {
            this.MainIcon = mainIcon;
            if (this.DialogShowing)
            {
                this.Commands.SetMainIcon(this.MainIcon);
            }
        }

        /// <summary>
        /// �t�b�^�[�A�C�R����ݒ肵�܂��B
        /// </summary>
        /// <param name="footerIcon">�t�b�^�[�A�C�R���B</param>
        public void SetFooterIcon(TaskDialogIcon footerIcon)
        {
            this.FooterIcon = footerIcon;
            if (this.DialogShowing)
            {
                this.Commands.SetFooterIcon(this.FooterIcon);
            }
        }

        /// <summary>
        /// �W���{�^����ݒ肵�܂��B
        /// </summary>
        /// <param name="commonButtons">�W���{�^���t���O�B</param>
        public void SetCommonButtons(TaskDialogCommonButtons commonButtons)
        {
            this.CommonButtons = (TASKDIALOG_COMMON_BUTTON_FLAGS)commonButtons;
        }

        /// <summary>
        /// �J�X�^���{�^���̃R���N�V������ݒ肵�܂��B
        /// </summary>
        /// <param name="customButtons">�J�X�^���{�^���̃R���N�V�����B</param>
        public void SetCustomButtons(TaskDialogControlCollection<TaskDialogButton> customButtons)
        {
            if (this.CustomButtons != null)
            {
                this.CustomButtons.Dispose();
                this.CustomButtons = null;
            }

            if (customButtons != null)
            {
                this.CustomButtons = new TaskDialogControlCollectionInternal<TaskDialogButton>(customButtons);
            }
        }

        /// <summary>
        /// �R�}���h�����N�̃R���N�V������ݒ肵�܂��B
        /// </summary>
        /// <param name="commandLinks">�R�}���h�����N�̃R���N�V�����B</param>
        public void SetCommandLinks(TaskDialogControlCollection<TaskDialogCommandLink> commandLinks)
        {
            if (this.CommandLinks != null)
            {
                this.CommandLinks.Dispose();
                this.CommandLinks = null;
            }

            if (commandLinks != null)
            {
                this.CommandLinks = new TaskDialogControlCollectionInternal<TaskDialogCommandLink>(commandLinks);
            }
        }

        /// <summary>
        /// ���W�I�{�^���̃R���N�V������ݒ肵�܂��B
        /// </summary>
        /// <param name="radioButtons">���W�I�{�^���̃R���N�V�����B</param>
        public void SetRadioButtons(TaskDialogControlCollection<TaskDialogRadioButton> radioButtons)
        {
            if (this.RadioButtons != null)
            {
                this.RadioButtons.Dispose();
                this.RadioButtons = null;
            }

            if (radioButtons != null)
            {
                this.RadioButtons = new TaskDialogControlCollectionInternal<TaskDialogRadioButton>(radioButtons);
            }
        }

        /// <summary>
        /// �v���O���X�o�[��ݒ肵�܂��B
        /// </summary>
        /// <param name="progressBar">�v���O���X�o�[�B</param>
        public void SetProgressBar(TaskDialogProgressBar progressBar)
        {
            if (this.ProgressBar != null)
            {
                this.ProgressBar.Reset();
                this.ProgressBar = null;
            }

            if (progressBar != null)
            {
                this.ProgressBar = progressBar;
                progressBar.Attach(this.Dialog);
            }
        }

        /// <summary>
        /// �v���O���X�o�[�̏�Ԃ�ݒ肵�܂��B
        /// </summary>
        /// <param name="control">�v���O���X�o�[�B</param>
        /// <param name="state">�v���O���X�o�[��ԁB</param>
        public void SetProgressBarState(TaskDialogProgressBar control, TaskDialogProgressBarState state)
        {
            this.Commands.SetProgressBarStateCommand(state);
        }

        /// <summary>
        /// �v���O���X�o�[�̈ʒu��ݒ肵�܂��B
        /// </summary>
        /// <param name="control">�v���O���X�o�[�B</param>
        /// <param name="position">�v���O���X�o�[�̈ʒu�B</param>
        public void SetProgressBarPosition(TaskDialogProgressBar control, int position)
        {
            this.Commands.SetProgressBarPosCommand(position);
        }

        /// <summary>
        /// �v���O���X�o�[�͈̔͂�ݒ肵�܂��B
        /// </summary>
        /// <param name="control">�v���O���X�o�[�B</param>
        /// <param name="minimum">�v���O���X�o�[�̍ŏ��l�B</param>
        /// <param name="maxmum">�v���O���X�o�[�̍ő�l�B</param>
        public void SetProgressBarRange(TaskDialogProgressBar control, int minimum, int maxmum)
        {
            this.Commands.SetProgressBarRangeCommand(minimum, maxmum);
        }

        public void SetButtonElevationRequiredState(TaskDialogButtonBase control, bool evelationRequired)
        {
            this.Commands.SetButtonElevationRequiredStateCommand(control, evelationRequired);
        }

        public void SetButtonEnabled(TaskDialogButtonBase control, bool enabled)
        {
            this.Commands.EnableButtonCommand(control.Id, enabled);
        }

        public void SetRadioButtonEnabled(TaskDialogRadioButton control, bool enabled)
        {
            this.Commands.EnableRadioButtonCommand(control.Id, enabled);
        }

        #region TaskDialog Notifications

        private int DialogProc(IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam, IntPtr refData)
        {
            switch ((TASKDIALOG_NOTIFICATIONS)message)
            {
                case TASKDIALOG_NOTIFICATIONS.TDN_DIALOG_CONSTRUCTED:
                {
                    this.Commands.WindowHandle = hWnd;
                    return TaskDialogConstructedEvent();
                }

                case TASKDIALOG_NOTIFICATIONS.TDN_CREATED:
                {
                    var hresult = TaskDialogCreatedEvent();
                    return hresult;
                }

                case TASKDIALOG_NOTIFICATIONS.TDN_DESTROYED:
                {
                    return TaskDialogDestroyedEvent();
                }

                case TASKDIALOG_NOTIFICATIONS.TDN_BUTTON_CLICKED:
                {
                    return TaskDialogButtonClickedEvent(wParam.ToInt32());
                }

                case TASKDIALOG_NOTIFICATIONS.TDN_RADIO_BUTTON_CLICKED:
                {
                    return TaskDialogRadioButtonClickedEvent(wParam.ToInt32());
                }

                case TASKDIALOG_NOTIFICATIONS.TDN_HYPERLINK_CLICKED:
                {
                    return TaskDialogHyperlinkClickedEvent(lParam);
                }

                case TASKDIALOG_NOTIFICATIONS.TDN_VERIFICATION_CLICKED:
                {
                    return TaskDialogVerificationClickedEvent(wParam.ToInt32());
                }

                case TASKDIALOG_NOTIFICATIONS.TDN_EXPANDO_BUTTON_CLICKED:
                {
                    return TaskDialogExpandoButtonClickedEvent(wParam.ToInt32());
                }

                case TASKDIALOG_NOTIFICATIONS.TDN_TIMER:
                {
                    return TaskDialogTimerEvent(wParam.ToInt32());
                }

                case TASKDIALOG_NOTIFICATIONS.TDN_NAVIGATED:
                {
                    return TaskDialogNavigatedEvent();
                }

                case TASKDIALOG_NOTIFICATIONS.TDN_HELP:
                {
                    return TaskDialogHelpEvent();
                }
            }

            return S_OK;
        }

        /// <summary>
        /// �^�X�N�_�C�A���O�\���O�쐬�C�x���g���������܂��B
        /// </summary>
        /// <returns></returns>
        private int TaskDialogConstructedEvent()
        {
            return Ignored;
        }

        /// <summary>
        /// �^�X�N�_�C�A���O�쐬�C�x���g���������܂��B
        /// </summary>
        /// <returns></returns>
        private int TaskDialogCreatedEvent()
        {
            if (this.ProgressBar != null)
            {
                // �v���O���X�o�[�ݒ�
                if (GetFlag(TASKDIALOG_FLAGS.TDF_SHOW_PROGRESS_BAR))
                {
                    this.Commands.SetProgressBarRangeCommand(this.ProgressBar.Minimum, this.ProgressBar.Maximum);
                    this.Commands.SetProgressBarStateCommand(this.ProgressBar.State);
                    this.Commands.SetProgressBarPosCommand(this.ProgressBar.Value);
                }
                else if (GetFlag(TASKDIALOG_FLAGS.TDF_SHOW_MARQUEE_PROGRESS_BAR))
                {
                    this.Commands.SetMarqueeProgressBarCommand(true);
                    this.Commands.SetProgressBarStateCommand(this.ProgressBar.State);
                }
            }

            if (this.CustomButtons != null)
            {
                // �J�X�^���{�^���ݒ�
                foreach (var button in this.CustomButtons.Where(x => x.UseElevationIcon))
                {
                    this.Commands.SetButtonElevationRequiredStateCommand(button, true);
                }
            }
            else if (this.CommandLinks != null)
            {
                // �R�}���h�����N�ݒ�B
                foreach (var commandLink in this.CommandLinks.Where(x => x.UseElevationIcon))
                {
                    this.Commands.SetButtonElevationRequiredStateCommand(commandLink, true);
                }
            }

            // �A�C�R���ݒ�
            if (this.MainIcon != TaskDialogIcon.None)
            {
                this.Commands.SetMainIcon(this.MainIcon);
            }
            if (this.FooterIcon != TaskDialogIcon.None)
            {
                this.Commands.SetFooterIcon(this.FooterIcon);
            }

            this.Dialog.RaiseOpenedEvent();

            return Ignored;
        }

        private int TaskDialogNavigatedEvent()
        {
            this.Dialog.RaiseNavigatedEvent();
            return Ignored;
        }

        /// <summary>
        /// �^�X�N�_�C�A���O�{�^���N���b�N�C�x���g���������܂��B
        /// </summary>
        /// <param name="buttonId">�N���b�N���ꂽ�{�^����ID�B</param>
        /// <returns></returns>
        private int TaskDialogButtonClickedEvent(int buttonId)
        {
            if (this.DialogShowStates == DialogShowStates.Showing)
            {
                // �_�C�A���O�\�����̃{�^���N���b�N
                this.Dialog.RaiseButtonClickedEvent(buttonId);

                if (buttonId < (int)TaskDialogCommonButtonId.MinCustomControlId)
                {
                    // �W���{�^��
                    return this.Dialog.RaiseClosingEvent(buttonId) ? S_OK : S_FALSE;
                }
                else if (this.CustomButtons != null && this.CustomButtons.Any())
                {
                    // �J�X�^���{�^��
                    var button = this.CustomButtons.FirstOrDefault(x => x.Id == buttonId);
                    if (button != null && button.DialogClosable)
                    {
                        return this.Dialog.RaiseClosingEvent(buttonId) ? S_OK : S_FALSE;
                    }
                }
                else if (this.CommandLinks != null && this.CommandLinks.Any())
                {
                    // �R�}���h�����N
                    var control = this.CommandLinks.FirstOrDefault(x => x.Id == buttonId);
                    if (control != null)
                    {
                        return this.Dialog.RaiseClosingEvent(buttonId) ? S_OK : S_FALSE;
                    }
                }
            }
            else if (this.DialogShowStates == DialogShowStates.Closing)
            {
                // �蓮�ŕ��邽�߂̃{�^���N���b�N: �_�C�A���O�����B
                return S_OK;
            }
            return S_FALSE;
        }

        private int TaskDialogHyperlinkClickedEvent(IntPtr pszHREF)
        {
            var link = Marshal.PtrToStringUni(pszHREF);
            this.Dialog.RaiseHyperlinkClickedEvent(link);

            return Ignored;
        }

        private int TaskDialogTimerEvent(int ticks)
        {
            this.Dialog.RaiseTimerEvent(ticks);
            return Ignored;
        }

        private int TaskDialogDestroyedEvent()
        {
            this.firstRadioButtonClicked = true;
            return Ignored;
        }

        private int TaskDialogRadioButtonClickedEvent(int radioButtonId)
        {
            if (this.firstRadioButtonClicked &&
                !GetFlag(TASKDIALOG_FLAGS.TDF_NO_DEFAULT_RADIO_BUTTON))
            {
                this.firstRadioButtonClicked = false;
            }
            else
            {
                this.Dialog.RaiseRadioButtonClickedEvent(radioButtonId);
            }

            return Ignored;
        }

        private int TaskDialogVerificationClickedEvent(int checkBoxCheched)
        {
            this.Dialog.RaiseVerificationClickedEvent(checkBoxCheched != 0);
            return Ignored;
        }

        private int TaskDialogHelpEvent()
        {
            this.Dialog.RaiseHelpEvent();
            return Ignored;
        }

        private int TaskDialogExpandoButtonClickedEvent(int expanded)
        {
            this.Dialog.RaiseExpandoButtonClickedEvent(expanded != 0);
            return Ignored;
        }

        #endregion
    }
}