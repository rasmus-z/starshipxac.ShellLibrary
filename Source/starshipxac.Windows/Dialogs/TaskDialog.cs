using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using starshipxac.Windows.Dialogs.Controls;
using starshipxac.Windows.Properties;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    ///     タスクダイアログを定義します。
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
        ///     <see cref="TaskDialog" />クラスの新しいインスタンスを初期化します。
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
                    this.disposed = true;

                    if (disposing)
                    {
                        if (this.DialogShowing)
                        {
                            CloseDialog(TaskDialogCommonButtons.Cancel);
                        }
                    }
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
        ///     ダイアログのタイトルを取得または設定します。
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                SetProperty(ref this.title, value, SetWindowTitle);
            }
        }

        /// <summary>
        ///     説明文を取得または設定します。
        /// </summary>
        public string MainInstructionText
        {
            get
            {
                return this.mainInstructionText;
            }
            set
            {
                SetProperty(ref this.mainInstructionText, value, SetMainInstructionText);
            }
        }

        /// <summary>
        ///     本文を取得または設定します。
        /// </summary>
        public string ContentText
        {
            get
            {
                return this.contentText;
            }
            set
            {
                SetProperty(ref this.contentText, value, SetContentText);
            }
        }

        /// <summary>
        ///     フッターテキストを取得または設定します。
        /// </summary>
        public string FooterText
        {
            get
            {
                return this.footerText;
            }
            set
            {
                SetProperty(ref this.footerText, value, SetFooterText);
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
                SetProperty(ref this.expandedText, value, SetExpandedText);
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
                SetProperty(ref this.expandedControlText, value, SetExpandedControlText);
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
                SetProperty(ref this.collapsedControlText, value, SetCollapsedControlText);
            }
        }

        /// <summary>
        ///     確認チェックボックスがチェックされているかどうかを判定する値を取得または設定します。
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
        ///     確認文を取得または設定します。
        /// </summary>
        public string VerificationText
        {
            get
            {
                return this.verificationText;
            }
            set
            {
                SetProperty(ref this.verificationText, value, SetVerificationText);
            }
        }

        /// <summary>
        ///     アイコンを取得または設定します。
        /// </summary>
        public TaskDialogIcon MainIcon
        {
            get
            {
                return this.mainIcon;
            }
            set
            {
                SetProperty(ref this.mainIcon, value, SetMainIcon);
            }
        }

        /// <summary>
        ///     フッターアイコンを取得または設定します。
        /// </summary>
        public TaskDialogIcon FooterIcon
        {
            get
            {
                return this.footerIcon;
            }
            set
            {
                SetProperty(ref this.footerIcon, value, SetFooterIcon);
            }
        }

        /// <summary>
        ///     標準ボタンを取得または設定します。
        /// </summary>
        public TaskDialogCommonButtons CommonButtons
        {
            get
            {
                return this.commonButtons;
            }
            set
            {
                SetProperty(ref this.commonButtons, value, SetCommonButtons);
            }
        }

        public bool HyperlinksEnabled { get; set; }

        public bool Expanded { get; set; }

        public TaskDialogExpansionMode ExpansionMode { get; set; }

        /// <summary>
        ///     カスタムボタンのコレクションを取得します。
        /// </summary>
        public TaskDialogControlCollection<TaskDialogButton> CustomButtons { get; }

        /// <summary>
        ///     コマンドリンクのコレクションを取得します。
        /// </summary>
        public TaskDialogControlCollection<TaskDialogCommandLink> CommandLinks { get; }

        /// <summary>
        ///     ラジオボタンのコレクションを取得します。
        /// </summary>
        public TaskDialogControlCollection<TaskDialogRadioButton> RadioButtons { get; }

        /// <summary>
        ///     プログレスバーを取得または設定します。
        /// </summary>
        public TaskDialogProgressBarBase ProgressBar { get; set; }

        #region Events

        #region Opened Event

        /// <summary>
        ///     ダイアログが開くと発生します。
        /// </summary>
        public event EventHandler Opened;

        protected virtual void OnOpened(EventArgs args)
        {
            this.Opened?.Invoke(this, args);
        }

        #endregion

        #region Closing Event

        /// <summary>
        ///     ダイアログが閉じる前に発生します。
        /// </summary>
        public event EventHandler<TaskDialogClosingEventArgs> Closing;

        protected virtual void OnClosing(TaskDialogClosingEventArgs args)
        {
            this.Closing?.Invoke(this, args);
        }

        #endregion

        #region HperlinkClicked Event

        /// <summary>
        ///     ハイパーリンクがクリックされると発生します。
        /// </summary>
        public event EventHandler<TaskDialogHyperlinkClickedEventArgs> HyperlinkClicked;

        protected virtual void OnHyperlinkClicked(TaskDialogHyperlinkClickedEventArgs args)
        {
            this.HyperlinkClicked?.Invoke(this, args);
        }

        #endregion

        #region VerificationChanged Event

        /// <summary>
        ///     確認チェックボックスが変更されると発生します。
        /// </summary>
        public event EventHandler<TaskDialogVerificationChangedEventArgs> VerificationChanged;

        protected virtual void OnVerificationChanged(TaskDialogVerificationChangedEventArgs args)
        {
            this.VerificationChanged?.Invoke(this, args);
        }

        #endregion

        #region ExpandChanged Event

        /// <summary>
        ///     拡張領域の表示状態が変更されると発生します。
        /// </summary>
        public event EventHandler<TaskDialogExpandChangedEventArgs> ExpandChanged;

        protected virtual void OnExpandChanged(TaskDialogExpandChangedEventArgs args)
        {
            this.ExpandChanged?.Invoke(this, args);
        }

        #endregion

        #region HelpInvoked Event

        /// <summary>
        ///     ヘルプが実行されると発生します。
        /// </summary>
        public event EventHandler HelpInvoked;

        protected virtual void OnHelpInvoked(EventArgs args)
        {
            this.HelpInvoked?.Invoke(this, args);
        }

        #endregion

        #region Timer Evnt

        /// <summary>
        ///     タイマーイベントを発生します。
        /// </summary>
        public event EventHandler<TaskDialogTimerEventArgs> Timer;

        protected virtual void OnTimer(TaskDialogTimerEventArgs args)
        {
            this.Timer?.Invoke(this, args);
        }

        #endregion

        #endregion

        /// <summary>
        ///     ダイアログを表示します。
        /// </summary>
        /// <returns>タスクダイアログ実行結果。</returns>
        public TaskDialogResult Show()
        {
            return Show(null);
        }

        /// <summary>
        ///     親ウィンドウを指定して、ダイアログを表示します。
        /// </summary>
        /// <param name="parentWindow">親ウィンドウ。</param>
        /// <returns>タスクダイアログ実行結果。</returns>
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
        ///     ダイアログを閉じます。
        /// </summary>
        public void Close()
        {
            Close(TaskDialogCommonButtons.Cancel);
        }

        /// <summary>
        ///     <see cref="TaskDialogSelectedButton" />を指定して、ダイアログを閉じます。
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
            if (this.Timer != null)
            {
                result |= TaskDialogOptions.EnableTimer;
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
            if ((int)TaskDialogCommonButtons.MinCustomControlId <= buttonId)
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
                args = TaskDialogClosingEventArgs.Create((TaskDialogCommonButtons)buttonId);
            }

            OnClosing(args);
            return !args.Cancel;
        }

        protected internal override void RaiseButtonClickedEvent(int buttonId)
        {
            var button = FindButton(buttonId);
            button?.RaiseClickEvent();
        }

        protected internal override void RaiseRadioButtonClickedEvent(int radioButtonId)
        {
            var control = this.RadioButtons.FirstOrDefault(x => x.Id == radioButtonId);
            control?.RaiseClickEvent();
        }

        protected internal override void RaiseHyperlinkClickedEvent(string link)
        {
            OnHyperlinkClicked(new TaskDialogHyperlinkClickedEventArgs(link));
        }

        protected internal override void RaiseVerificationClickedEvent(bool verification)
        {
            OnVerificationChanged(new TaskDialogVerificationChangedEventArgs(verification));
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