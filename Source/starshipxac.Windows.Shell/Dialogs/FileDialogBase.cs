using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;
using starshipxac.Shell;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Dialogs.Controls;
using starshipxac.Windows.Shell.Dialogs.Internal;
using starshipxac.Windows.Shell.Dialogs.Interop;
using starshipxac.Windows.Shell.Properties;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    /// ファイルダイアログの基底クラスを定義します。
    /// </summary>
    [ContentProperty("Controls")]
    public abstract class FileDialogBase : IDisposable
    {
        private FileDialogInternal fileDialogInternal;
        private FileDialogEventsInternal fileDialogEventsInternal;

        private string title;
        private string okButtonText;
        private string cancelButtonText;

        /// <summary>
        /// <see cref="FileDialogBase"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        protected FileDialogBase()
        {
            this.Controls = new FileDialogControlCollection(this);
            this.DialogResult = FileDialogResult.None;
        }

        /// <summary>
        /// ダイアログのタイトルを指定して、<see cref="FileDialogBase"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="title">ダイアログのタイトル。</param>
        protected FileDialogBase(string title)
            : this()
        {
            this.Title = title;
        }

        /// <summary>
        /// ファイナライザー。
        /// </summary>
        ~FileDialogBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// <see cref="FileDialogBase"/>によって使用されているすべてのリソースを解放します。
        /// </summary>        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// <see cref="FileDialogBase"/>によって使用されているすべてのリソースを解放し、
        /// オプションでマネージリソースも解放します。
        /// </summary>
        /// <param name="disposing">
        /// マネージリソースとアンマネージリソースの両方を解放する場合は<c>true</c>。
        /// アンマネージリソースだけを解放する場合は<c>false</c>。
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.fileDialogInternal != null)
                {
                    this.fileDialogInternal.Dispose();
                }
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvaliant()
        {
            Contract.Invariant(this.Controls != null);
        }

        /// <summary>
        /// ダイアログの表示状態を取得します。
        /// </summary>
        protected DialogShowStates DialogShowStates
        {
            get
            {
                return this.FileDialogInternal.DialogShowStates;
            }
        }

        /// <summary>
        /// ダイアログが表示中かどうかを判定する値を取得します。
        /// </summary>
        public bool DialogShowing
        {
            get
            {
                return this.FileDialogInternal.DialogShowing;
            }
        }

        /// <summary>
        /// ダイアログの実行結果を取得します。
        /// </summary>
        protected FileDialogResult DialogResult { get; private set; }

        /// <summary>
        /// ダイアログのタイトルを取得または設定します。
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

                if (value == null)
                {
                    this.title = String.Empty;
                }
                else
                {
                    this.title = value;
                }
                if (this.fileDialogInternal != null)
                {
                    this.fileDialogInternal.SetTitle(this.title);
                }
            }
        }

        /// <summary>
        /// OKボタンのテキストを取得または設定します。
        /// </summary>
        public string OkButtonText
        {
            get
            {
                return this.okButtonText;
            }
            set
            {
                if (this.okButtonText == value)
                {
                    return;
                }

                if (value == null)
                {
                    this.okButtonText = String.Empty;
                }
                else
                {
                    this.okButtonText = value;
                }
                if (this.fileDialogInternal != null)
                {
                    this.fileDialogInternal.SetOkButtonText(this.okButtonText);
                }
            }
        }

        /// <summary>
        /// キャンセルボタンのテキストを取得または設定します。
        /// </summary>
        public string CancelButtonText
        {
            get
            {
                return this.cancelButtonText;
            }
            set
            {
                if (this.cancelButtonText == value)
                {
                    return;
                }

                if (value == null)
                {
                    this.cancelButtonText = String.Empty;
                }
                else
                {
                    this.cancelButtonText = value;
                }
                if (this.fileDialogInternal != null)
                {
                    this.fileDialogInternal.SetCancelButtonText(this.cancelButtonText);
                }
            }
        }

        public bool ShowPlacesList { get; set; }

        ///<summary>
        /// 隠しアイテムを表示するかどうかを判定する値を取得または設定します。
        /// </summary>
        public bool ShowHiddenItems { get; set; }

        public bool NavigateToShortcut { get; set; }

        /// <summary>
        /// 初期表示フォルダーを取得または設定します。
        /// </summary>
        public ShellFolder InitialFolder { get; set; }

        /// <summary>
        /// 規定フォルダーを取得または設定します。
        /// </summary>
        public ShellFolder DefaultFolder { get; set; }

        /// <summary>
        /// ダイアログコントロールのコレクションを取得します。
        /// </summary>
        public FileDialogControlCollection Controls { get; private set; }

        internal FileDialogInternal FileDialogInternal
        {
            get
            {
                if (this.fileDialogInternal == null)
                {
                    this.fileDialogInternal = new FileDialogInternal(this);
                }
                return this.fileDialogInternal;
            }
        }

        internal FileDialogEventsInternal FileDialogEventsInternal
        {
            get
            {
                if (this.fileDialogEventsInternal == null)
                {
                    this.fileDialogEventsInternal = new FileDialogEventsInternal(this);
                }
                return this.fileDialogEventsInternal;
            }
        }

        #region Events

        #region DialogOpening Event

        public event EventHandler DialogOpening;

        protected virtual void OnDialogOpening(EventArgs e)
        {
            var handler = Interlocked.CompareExchange(ref this.DialogOpening, null, null);
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal void RaiseDialogOpeningEvent()
        {
            OnDialogOpening(EventArgs.Empty);
        }

        #endregion

        #region FolderChanging Event

        public event EventHandler<FileDialogFolderChangeEventArgs> FolderChanging;

        protected virtual void OnFolderChanging(FileDialogFolderChangeEventArgs e)
        {
            var handler = Interlocked.CompareExchange(ref this.FolderChanging, null, null);
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal bool RaiseFolderChangingEvent(ShellFolder folder)
        {
            Contract.Requires<ArgumentNullException>(folder != null);

            var args = new FileDialogFolderChangeEventArgs(folder);
            OnFolderChanging(args);
            return !args.Cancel;
        }

        #endregion

        #region FolderChanged Event

        public event EventHandler FolderChanged;

        protected virtual void OnFolderChanged(EventArgs e)
        {
            var handler = Interlocked.CompareExchange(ref this.FolderChanged, null, null);
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal void RaiseFolderChangedEvent()
        {
            OnFolderChanged(EventArgs.Empty);
        }

        #endregion

        #region SelectionChanged Event

        public event EventHandler SelectionChanged;

        protected virtual void OnSelectionChanged(EventArgs e)
        {
            var handler = Interlocked.CompareExchange(ref this.SelectionChanged, null, null);
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal void RaiseSelectionChangedEvent()
        {
            OnSelectionChanged(EventArgs.Empty);
        }

        #endregion

        #region FileTypeChanged Event

        public event EventHandler FileTypeChanged;

        protected virtual void OnFileTypeChanged(EventArgs e)
        {
            var handler = Interlocked.CompareExchange(ref this.FileTypeChanged, null, null);
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal void RaiseFileTypeChangedEvent()
        {
            OnFileTypeChanged(EventArgs.Empty);
        }

        #endregion

        #region Committed Event

        public event CancelEventHandler Committed;

        protected virtual void OnCommitted(CancelEventArgs e)
        {
            var handler = Interlocked.CompareExchange(ref this.Committed, null, null);
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal bool RaiseCommittedEvent()
        {
            var args = new CancelEventArgs();
            OnCommitted(args);

            if (!args.Cancel)
            {
                foreach (var control in this.Controls)
                {
                    control.Detach();
                }
            }

            return !args.Cancel;
        }

        #endregion

        #endregion

        public void AddPlace(ShellFolder place, FileDialogAddPlaceLocation location)
        {
            Contract.Requires<ArgumentNullException>(place != null);

            this.FileDialogInternal.AddPlace(place, location);
        }

        public void AddPlace(string path, FileDialogAddPlaceLocation location)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(path));

            this.FileDialogInternal.AddPlace(path, location);
        }

        /// <summary>
        /// ダイアログを表示します。
        /// </summary>
        /// <returns>ダイアログの実行結果。</returns>
        protected FileDialogResult ShowDialog()
        {
            var parentWindowHandle = IntPtr.Zero;
            if (Application.Current != null && Application.Current.MainWindow != null)
            {
                parentWindowHandle = (new WindowInteropHelper(Application.Current.MainWindow)).Handle;
            }

            return ShowDialog(parentWindowHandle);
        }

        /// <summary>
        /// 親ウィンドウを指定して、ダイアログを表示します。
        /// </summary>
        /// <param name="window">親ウィンドウ。</param>
        /// <returns>ダイアログの実行結果。</returns>
        protected FileDialogResult ShowDialog(Window window)
        {
            Contract.Requires<ArgumentNullException>(window != null);

            var windowInteropHelper = new WindowInteropHelper(window);
            return ShowDialog(windowInteropHelper.Handle);
        }

        /// <summary>
        /// 親ウィンドウのハンドルを指定して、ダイアログを表示します。
        /// </summary>
        /// <param name="parentWindowHandle">親ウィンドウのハンドル。</param>
        /// <returns>ダイアログの実行結果。</returns>
        private FileDialogResult ShowDialog(IntPtr parentWindowHandle)
        {
            // ダイアログ作成
            this.FileDialogInternal.SetOptions(GetDialogOptions());
            SetNativeSettings();
            SetNativeEvents();

            // ダイアログ表示
            var hresult = this.FileDialogInternal.ShowDialog(parentWindowHandle);
            if (hresult == COMErrorCodes.Cancelled)
            {
                this.DialogResult = FileDialogResult.Cancel;
            }
            else
            {
                this.DialogResult = FileDialogResult.Ok;
            }

            return this.DialogResult;
        }

        /// <summary>
        /// <see cref="IFileDialog2"/>を作成します。
        /// </summary>
        /// <returns></returns>
        internal abstract IFileDialog2 CreateNativeFileDialog();

        /// <summary>
        /// <see cref="FileDialogOptions"/>を取得します。
        /// </summary>
        /// <returns></returns>
        protected virtual FileDialogOptions GetDialogOptions()
        {
            var result = FileDialogOptions.None;

            if (this.ShowPlacesList)
            {
                result |= FileDialogOptions.ShowPlacesList;
            }

            if (this.ShowHiddenItems)
            {
                result |= FileDialogOptions.ShowHiddenItems;
            }

            if (this.NavigateToShortcut)
            {
                result |= FileDialogOptions.NavigateToShortcut;
            }

            return result;
        }

        /// <summary>
        /// ネイティブダイアログに設定を適用します。
        /// </summary>
        protected virtual void SetNativeSettings()
        {
            if (this.Title != null)
            {
                this.FileDialogInternal.SetTitle(this.Title);
            }

            if (this.OkButtonText != null)
            {
                this.FileDialogInternal.SetOkButtonText(this.OkButtonText);
            }

            if (this.CancelButtonText != null)
            {
                this.FileDialogInternal.SetCancelButtonText(this.CancelButtonText);
            }

            if (this.InitialFolder != null)
            {
                this.FileDialogInternal.SetInitialFolder(this.InitialFolder);
            }

            if (this.DefaultFolder != null)
            {
                this.FileDialogInternal.SetDefaultFolder(this.DefaultFolder);
            }
        }

        /// <summary>
        /// ネイティブダイアログにイベントを設定します。
        /// </summary>
        private void SetNativeEvents()
        {
            if (this.DialogOpening != null ||
                this.Committed != null ||
                this.FolderChanging != null ||
                this.FolderChanged != null ||
                this.SelectionChanged != null ||
                this.FileTypeChanged != null ||
                (this.Controls != null && this.Controls.Any()))
            {
                this.FileDialogInternal.Advise(this.FileDialogEventsInternal);
            }
        }

        #region Control Methods

        internal void AddButton(FileDialogButton control)
        {
            this.FileDialogInternal.AddButton(control.Id, control.Text);
        }

        internal void AddCheckBox(FileDialogCheckBox control)
        {
            this.FileDialogInternal.AddCheckBox(control.Id, control.Text, control.IsChecked);
        }

        internal void AddComboBox(FileDialogComboBox control)
        {
            this.FileDialogInternal.AddComboBox(control.Id);
        }

        internal void AddControlItem(FileDialogControl control, int itemId, string label)
        {
            this.FileDialogInternal.AddControlItem(control.Id, itemId, label);
        }

        internal void AddEditBox(FileDialogTextBox control, string text)
        {
            this.FileDialogInternal.AddEditBox(control.Id, text);
        }

        internal void AddLabel(FileDialogLabel control, string label)
        {
            this.FileDialogInternal.AddText(control.Id, label);
        }

        internal void AddMenu(FileDialogMenu control, string label)
        {
            this.FileDialogInternal.AddMenu(control.Id, label);
        }

        internal void AddMenuItem(FileDialogMenu control, FileDialogMenuItem menuItem, string label)
        {
            this.FileDialogInternal.AddControlItem(control.Id, menuItem.Id, label);
        }

        internal void AddRadioButtonList(FileDialogRadioButtonList control)
        {
            this.FileDialogInternal.AddRadioButtonList(control.Id);
        }

        internal void AddSeparator(FileDialogSeparator control)
        {
            this.FileDialogInternal.AddSeparator(control.Id);
        }

        internal void StartVisualGroup(FileDialogGroupBox control, string label)
        {
            this.FileDialogInternal.StartVisualGroup(control.Id, label);
        }

        internal void EndVisualGroup()
        {
            this.FileDialogInternal.EndVisualGroup();
        }

        internal void SetDefaultControl(FileDialogControl control)
        {
            this.FileDialogInternal.SetDefaultControl(control.Id);
        }

        internal bool GetControlEnabled(FileDialogControl control)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            var state = this.FileDialogInternal.GetControlState(control.Id);
            return (state & CDCONTROLSTATEF.CDCS_ENABLED) != 0;
        }

        internal void SetControlEnabled(FileDialogControl control, bool enabled)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            var state = this.FileDialogInternal.GetControlState(control.Id);

            if (enabled)
            {
                state |= CDCONTROLSTATEF.CDCS_ENABLED;
            }
            else
            {
                state &= ~CDCONTROLSTATEF.CDCS_ENABLED;
            }

            this.FileDialogInternal.SetControlState(control.Id, state);
        }

        internal bool GetControlVisible(FileDialogControl control)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            var state = this.FileDialogInternal.GetControlState(control.Id);
            return (state & CDCONTROLSTATEF.CDCS_VISIBLE) != 0;
        }

        internal void SetControlVisible(FileDialogControl control, bool visible)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            var state = this.FileDialogInternal.GetControlState(control.Id);

            if (visible)
            {
                state |= CDCONTROLSTATEF.CDCS_VISIBLE;
            }
            else
            {
                state &= ~CDCONTROLSTATEF.CDCS_VISIBLE;
            }

            this.FileDialogInternal.SetControlState(control.Id, state);
        }

        internal void SetControlLabel(FileDialogControl control, string label)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            this.FileDialogInternal.SetControlLabel(control.Id, label);
        }

        internal bool GetCheckBoxChecked(FileDialogCheckBox control)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            return this.FileDialogInternal.GetCheckBoxChecked(control.Id);
        }

        internal void SetCheckBoxChecked(FileDialogCheckBox control, bool value)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            this.FileDialogInternal.SetCheckBoxChecked(control.Id, value);
        }

        internal string GetEditBoxText(FileDialogTextBox control)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            return this.FileDialogInternal.GetEditBoxText(control.Id);
        }

        internal void SetEditBoxText(FileDialogTextBox control, string text)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            this.FileDialogInternal.SetEditBoxText(control.Id, text);
        }

        internal int GetControlSelectedIndex(FileDialogControl control)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            return this.FileDialogInternal.GetSelectedControlItem(control.Id);
        }

        internal void SetControlSelectedIndex(FileDialogControl control, int index)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            this.FileDialogInternal.SetSelectedControlItem(control.Id, index);
        }

        #endregion

        protected void ThrowIfDialogShowing(string message)
        {
            if (this.DialogShowing)
            {
                throw new InvalidOperationException(message);
            }
        }

        protected void ThrowIfDialogShowingPropertyCannotBeChanged([CallerMemberName] string propertyName = "")
        {
            if (this.DialogShowing)
            {
                throw new InvalidOperationException(String.Format(ErrorMessages.PropertyChannotBeChanged, propertyName));
            }
        }
    }
}