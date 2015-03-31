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
    /// タスクダイアログの設定を定義します。
    /// </summary>
    internal sealed class TaskDialogInternal : IDisposable
    {
        private bool firstRadioButtonClicked = false;

        private const int S_OK = 0;
        private const int S_FALSE = 1;
        private const int Ignored = S_OK;

        /// <summary>
        /// <see cref="TaskDialogInternal"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="dialog"><see cref="TaskDialogBase"/>。</param>
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
        /// <see cref="TaskDialogBase"/>を取得します。
        /// </summary>
        private TaskDialogBase Dialog { get; set; }

        /// <summary>
        /// ダイアログの表示状態を取得します。
        /// </summary>
        public DialogShowStates DialogShowStates { get; internal set; }

        /// <summary>
        /// ダイアログが表示中かどうかを判定する値を取得します。
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
        /// タスクダイアログフラグを取得または設定します。
        /// </summary>
        private TASKDIALOG_FLAGS Flags { get; set; }

        /// <summary>
        /// ダイアログのタイトルを取得または設定します。
        /// </summary>
        private string WindowTitle { get; set; }

        /// <summary>
        /// 説明文を取得または設定します。
        /// </summary>
        private string InstructionText { get; set; }

        /// <summary>
        /// 本文を取得または設定します。
        /// </summary>
        private string ContentText { get; set; }

        /// <summary>
        /// フッターテキストを取得または設定します。
        /// </summary>
        private string FooterText { get; set; }

        private string ExpandedText { get; set; }

        private string ExpandedControlText { get; set; }

        private string CollapsedControlText { get; set; }

        private bool? VerificationChecked { get; set; }

        private string VerificationText { get; set; }

        /// <summary>
        /// メインアイコンを取得または設定します。
        /// </summary>
        private TaskDialogIcon MainIcon { get; set; }

        /// <summary>
        /// フッターアイコンを取得または設定します。
        /// </summary>
        private TaskDialogIcon FooterIcon { get; set; }

        /// <summary>
        /// 標準ボタンフラグを取得または設定します。
        /// </summary>
        private TASKDIALOG_COMMON_BUTTON_FLAGS CommonButtons { get; set; }

        /// <summary>
        /// カスタムボタンのコレクションを取得または設定します。
        /// </summary>
        private TaskDialogControlCollectionInternal<TaskDialogButton> CustomButtons { get; set; }

        /// <summary>
        /// コマンドリンクのコレクションを取得または設定します。
        /// </summary>
        private TaskDialogControlCollectionInternal<TaskDialogCommandLink> CommandLinks { get; set; }

        /// <summary>
        /// ラジオボタンのコレクションを取得または設定します。
        /// </summary>
        private TaskDialogControlCollectionInternal<TaskDialogRadioButton> RadioButtons { get; set; }

        /// <summary>
        /// プログレスバーを取得または設定します。
        /// </summary>
        private TaskDialogProgressBar ProgressBar { get; set; }

        private TaskDialogCommands Commands { get; set; }

        /// <summary>
        /// タスクダイアログを表示します。
        /// </summary>
        /// <param name="parentWindowHandle">親ウィンドウのハンドル。</param>
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
        /// 手動でダイアログを閉じます。
        /// </summary>
        /// <param name="commonButtonId">押されたと仮定するボタンのID。</param>
        public void CloseDialog(TaskDialogCommonButtons commonButtonId)
        {
            this.DialogShowStates = DialogShowStates.Closing;

            this.Commands.ClickButtonCommand((int)TaskDialogCommonButtons.Cancel);
            this.Dialog.RaiseClosingEvent((int)commonButtonId);
        }

        /// <summary>
        /// 手動でダイアログを閉じます。
        /// </summary>
        /// <param name="buttonId">押されたと仮定するボタンのID。</param>
        public void CloseDialog(int buttonId)
        {
            this.DialogShowStates = DialogShowStates.Closing;

            this.Commands.ClickButtonCommand((int)TaskDialogCommonButtons.Cancel);
            this.Dialog.RaiseClosingEvent(buttonId);
        }

        #region Create Native Config

        /// <summary>
        /// <see cref="TASKDIALOGCONFIG"/>を作成します。
        /// </summary>
        /// <param name="parentWindowHandle">親ウィンドウのハンドル。</param>
        /// <returns>作成した<see cref="TASKDIALOGCONFIG"/>。</returns>
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
        /// デフォルトコントロールの IDを検索します。
        /// </summary>
        /// <param name="controls">コントロールのコレクション。</param>
        /// <returns>
        /// デフォルトコントロールの ID。
        /// デフォルトコントロールが存在しない場合は、<see cref="TaskDialogNativeMethods.NoDefaultButtonSpecified"/>。
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
        /// 指定したフラグが設定されているかどうかを判定する値を取得します。
        /// </summary>
        /// <param name="checkFlag">判定するフラグ。</param>
        /// <returns>フラグが設定されている場合は<c>true</c>。されていない場合は<c>false</c>。</returns>
        internal bool GetFlag(TASKDIALOG_FLAGS checkFlag)
        {
            return (this.Flags & checkFlag) == checkFlag;
        }

        /// <summary>
        /// フラグを設定します。
        /// </summary>
        /// <param name="options"></param>
        internal void SetFlags(TaskDialogOptions options)
        {
            this.Flags = (TASKDIALOG_FLAGS)options;
        }

        /// <summary>
        /// ダイアログのタイトルを設定します。
        /// </summary>
        /// <param name="title">ダイアログのタイトル。</param>
        public void SetTitle(string title)
        {
            this.WindowTitle = title ?? String.Empty;
        }

        /// <summary>
        /// 説明文を設定します。
        /// </summary>
        /// <param name="instructionText">説明文。</param>
        public void SetMainInstructionText(string instructionText)
        {
            this.InstructionText = instructionText ?? String.Empty;
            if (this.DialogShowing)
            {
                this.Commands.SetInstructionText(this.InstructionText);
            }
        }

        /// <summary>
        /// 本文を設定します。
        /// </summary>
        /// <param name="contentText">本文。</param>
        public void SetContentText(string contentText)
        {
            this.ContentText = contentText ?? String.Empty;
            if (this.DialogShowing)
            {
                this.Commands.SetContentText(this.ContentText);
            }
        }

        /// <summary>
        /// フッターテキストを設定します。
        /// </summary>
        /// <param name="footerText">フッターテキスト。</param>
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
        /// メインアイコンを設定します。
        /// </summary>
        /// <param name="mainIcon">メインアイコン。</param>
        public void SetMainIcon(TaskDialogIcon mainIcon)
        {
            this.MainIcon = mainIcon;
            if (this.DialogShowing)
            {
                this.Commands.SetMainIcon(this.MainIcon);
            }
        }

        /// <summary>
        /// フッターアイコンを設定します。
        /// </summary>
        /// <param name="footerIcon">フッターアイコン。</param>
        public void SetFooterIcon(TaskDialogIcon footerIcon)
        {
            this.FooterIcon = footerIcon;
            if (this.DialogShowing)
            {
                this.Commands.SetFooterIcon(this.FooterIcon);
            }
        }

        /// <summary>
        /// 標準ボタンを設定します。
        /// </summary>
        /// <param name="commonButtons">標準ボタンフラグ。</param>
        public void SetCommonButtons(TaskDialogCommonButtons commonButtons)
        {
            this.CommonButtons = (TASKDIALOG_COMMON_BUTTON_FLAGS)commonButtons;
        }

        /// <summary>
        /// カスタムボタンのコレクションを設定します。
        /// </summary>
        /// <param name="customButtons">カスタムボタンのコレクション。</param>
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
        /// コマンドリンクのコレクションを設定します。
        /// </summary>
        /// <param name="commandLinks">コマンドリンクのコレクション。</param>
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
        /// ラジオボタンのコレクションを設定します。
        /// </summary>
        /// <param name="radioButtons">ラジオボタンのコレクション。</param>
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
        /// プログレスバーを設定します。
        /// </summary>
        /// <param name="progressBar">プログレスバー。</param>
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
        /// プログレスバーの状態を設定します。
        /// </summary>
        /// <param name="control">プログレスバー。</param>
        /// <param name="state">プログレスバー状態。</param>
        public void SetProgressBarState(TaskDialogProgressBar control, TaskDialogProgressBarState state)
        {
            this.Commands.SetProgressBarStateCommand(state);
        }

        /// <summary>
        /// プログレスバーの位置を設定します。
        /// </summary>
        /// <param name="control">プログレスバー。</param>
        /// <param name="position">プログレスバーの位置。</param>
        public void SetProgressBarPosition(TaskDialogProgressBar control, int position)
        {
            this.Commands.SetProgressBarPosCommand(position);
        }

        /// <summary>
        /// プログレスバーの範囲を設定します。
        /// </summary>
        /// <param name="control">プログレスバー。</param>
        /// <param name="minimum">プログレスバーの最小値。</param>
        /// <param name="maxmum">プログレスバーの最大値。</param>
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
        /// タスクダイアログ表示前作成イベントを処理します。
        /// </summary>
        /// <returns></returns>
        private int TaskDialogConstructedEvent()
        {
            return Ignored;
        }

        /// <summary>
        /// タスクダイアログ作成イベントを処理します。
        /// </summary>
        /// <returns></returns>
        private int TaskDialogCreatedEvent()
        {
            if (this.ProgressBar != null)
            {
                // プログレスバー設定
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
                // カスタムボタン設定
                foreach (var button in this.CustomButtons.Where(x => x.UseElevationIcon))
                {
                    this.Commands.SetButtonElevationRequiredStateCommand(button, true);
                }
            }
            else if (this.CommandLinks != null)
            {
                // コマンドリンク設定。
                foreach (var commandLink in this.CommandLinks.Where(x => x.UseElevationIcon))
                {
                    this.Commands.SetButtonElevationRequiredStateCommand(commandLink, true);
                }
            }

            // アイコン設定
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
        /// タスクダイアログボタンクリックイベントを処理します。
        /// </summary>
        /// <param name="buttonId">クリックされたボタンのID。</param>
        /// <returns></returns>
        private int TaskDialogButtonClickedEvent(int buttonId)
        {
            if (this.DialogShowStates == DialogShowStates.Showing)
            {
                // ダイアログ表示中のボタンクリック
                this.Dialog.RaiseButtonClickedEvent(buttonId);

                if (buttonId < (int)TaskDialogCommonButtonId.MinCustomControlId)
                {
                    // 標準ボタン
                    return this.Dialog.RaiseClosingEvent(buttonId) ? S_OK : S_FALSE;
                }
                else if (this.CustomButtons != null && this.CustomButtons.Any())
                {
                    // カスタムボタン
                    var button = this.CustomButtons.FirstOrDefault(x => x.Id == buttonId);
                    if (button != null && button.DialogClosable)
                    {
                        return this.Dialog.RaiseClosingEvent(buttonId) ? S_OK : S_FALSE;
                    }
                }
                else if (this.CommandLinks != null && this.CommandLinks.Any())
                {
                    // コマンドリンク
                    var control = this.CommandLinks.FirstOrDefault(x => x.Id == buttonId);
                    if (control != null)
                    {
                        return this.Dialog.RaiseClosingEvent(buttonId) ? S_OK : S_FALSE;
                    }
                }
            }
            else if (this.DialogShowStates == DialogShowStates.Closing)
            {
                // 手動で閉じるためのボタンクリック: ダイアログを閉じる。
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