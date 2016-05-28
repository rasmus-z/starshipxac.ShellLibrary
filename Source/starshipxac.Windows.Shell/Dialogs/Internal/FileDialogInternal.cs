using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using starshipxac.Shell;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs.Internal
{
    internal sealed class FileDialogInternal : IDisposable
    {
        private bool diposed = false;
        private IFileDialog2 fileDialogNative;

        public FileDialogInternal(FileDialogBase dialog)
        {
            Contract.Requires<ArgumentNullException>(dialog != null);

            this.Dialog = dialog;
            this.DialogShowStates = DialogShowStates.PreShow;
        }

        ~FileDialogInternal()
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
            if (!this.diposed)
            {
                if (disposing)
                {
                    if (this.fileDialogNative != null)
                    {
                        Marshal.ReleaseComObject(this.fileDialogNative);
                    }
                }

                this.diposed = true;
            }
        }

        public FileDialogBase Dialog { get; }

        internal IFileDialog2 FileDialogNative
        {
            get
            {
                if (this.fileDialogNative == null)
                {
                    this.fileDialogNative = this.Dialog.CreateNativeFileDialog();
                }
                return this.fileDialogNative;
            }
        }

        private IFileDialogCustomize FileDialogCustomizeNative
        {
            get
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                return (IFileDialogCustomize)this.FileDialogNative;
            }
        }

        public DialogShowStates DialogShowStates { get; internal set; }

        public bool DialogShowing
        {
            get
            {
                return (this.FileDialogNative != null) &&
                       (this.DialogShowStates == DialogShowStates.Showing || this.DialogShowStates == DialogShowStates.Closing);
            }
        }

        public uint Cookie { get; private set; }

        /// <summary>
        ///     ダイアログを表示します。
        /// </summary>
        /// <param name="parentWindowHandle">親ウィンドウのハンドル。</param>
        /// <returns>ダイアログ実行結果。</returns>
        public HRESULT ShowDialog(IntPtr parentWindowHandle)
        {
            this.DialogShowStates = DialogShowStates.Showing;
            var result = (HRESULT)this.FileDialogNative.Show(parentWindowHandle);
            this.DialogShowStates = DialogShowStates.Closed;

            return result;
        }

        /// <summary>
        ///     ファイルダイアログイベントを設定します。
        /// </summary>
        /// <param name="fileDialogEvents"></param>
        public void Advise(FileDialogEventsInternal fileDialogEvents)
        {
            Contract.Requires<ArgumentNullException>(fileDialogEvents != null);

            uint cookie;
            this.FileDialogNative.Advise(fileDialogEvents, out cookie);
            this.Cookie = cookie;
        }

        /// <summary>
        ///     ファイルダイアログオプションを設定します。
        /// </summary>
        /// <param name="fileDialogOptions"></param>
        public void SetOptions(FileDialogOptions fileDialogOptions)
        {
            var options = FILEOPENDIALOGOPTIONS.FOS_NOTESTFILECREATE | (FILEOPENDIALOGOPTIONS)fileDialogOptions;
            this.FileDialogNative.SetOptions(options);
        }

        /// <summary>
        ///     ダイアログのタイトルを設定します。
        /// </summary>
        /// <param name="title"></param>
        public void SetTitle(string title)
        {
            Contract.Requires<ArgumentNullException>(title != null);

            this.FileDialogNative.SetTitle(title);
        }

        /// <summary>
        ///     OKボタンのテキストを設定します。
        /// </summary>
        /// <param name="label"></param>
        public void SetOkButtonText(string label)
        {
            Contract.Requires<ArgumentNullException>(label != null);

            this.FileDialogNative.SetOkButtonLabel(label);
        }

        /// <summary>
        ///     キャンセルボタンのテキストを設定します。
        /// </summary>
        /// <param name="label"></param>
        public void SetCancelButtonText(string label)
        {
            Contract.Requires<ArgumentNullException>(label != null);

            this.FileDialogNative.SetCancelButtonLabel(label);
        }

        /// <summary>
        ///     デフォルトのファイル名を設定します。
        /// </summary>
        /// <param name="filename"></param>
        public void SetDefaultFileName(string filename)
        {
            Contract.Requires<ArgumentNullException>(filename != null);

            this.FileDialogNative.SetFileName(filename);
        }

        /// <summary>
        ///     デフォルトの拡張子を設定します。
        /// </summary>
        /// <param name="extension"></param>
        public void SetDefaultExtension(string extension)
        {
            Contract.Requires<ArgumentNullException>(extension != null);

            this.FileDialogNative.SetDefaultExtension(extension);
        }

        /// <summary>
        ///     初期フォルダーを設定します。
        /// </summary>
        /// <param name="folder"></param>
        public void SetInitialFolder(ShellFolder folder)
        {
            Contract.Requires<ArgumentNullException>(folder != null);

            this.FileDialogNative.SetFolder(folder.ShellItem.ShellItemInterface);
        }

        /// <summary>
        ///     デフォルトフォルダーを設定します。
        /// </summary>
        /// <param name="folder"></param>
        public void SetDefaultFolder(ShellFolder folder)
        {
            Contract.Requires<ArgumentNullException>(folder != null);

            this.FileDialogNative.SetDefaultFolder(folder.ShellItem.ShellItemInterface);
        }

        public void SetClientId(Guid cliendId)
        {
            this.FileDialogNative.SetClientGuid(cliendId);
        }

        public void SetFilters(FileTypeFilterCollection filters)
        {
            Contract.Requires<ArgumentNullException>(filters != null);

            this.FileDialogNative.SetFileTypes((uint)filters.Count, filters.EnumerateFilterSpecs().ToArray());
        }

        public void SetFilterIndex(int index)
        {
            Contract.Requires<ArgumentOutOfRangeException>(index >= 1);

            this.FileDialogNative.SetFileTypeIndex((UInt32)index);
        }

        public void AddPlace(ShellFolder place, FileDialogAddPlaceLocation location)
        {
            this.FileDialogNative?.AddPlace(place.ShellItem.ShellItemInterface, (FDAP)location);
        }

        public void AddPlace(string path, FileDialogAddPlaceLocation location)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(path));

            if (this.FileDialogNative != null)
            {
                var shellItem = ShellItem.FromParsingName(path);
                this.FileDialogNative.AddPlace(shellItem.ShellItemInterface, (FDAP)location);
            }
        }

        public void AddButton(int id, string label)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);
            Contract.Requires<ArgumentNullException>(label != null);

            this.FileDialogCustomizeNative.AddPushButton((UInt32)id, label);
        }

        public void AddCheckBox(int id, string label, bool isChecked)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);
            Contract.Requires<ArgumentNullException>(label != null);

            this.FileDialogCustomizeNative.AddCheckButton((UInt32)id, label, isChecked);
        }

        public void AddComboBox(int id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);

            this.FileDialogCustomizeNative.AddComboBox((UInt32)id);
        }

        public void AddEditBox(int id, string text)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);
            Contract.Requires<ArgumentNullException>(text != null);

            this.FileDialogCustomizeNative.AddEditBox((UInt32)id, text);
        }

        public void AddMenu(int id, string label)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);
            Contract.Requires<ArgumentNullException>(label != null);

            this.FileDialogCustomizeNative.AddMenu((UInt32)id, label);
        }

        public void AddRadioButtonList(int id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);

            this.FileDialogCustomizeNative.AddRadioButtonList((UInt32)id);
        }

        public void AddSeparator(int id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);

            this.FileDialogCustomizeNative.AddSeparator((UInt32)id);
        }

        public void AddText(int id, string label)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);
            Contract.Requires<ArgumentNullException>(label != null);

            this.FileDialogCustomizeNative.AddText((UInt32)id, label);
        }

        public void AddControlItem(int id, int itemId, string label)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(itemId >= 0);
            Contract.Requires<ArgumentNullException>(label != null);

            this.FileDialogCustomizeNative.AddControlItem((UInt32)id, (UInt32)itemId, label);
        }

        public void StartVisualGroup(int id, string label)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);
            Contract.Requires<ArgumentNullException>(label != null);

            this.FileDialogCustomizeNative.StartVisualGroup((UInt32)id, label);
        }

        public void EndVisualGroup()
        {
            this.FileDialogCustomizeNative.EndVisualGroup();
        }

        public void SetDefaultControl(int id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);

            this.FileDialogCustomizeNative.MakeProminent((UInt32)id);
        }

        public CDCONTROLSTATEF GetControlState(int id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);

            CDCONTROLSTATEF result;
            this.FileDialogCustomizeNative.GetControlState((UInt32)id, out result);
            return result;
        }

        public void SetControlState(int id, CDCONTROLSTATEF state)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);

            this.FileDialogCustomizeNative.SetControlState((UInt32)id, state);
        }

        public void SetControlLabel(int id, string label)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);
            Contract.Requires<ArgumentNullException>(label != null);

            this.FileDialogCustomizeNative.SetControlLabel((UInt32)id, label);
        }

        public bool GetCheckBoxChecked(int id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);

            bool result;
            this.FileDialogCustomizeNative.GetCheckButtonState((UInt32)id, out result);
            return result;
        }

        public void SetCheckBoxChecked(int id, bool isChecked)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);

            this.FileDialogCustomizeNative.SetCheckButtonState((UInt32)id, isChecked);
        }

        public string GetEditBoxText(int id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);

            string result;
            this.FileDialogCustomizeNative.GetEditBoxText((UInt32)id, out result);
            return result;
        }

        public void SetEditBoxText(int id, string value)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);
            Contract.Requires<ArgumentNullException>(value != null);

            this.FileDialogCustomizeNative.SetEditBoxText((UInt32)id, value);
        }

        public int GetSelectedControlItem(int id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);

            uint result;
            this.FileDialogCustomizeNative.GetSelectedControlItem((UInt32)id, out result);
            return (int)result;
        }

        public void SetSelectedControlItem(int id, int selectedIndex)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(selectedIndex >= 0);

            this.FileDialogCustomizeNative.SetSelectedControlItem((UInt32)id, (UInt32)selectedIndex);
        }
    }
}