using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
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
    ///     Define file dialog base class.
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
        ///     Initialize a new instance of the <see cref="FileDialogBase" /> class.
        /// </summary>
        protected FileDialogBase()
        {
            this.Controls = new FileDialogControlCollection(this);
            this.DialogResult = FileDialogResult.None;
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="FileDialogBase" /> class
        ///     to the specified dialog title.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        protected FileDialogBase(string title)
            : this()
        {
            this.Title = title;
        }

        /// <summary>
        ///     Finalizer.
        /// </summary>
        ~FileDialogBase()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Release all resources used by <see cref="FileDialogBase" />.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Release all resources used by <see cref="FileDialogBase" />,
        ///     and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources.
        ///     <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.fileDialogInternal?.Dispose();
            }
        }

        /// <summary>
        ///     Get the dialog show states.
        /// </summary>
        protected DialogShowStates DialogShowStates => this.FileDialogInternal.DialogShowStates;

        /// <summary>
        ///     Get a value that determines whether a dialog is being displayed.
        /// </summary>
        public bool DialogShowing => this.FileDialogInternal.DialogShowing;

        /// <summary>
        ///     Get the execution result of the dialog.
        /// </summary>
        protected FileDialogResult DialogResult { get; private set; }

        /// <summary>
        ///     Get and set the dialog title.
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
                this.fileDialogInternal?.SetTitle(this.title);
            }
        }

        /// <summary>
        ///     Get or set the OK button text.
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

                this.okButtonText = value ?? String.Empty;
                this.fileDialogInternal?.SetOkButtonText(this.okButtonText);
            }
        }

        /// <summary>
        ///     Get or set the cancel button text.
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

                this.cancelButtonText = value ?? String.Empty;
                this.fileDialogInternal?.SetCancelButtonText(this.cancelButtonText);
            }
        }

        /// <summary>
        ///     Get a value that determines whether to display the places list.
        /// </summary>
        public bool ShowPlacesList { get; set; }

        /// <summary>
        ///     Get or set a value that determines whether to display hidden items.
        /// </summary>
        public bool ShowHiddenItems { get; set; }

        /// <summary>
        ///     Get or set a value that determines whether to navigate to shortcut.
        /// </summary>
        public bool NavigateToShortcut { get; set; }

        /// <summary>
        ///     Get or set initial folder.
        /// </summary>
        public ShellFolder InitialFolder { get; set; }

        /// <summary>
        ///     Get or set default folder.
        /// </summary>
        public ShellFolder DefaultFolder { get; set; }

        /// <summary>
        ///     Get or set dialog control collection.
        /// </summary>
        public FileDialogControlCollection Controls { get; }

        /// <summary>
        ///     Get the <see cref="FileDialogInternal" />.
        /// </summary>
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

        /// <summary>
        ///     Get the <see cref="FileDialogEventsInternal" />.
        /// </summary>
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

        /// <summary>
        ///     Dialog opening event.
        /// </summary>
        public event EventHandler DialogOpening;

        protected virtual void OnDialogOpening(EventArgs e)
        {
            this.DialogOpening?.Invoke(this, e);
        }

        internal void RaiseDialogOpeningEvent()
        {
            OnDialogOpening(EventArgs.Empty);
        }

        #endregion

        #region FolderChanging Event

        /// <summary>
        ///     Folder changing event.
        /// </summary>
        public event EventHandler<FileDialogFolderChangeEventArgs> FolderChanging;

        protected virtual void OnFolderChanging(FileDialogFolderChangeEventArgs e)
        {
            this.FolderChanging?.Invoke(this, e);
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

        /// <summary>
        ///     Folder changed event.
        /// </summary>
        public event EventHandler FolderChanged;

        protected virtual void OnFolderChanged(EventArgs e)
        {
            this.FolderChanged?.Invoke(this, e);
        }

        internal void RaiseFolderChangedEvent()
        {
            OnFolderChanged(EventArgs.Empty);
        }

        #endregion

        #region SelectionChanged Event

        /// <summary>
        ///     Selection changed event.
        /// </summary>
        public event EventHandler SelectionChanged;

        protected virtual void OnSelectionChanged(EventArgs e)
        {
            this.SelectionChanged?.Invoke(this, e);
        }

        internal void RaiseSelectionChangedEvent()
        {
            OnSelectionChanged(EventArgs.Empty);
        }

        #endregion

        #region FileTypeChanged Event

        /// <summary>
        ///     File type changed event.
        /// </summary>
        public event EventHandler FileTypeChanged;

        protected virtual void OnFileTypeChanged(EventArgs e)
        {
            this.FileTypeChanged?.Invoke(this, e);
        }

        internal void RaiseFileTypeChangedEvent()
        {
            OnFileTypeChanged(EventArgs.Empty);
        }

        #endregion

        #region Committed Event

        /// <summary>
        ///     Committed event.
        /// </summary>
        public event CancelEventHandler Committed;

        protected virtual void OnCommitted(CancelEventArgs e)
        {
            this.Committed?.Invoke(this, e);
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

        /// <summary>
        ///     Add a folder to the specified location.
        /// </summary>
        /// <param name="place"></param>
        /// <param name="location"></param>
        public void AddPlace(ShellFolder place, FileDialogAddPlaceLocation location)
        {
            Contract.Requires<ArgumentNullException>(place != null);

            this.FileDialogInternal.AddPlace(place, location);
        }

        /// <summary>
        ///     Add a path to the specified location.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="location"></param>
        public void AddPlace(string path, FileDialogAddPlaceLocation location)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(path));

            this.FileDialogInternal.AddPlace(path, location);
        }

        /// <summary>
        ///     Show dialog.
        /// </summary>
        /// <returns>Result of dialog execution.</returns>
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
        ///     Show dialog.
        /// </summary>
        /// <param name="window">Parent window.</param>
        /// <returns>Result of dialog execution.</returns>
        protected FileDialogResult ShowDialog(Window window)
        {
            Contract.Requires<ArgumentNullException>(window != null);

            var windowInteropHelper = new WindowInteropHelper(window);
            return ShowDialog(windowInteropHelper.Handle);
        }

        /// <summary>
        ///     Show dialog.
        /// </summary>
        /// <param name="parentWindowHandle">Parent window handle.</param>
        /// <returns>Result of dialog execution.</returns>
        private FileDialogResult ShowDialog(IntPtr parentWindowHandle)
        {
            // Create dialog.
            this.FileDialogInternal.SetOptions(GetDialogOptions());
            SetNativeSettings();
            SetNativeEvents();

            // Show dialog.
            var hresult = this.FileDialogInternal.ShowDialog(parentWindowHandle);
            this.DialogResult = (hresult == COMErrorCodes.Cancelled) ? FileDialogResult.Cancel : FileDialogResult.Ok;

            return this.DialogResult;
        }

        /// <summary>
        ///     Create <see cref="IFileDialog2" />.
        /// </summary>
        /// <returns></returns>
        internal abstract IFileDialog2 CreateNativeFileDialog();

        /// <summary>
        ///     Get the <see cref="FileDialogOptions" />.
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
        ///     Set the setting to the native dialog.
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
        ///     Set the event in the native dialog.
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
            Contract.Requires(control != null);
            this.FileDialogInternal.AddButton(control.Id, control.Text);
        }

        internal void AddCheckBox(FileDialogCheckBox control)
        {
            Contract.Requires(control != null);
            this.FileDialogInternal.AddCheckBox(control.Id, control.Text, control.IsChecked);
        }

        internal void AddComboBox(FileDialogComboBox control)
        {
            Contract.Requires(control != null);
            this.FileDialogInternal.AddComboBox(control.Id);
        }

        internal void AddControlItem(FileDialogControl control, int itemId, string label)
        {
            Contract.Requires(control != null);
            Contract.Requires(0 <= itemId);
            this.FileDialogInternal.AddControlItem(control.Id, itemId, label);
        }

        internal void AddEditBox(FileDialogTextBox control, string text)
        {
            Contract.Requires(control != null);
            this.FileDialogInternal.AddEditBox(control.Id, text);
        }

        internal void AddLabel(FileDialogLabel control, string label)
        {
            Contract.Requires(control != null);
            this.FileDialogInternal.AddText(control.Id, label);
        }

        internal void AddMenu(FileDialogMenu control, string label)
        {
            Contract.Requires(control != null);
            this.FileDialogInternal.AddMenu(control.Id, label);
        }

        internal void AddMenuItem(FileDialogMenu control, FileDialogMenuItem menuItem, string label)
        {
            Contract.Requires(control != null);
            Contract.Requires(menuItem != null);

            this.FileDialogInternal.AddControlItem(control.Id, menuItem.Id, label);
        }

        internal void AddRadioButtonList(FileDialogRadioButtonList control)
        {
            Contract.Requires(control != null);
            this.FileDialogInternal.AddRadioButtonList(control.Id);
        }

        internal void AddSeparator(FileDialogSeparator control)
        {
            Contract.Requires(control != null);
            this.FileDialogInternal.AddSeparator(control.Id);
        }

        internal void StartVisualGroup(FileDialogGroupBox control, string label)
        {
            Contract.Requires(control != null);
            this.FileDialogInternal.StartVisualGroup(control.Id, label);
        }

        internal void EndVisualGroup()
        {
            this.FileDialogInternal.EndVisualGroup();
        }

        internal void SetDefaultControl(FileDialogControl control)
        {
            Contract.Requires(control != null);
            this.FileDialogInternal.SetDefaultControl(control.Id);
        }

        internal bool GetControlEnabled(FileDialogControl control)
        {
            Contract.Requires(control != null);

            var state = this.FileDialogInternal.GetControlState(control.Id);
            return (state & CDCONTROLSTATEF.CDCS_ENABLED) != 0;
        }

        internal void SetControlEnabled(FileDialogControl control, bool enabled)
        {
            Contract.Requires(control != null);

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
            Contract.Requires(control != null);

            var state = this.FileDialogInternal.GetControlState(control.Id);
            return (state & CDCONTROLSTATEF.CDCS_VISIBLE) != 0;
        }

        internal void SetControlVisible(FileDialogControl control, bool visible)
        {
            Contract.Requires(control != null);

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
            Contract.Requires(control != null);

            this.FileDialogInternal.SetControlLabel(control.Id, label);
        }

        internal bool GetCheckBoxChecked(FileDialogCheckBox control)
        {
            Contract.Requires(control != null);

            return this.FileDialogInternal.GetCheckBoxChecked(control.Id);
        }

        internal void SetCheckBoxChecked(FileDialogCheckBox control, bool value)
        {
            Contract.Requires(control != null);

            this.FileDialogInternal.SetCheckBoxChecked(control.Id, value);
        }

        internal string GetEditBoxText(FileDialogTextBox control)
        {
            Contract.Requires(control != null);

            return this.FileDialogInternal.GetEditBoxText(control.Id);
        }

        internal void SetEditBoxText(FileDialogTextBox control, string text)
        {
            Contract.Requires(control != null);

            this.FileDialogInternal.SetEditBoxText(control.Id, text);
        }

        internal int GetControlSelectedIndex(FileDialogControl control)
        {
            Contract.Requires(control != null);

            return this.FileDialogInternal.GetSelectedControlItem(control.Id);
        }

        internal void SetControlSelectedIndex(FileDialogControl control, int index)
        {
            Contract.Requires(control != null);
            Contract.Requires(0 <= index);

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