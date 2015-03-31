using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using starshipxac.Windows.Dialogs.Controls;
using starshipxac.Windows.Properties;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// �^�X�N�_�C�A���O���`���܂��B
    /// </summary>
    public class TaskDialog : TaskDialogBase
    {
        private bool disposed = false;

        private string title = String.Empty;
        private string mainInstructionText = String.Empty;
        private string contentText = String.Empty;
        private string footerText = String.Empty;
        private string expandedText = String.Empty;
        private string expandedControlText = String.Empty;
        private string collapsedControlText = String.Empty;
        private bool? verificationChecked;
        private string verificationText = String.Empty;
        private TaskDialogIcon mainIcon = TaskDialogIcon.None;
        private TaskDialogIcon footerIcon = TaskDialogIcon.None;
        private TaskDialogCommonButtons commonButtons = TaskDialogCommonButtons.None;

        /// <summary>
        /// <see cref="TaskDialog"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        public TaskDialog()
        {
            this.HyperlinksEnabled = false;
            this.Expanded = false;
            this.ExpansionMode = TaskDialogExpansionMode.Hide;
            
            this.CustomButtons = new TaskDialogControlCollection<TaskDialogButton>(this);
            this.CommandLinks = new TaskDialogControlCollection<TaskDialogCommandLink>(this);
            this.RadioButtons = new TaskDialogControlCollection<TaskDialogRadioButton>(this);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        if (this.DialogShowing)
                        {
                            CloseDialog(TaskDialogCommonButtons.Cancel);
                        }
                    }

                    this.disposed = true;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.mainInstructionText != null);
            Contract.Invariant(this.CustomButtons != null);
            Contract.Invariant(this.CommandLinks != null);
            Contract.Invariant(this.RadioButtons != null);
        }

        /// <summary>
        /// �_�C�A���O�̃^�C�g�����擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (this.title == value)
                {
                    return;
                }

                this.title = value ?? String.Empty;
                SetTitle(this.title);
            }
        }

        /// <summary>
        /// ���������擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public string MainInstructionText
        {
            get
            {
                return this.mainInstructionText;
            }
            set
            {
                if (this.mainInstructionText == value)
                {
                    return;
                }

                this.mainInstructionText = value ?? String.Empty;
                SetMainInstructionText(this.mainInstructionText);
            }
        }

        /// <summary>
        /// �{�����擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public string ContentText
        {
            get
            {
                return this.contentText;
            }
            set
            {
                if (this.contentText == value)
                {
                    return;
                }

                this.contentText = value ?? String.Empty;
                SetContentText(this.contentText);
            }
        }

        /// <summary>
        /// �t�b�^�[�e�L�X�g���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public string FooterText
        {
            get
            {
                return this.footerText;
            }
            set
            {
                if (this.footerText == value)
                {
                    return;
                }

                this.footerText = value ?? String.Empty;
                SetFooterText(this.footerText);
            }
        }

        public string ExpandedText
        {
            get
            {
                return this.expandedText;
            }
            set
            {
                if (this.expandedText == value)
                {
                    return;
                }
                this.expandedText = value ?? String.Empty;
                SetExpandedText(this.expandedText);
            }
        }

        public string ExpandedControlText
        {
            get
            {
                return this.expandedControlText;
            }
            set
            {
                if (this.expandedControlText == value)
                {
                    return;
                }
                this.expandedControlText = value ?? String.Empty;
                SetExpandedControlText(this.expandedControlText);
            }
        }

        public string CollapsedControlText
        {
            get
            {
                return this.collapsedControlText;
            }
            set
            {
                if (this.collapsedControlText == value)
                {
                    return;
                }

                this.collapsedControlText = value ?? String.Empty;
                SetCollapsedControlText(this.collapsedControlText);
            }
        }

        /// <summary>
        /// �m�F�`�F�b�N�{�b�N�X���`�F�b�N����Ă��邩�ǂ����𔻒肷��l���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public bool? VerificationChecked
        {
            get
            {
                return this.verificationChecked.GetValueOrDefault(false);
            }
            set
            {
                this.verificationChecked = value;
                SetVerificationChecked(this.verificationChecked.HasValue && this.verificationChecked.Value);
            }
        }

        /// <summary>
        /// �m�F�����擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public string VerificationText
        {
            get
            {
                return this.verificationText;
            }
            set
            {
                if (this.verificationText == value)
                {
                    return;
                }

                this.verificationText = value ?? String.Empty;
                SetVerificationText(this.verificationText);
            }
        }

        /// <summary>
        /// �A�C�R�����擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public TaskDialogIcon MainIcon
        {
            get
            {
                return this.mainIcon;
            }
            set
            {
                if (this.mainIcon == value)
                {
                    return;
                }

                this.mainIcon = value;
                SetMainIcon(this.mainIcon);
            }
        }

        /// <summary>
        /// �t�b�^�[�A�C�R�����擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public TaskDialogIcon FooterIcon
        {
            get
            {
                return this.footerIcon;
            }
            set
            {
                if (this.footerIcon == value)
                {
                    return;
                }

                this.footerIcon = value;
                SetFooterIcon(this.footerIcon);
            }
        }

        /// <summary>
        /// �W���{�^�����擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public TaskDialogCommonButtons CommonButtons
        {
            get
            {
                return this.commonButtons;
            }
            set
            {
                if (this.commonButtons == value)
                {
                    return;
                }

                this.commonButtons = value;
                SetCommonButtons(this.commonButtons);
            }
        }
        
        public bool HyperlinksEnabled { get; set; }

        public bool Expanded { get; set; }

        public TaskDialogExpansionMode ExpansionMode { get; set; }

        /// <summary>
        /// �J�X�^���{�^���̃R���N�V�������擾���܂��B
        /// </summary>
        public TaskDialogControlCollection<TaskDialogButton> CustomButtons { get; private set; }

        /// <summary>
        /// �R�}���h�����N�̃R���N�V�������擾���܂��B
        /// </summary>
        public TaskDialogControlCollection<TaskDialogCommandLink> CommandLinks { get; private set; }

        /// <summary>
        /// ���W�I�{�^���̃R���N�V�������擾���܂��B
        /// </summary>
        public TaskDialogControlCollection<TaskDialogRadioButton> RadioButtons { get; private set; }

        /// <summary>
        /// �v���O���X�o�[���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public TaskDialogProgressBar ProgressBar { get; set; }

        #region Events

        #region Opened Event

        /// <summary>
        /// �_�C�A���O���J���Ɣ������܂��B
        /// </summary>
        public event EventHandler Opened;

        protected virtual void OnOpened(EventArgs args)
        {
            var handler = this.Opened;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        #region Closing Event

        /// <summary>
        /// �_�C�A���O������O�ɔ������܂��B
        /// </summary>
        public event EventHandler<TaskDialogClosingEventArgs> Closing;

        protected virtual void OnClosing(TaskDialogClosingEventArgs args)
        {
            var handler = this.Closing;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        #region HperlinkClicked Event

        /// <summary>
        /// �n�C�p�[�����N���N���b�N�����Ɣ������܂��B
        /// </summary>
        public event EventHandler<TaskDialogHyperlinkClickedEventArgs> HyperlinkClicked;

        protected virtual void OnHyperlinkClicked(TaskDialogHyperlinkClickedEventArgs args)
        {
            var handler = this.HyperlinkClicked;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        #region VerificationChanged Event

        /// <summary>
        /// �m�F�`�F�b�N�{�b�N�X���ύX�����Ɣ������܂��B
        /// </summary>
        public event EventHandler<TaskDialogVerificationChangedEventArgs> VerificationChanged;

        protected virtual void OnVerificationChanged(TaskDialogVerificationChangedEventArgs args)
        {
            var handler = this.VerificationChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        #region ExpandChanged Event

        /// <summary>
        /// �g���̈�̕\����Ԃ��ύX�����Ɣ������܂��B
        /// </summary>
        public event EventHandler<TaskDialogExpandChangedEventArgs> ExpandChanged;

        protected virtual void OnExpandChanged(TaskDialogExpandChangedEventArgs args)
        {
            var handler = this.ExpandChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        #region HelpInvoked Event

        /// <summary>
        /// �w���v�����s�����Ɣ������܂��B
        /// </summary>
        public event EventHandler HelpInvoked;

        protected virtual void OnHelpInvoked(EventArgs args)
        {
            var handler = this.HelpInvoked;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        #region Timer Evnt

        /// <summary>
        /// �^�C�}�[�C�x���g�𔭐����܂��B
        /// </summary>
        public event EventHandler<TaskDialogTimerEventArgs> Timer;

        protected virtual void OnTimer(TaskDialogTimerEventArgs args)
        {
            var handler = this.Timer;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// �_�C�A���O��\�����܂��B
        /// </summary>
        /// <returns>�^�X�N�_�C�A���O���s���ʁB</returns>
        public TaskDialogResult Show()
        {
            return Show(null);
        }

        /// <summary>
        /// �e�E�B���h�E���w�肵�āA�_�C�A���O��\�����܂��B
        /// </summary>
        /// <param name="parentWindow">�e�E�B���h�E�B</param>
        /// <returns>�^�X�N�_�C�A���O���s���ʁB</returns>
        public TaskDialogResult Show(Window parentWindow)
        {
            if (this.CustomButtons.Any())
            {
                SetCustomButtons(this.CustomButtons);
            }
            else if (this.CommandLinks.Any())
            {
                SetCommandLinks(this.CommandLinks);
            }

            if (this.RadioButtons.Any())
            {
                SetRadioButtons(this.RadioButtons);
            }

            if (this.ProgressBar != null)
            {
                SetProgressBar(this.ProgressBar);
            }

            return ShowDialog(parentWindow);
        }

        /// <summary>
        /// �_�C�A���O����܂��B
        /// </summary>
        public void Close()
        {
            Close(TaskDialogCommonButtons.Cancel);
        }

        /// <summary>
        /// <see cref="TaskDialogSelectedButton"/>���w�肵�āA�_�C�A���O����܂��B
        /// </summary>
        /// <param name="commonButton"></param>
        public void Close(TaskDialogCommonButtons commonButton)
        {
            Contract.Requires<InvalidOperationException>(this.DialogShowing, DialogErrorMessages.TaskDialogCloseNonShowing);

            CloseDialog(commonButton);
        }

        public void Close(int buttonId)
        {
            Contract.Requires<InvalidOperationException>(this.DialogShowing, DialogErrorMessages.TaskDialogCloseNonShowing);

            CloseDialog(buttonId);
        }

        #region TaskDialogBase Methods

        protected internal override TaskDialogOptions GetDialogOptions()
        {
            var result = base.GetDialogOptions();

            if (this.VerificationChecked.HasValue && this.VerificationChecked.Value)
            {
                result |= TaskDialogOptions.VerificationChecked;
            }
            if (this.HyperlinksEnabled)
            {
                result |= TaskDialogOptions.HyperlinksEnabled;
            }
            if (this.Expanded)
            {
                result |= TaskDialogOptions.Expanded;
            }
            if (this.ExpansionMode == TaskDialogExpansionMode.ExpandFooter)
            {
                result |= TaskDialogOptions.ExpandFooterArea;
            }

            return result;
        }

        protected internal override TaskDialogButtonBase FindButton(int buttonId)
        {
            if (this.CustomButtons.Any())
            {
                return this.CustomButtons.FirstOrDefault(x => x.Id == buttonId);
            }
            else if (this.CommandLinks.Any())
            {
                return this.CommandLinks.FirstOrDefault(x => x.Id == buttonId);
            }
            return null;
        }

        protected internal override void RaiseOpenedEvent()
        {
            OnOpened(EventArgs.Empty);
        }

        protected internal override bool RaiseClosingEvent(int buttonId)
        {
            TaskDialogClosingEventArgs args;
            if ((int)TaskDialogCommonButtonId.MinCustomControlId <= buttonId)
            {
                var control = FindButton(buttonId);
                if (control == null)
                {
                    throw new InvalidOperationException(DialogErrorMessages.TaskDialogInvalidateButtonId);
                }

                args = TaskDialogClosingEventArgs.Create(control);
            }
            else
            {
                args = TaskDialogClosingEventArgs.Create((TaskDialogCommonButtonId)buttonId);
            }

            OnClosing(args);
            return !args.Cancel;
        }

        protected internal override void RaiseButtonClickedEvent(int buttonId)
        {
            var button = FindButton(buttonId);
            if (button != null)
            {
                button.RaiseClickEvent();
            }
        }

        protected internal override void RaiseRadioButtonClickedEvent(int radioButtonId)
        {
            var control = this.RadioButtons.FirstOrDefault(x => x.Id == radioButtonId);
            if (control != null)
            {
                control.RaiseClickEvent();
            }
        }

        protected internal override void RaiseHyperlinkClickedEvent(string link)
        {
            OnHyperlinkClicked(new TaskDialogHyperlinkClickedEventArgs(link));
        }

        protected internal override void RaiseVerificationClickedEvent(bool verificationChecked)
        {
            OnVerificationChanged(new TaskDialogVerificationChangedEventArgs(verificationChecked));
        }

        protected internal override void RaiseExpandoButtonClickedEvent(bool expanded)
        {
            OnExpandChanged(new TaskDialogExpandChangedEventArgs(expanded));
        }

        protected internal override void RaiseHelpEvent()
        {
            OnHelpInvoked(EventArgs.Empty);
        }

        protected internal override void RaiseTimerEvent(int ticks)
        {
            OnTimer(new TaskDialogTimerEventArgs(ticks));
        }

        #endregion
    }
}