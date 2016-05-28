using System;
using System.Diagnostics.Contracts;
using System.Linq;
using starshipxac.Shell;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Dialogs.Controls;
using starshipxac.Windows.Shell.Dialogs.Interop;
// ReSharper disable InconsistentNaming

namespace starshipxac.Windows.Shell.Dialogs.Internal
{
    internal class FileDialogEventsInternal : IFileDialogEvents, IFileDialogControlEvents
    {
        private bool firstFolderChanged = true;

        public FileDialogEventsInternal(FileDialogBase dialog)
        {
            Contract.Requires<ArgumentNullException>(dialog != null);

            this.Dialog = dialog;
        }

        public FileDialogBase Dialog { get; }

        public HRESULT OnFileOk(IFileDialog pfd)
        {
            var committed = this.Dialog.RaiseCommittedEvent();

            return committed ? COMErrorCodes.S_OK : COMErrorCodes.S_FALSE;
        }

        public HRESULT OnFolderChanging(IFileDialog pfd, IShellItem psiFolder)
        {
            var shellFolder = ShellFactory.FromShellItem(new ShellItem(psiFolder)) as ShellFolder;

            var change = true;
            if (!this.firstFolderChanged)
            {
                change = this.Dialog.RaiseFolderChangingEvent(shellFolder);
            }

            return change ? COMErrorCodes.S_OK : COMErrorCodes.S_FALSE;
        }

        public void OnFolderChange(IFileDialog pfd)
        {
            if (this.firstFolderChanged)
            {
                this.firstFolderChanged = false;
                this.Dialog.RaiseDialogOpeningEvent();
            }
            else
            {
                this.Dialog.RaiseFolderChangedEvent();
            }
        }

        public void OnSelectionChange(IFileDialog pfd)
        {
            this.Dialog.RaiseSelectionChangedEvent();
        }

        public void OnShareViolation(IFileDialog pfd, IShellItem psi, out FDE_SHAREVIOLATION_RESPONSE pResponse)
        {
            pResponse = FDE_SHAREVIOLATION_RESPONSE.FDESVR_ACCEPT;
        }

        public void OnTypeChange(IFileDialog pfd)
        {
            this.Dialog.RaiseFileTypeChangedEvent();
        }

        public void OnOverwrite(IFileDialog pfd, IShellItem psi, out FDE_OVERWRITE_RESPONSE pResponse)
        {
            pResponse = FDE_OVERWRITE_RESPONSE.FDEOR_DEFAULT;
        }

        public void OnItemSelected(IFileDialogCustomize pfdc, UInt32 dwIDCtl, UInt32 dwIDItem)
        {
            var control = this.Dialog.Controls.FirstOrDefault(x => x.Id == dwIDCtl);

            if (control is IFileDialogIndexedControls)
            {
                var controlInterface = control as IFileDialogIndexedControls;
                controlInterface.SelectedIndex = (int)dwIDItem;
                controlInterface.RaiseSelectedIndexChangedEvent();
            }
            else if (control is FileDialogMenu)
            {
                var menu = control as FileDialogMenu;
                var menuItem = menu.Items.FirstOrDefault(x => x.Id == dwIDItem);
                menuItem?.RaiseClickEvent();
            }
        }

        public void OnButtonClicked(IFileDialogCustomize pfdc, UInt32 dwIDCtl)
        {
            var control = this.Dialog.Controls.FirstOrDefault(x => x.Id == dwIDCtl);

            var button = control as FileDialogButton;
            button?.RaiseClickEvent();
        }

        public void OnCheckButtonToggled(IFileDialogCustomize pfdc, UInt32 dwIDCtl, bool bChecked)
        {
            var control = this.Dialog.Controls.FirstOrDefault(x => x.Id == dwIDCtl);

            var checkBox = control as FileDialogCheckBox;
            checkBox?.RaiseCheckedChangedEvent(bChecked);
        }

        public void OnControlActivating(IFileDialogCustomize pfdc, UInt32 dwIDCtl)
        {
        }
    }
}