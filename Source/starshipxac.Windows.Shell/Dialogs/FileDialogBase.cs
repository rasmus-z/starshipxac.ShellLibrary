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
    /// �t�@�C���_�C�A���O�̊��N���X���`���܂��B
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
        /// <see cref="FileDialogBase"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        protected FileDialogBase()
        {
            this.Controls = new FileDialogControlCollection(this);
            this.DialogResult = FileDialogResult.None;
        }

        /// <summary>
        /// �_�C�A���O�̃^�C�g�����w�肵�āA<see cref="FileDialogBase"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="title">�_�C�A���O�̃^�C�g���B</param>
        protected FileDialogBase(string title)
            : this()
        {
            this.Title = title;
        }

        /// <summary>
        /// �t�@�C�i���C�U�[�B
        /// </summary>
        ~FileDialogBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// <see cref="FileDialogBase"/>�ɂ���Ďg�p����Ă��邷�ׂẴ��\�[�X��������܂��B
        /// </summary>        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// <see cref="FileDialogBase"/>�ɂ���Ďg�p����Ă��邷�ׂẴ��\�[�X��������A
        /// �I�v�V�����Ń}�l�[�W���\�[�X��������܂��B
        /// </summary>
        /// <param name="disposing">
        /// �}�l�[�W���\�[�X�ƃA���}�l�[�W���\�[�X�̗������������ꍇ��<c>true</c>�B
        /// �A���}�l�[�W���\�[�X�������������ꍇ��<c>false</c>�B
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
        /// �_�C�A���O�̕\����Ԃ��擾���܂��B
        /// </summary>
        protected DialogShowStates DialogShowStates
        {
            get
            {
                return this.FileDialogInternal.DialogShowStates;
            }
        }

        /// <summary>
        /// �_�C�A���O���\�������ǂ����𔻒肷��l���擾���܂��B
        /// </summary>
        public bool DialogShowing
        {
            get
            {
                return this.FileDialogInternal.DialogShowing;
            }
        }

        /// <summary>
        /// �_�C�A���O�̎��s���ʂ��擾���܂��B
        /// </summary>
        protected FileDialogResult DialogResult { get; private set; }

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
        /// OK�{�^���̃e�L�X�g���擾�܂��͐ݒ肵�܂��B
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
        /// �L�����Z���{�^���̃e�L�X�g���擾�܂��͐ݒ肵�܂��B
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
        /// �B���A�C�e����\�����邩�ǂ����𔻒肷��l���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public bool ShowHiddenItems { get; set; }

        public bool NavigateToShortcut { get; set; }

        /// <summary>
        /// �����\���t�H���_�[���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public ShellFolder InitialFolder { get; set; }

        /// <summary>
        /// �K��t�H���_�[���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public ShellFolder DefaultFolder { get; set; }

        /// <summary>
        /// �_�C�A���O�R���g���[���̃R���N�V�������擾���܂��B
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
        /// �_�C�A���O��\�����܂��B
        /// </summary>
        /// <returns>�_�C�A���O�̎��s���ʁB</returns>
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
        /// �e�E�B���h�E���w�肵�āA�_�C�A���O��\�����܂��B
        /// </summary>
        /// <param name="window">�e�E�B���h�E�B</param>
        /// <returns>�_�C�A���O�̎��s���ʁB</returns>
        protected FileDialogResult ShowDialog(Window window)
        {
            Contract.Requires<ArgumentNullException>(window != null);

            var windowInteropHelper = new WindowInteropHelper(window);
            return ShowDialog(windowInteropHelper.Handle);
        }

        /// <summary>
        /// �e�E�B���h�E�̃n���h�����w�肵�āA�_�C�A���O��\�����܂��B
        /// </summary>
        /// <param name="parentWindowHandle">�e�E�B���h�E�̃n���h���B</param>
        /// <returns>�_�C�A���O�̎��s���ʁB</returns>
        private FileDialogResult ShowDialog(IntPtr parentWindowHandle)
        {
            // �_�C�A���O�쐬
            this.FileDialogInternal.SetOptions(GetDialogOptions());
            SetNativeSettings();
            SetNativeEvents();

            // �_�C�A���O�\��
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
        /// <see cref="IFileDialog2"/>���쐬���܂��B
        /// </summary>
        /// <returns></returns>
        internal abstract IFileDialog2 CreateNativeFileDialog();

        /// <summary>
        /// <see cref="FileDialogOptions"/>���擾���܂��B
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
        /// �l�C�e�B�u�_�C�A���O�ɐݒ��K�p���܂��B
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
        /// �l�C�e�B�u�_�C�A���O�ɃC�x���g��ݒ肵�܂��B
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