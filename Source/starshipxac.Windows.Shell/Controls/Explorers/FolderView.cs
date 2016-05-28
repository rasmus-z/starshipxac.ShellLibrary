using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using starshipxac.Shell;
using starshipxac.Shell.Internal;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Controls.Explorers.Interop;
using starshipxac.Windows.Shell.Properties;

namespace starshipxac.Windows.Shell.Controls.Explorers
{
    public class FolderView : IDisposable
    {
        private bool disposed = false;

        private readonly ExplorerBrowserBase explorerBrowser;
        private IFolderView2 folderViewNative = null;

        private FolderView(ExplorerBrowserBase explorerBrowser)
        {
            Contract.Requires<ArgumentNullException>(explorerBrowser != null);

            this.explorerBrowser = explorerBrowser;
            this.folderViewNative = Create();
        }

        ~FolderView()
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
                if (disposing)
                {
                }

                if (this.folderViewNative != null)
                {
                    Marshal.ReleaseComObject(this.folderViewNative);
                    this.folderViewNative = null;
                }

                this.disposed = true;
            }
        }

        public static FolderView Create(ExplorerBrowserBase explorerBrowser)
        {
            Contract.Requires<ArgumentNullException>(explorerBrowser != null);
            Contract.Requires<ArgumentNullException>(explorerBrowser.ExplorerBrowserNative != null);

            return new FolderView(explorerBrowser);
        }

        internal static FolderView CreateInternal(ExplorerBrowserBase explorerBrowser)
        {
            Contract.Requires<ArgumentNullException>(explorerBrowser != null);

            return new FolderView(explorerBrowser);
        }

        public FOLDERVIEWMODE ViewMode
        {
            get
            {
                uint result = 0;
                if (this.folderViewNative != null)
                {
                    var hr = this.folderViewNative.GetCurrentViewMode(out result);
                    if (HRESULT.Failed(hr))
                    {
                        throw new ExplorerControlException(ErrorMessages.ExplorerGetViewModeError, hr);
                    }
                }
                return (FOLDERVIEWMODE)result;
            }
        }

        public int IconSize
        {
            get
            {
                var result = 0;
                if (this.folderViewNative != null)
                {
                    var viewMode = 0;
                    var hr = this.folderViewNative.GetViewModeAndIconSize(out viewMode, out result);
                    if (HRESULT.Failed(hr))
                    {
                        throw new ExplorerControlException(ErrorMessages.ExplorerGetIconSizeError, hr);
                    }
                }
                return result;
            }
            set
            {
                if (this.folderViewNative != null)
                {
                    var viewMode = 0;
                    var iconSize = 0;
                    var hr = this.folderViewNative.GetViewModeAndIconSize(out viewMode, out iconSize);
                    if (HRESULT.Failed(hr))
                    {
                        throw new ExplorerControlException(ErrorMessages.ExplorerGetIconSizeError, hr);
                    }

                    hr = this.folderViewNative.SetViewModeAndIconSize(viewMode, value);
                    if (HRESULT.Failed(hr))
                    {
                        throw new ExplorerControlException(ErrorMessages.ExplorerSetIconSizeError, hr);
                    }
                }
            }
        }

        public int ItemsCount
        {
            get
            {
                var result = 0;
                if (this.folderViewNative != null)
                {
                    var hr = this.folderViewNative.ItemCount((uint)SVGIO.SVGIO_ALLVIEW, out result);
                    if (hr != COMErrorCodes.S_OK &&
                        hr != COMErrorCodes.E_FAIL &&
                        hr != COMErrorCodes.TYPE_E_ELEMENTNOTFOUND)
                    {
                        throw new ExplorerControlException(ErrorMessages.ExplorerGetItemCountError);
                    }
                }
                return result;
            }
        }

        public int SelectedItemsCount
        {
            get
            {
                var result = 0;
                if (this.folderViewNative != null)
                {
                    var hr = this.folderViewNative.ItemCount((uint)SVGIO.SVGIO_SELECTION, out result);
                    if (hr != COMErrorCodes.S_OK &&
                        hr != COMErrorCodes.E_FAIL &&
                        hr != COMErrorCodes.TYPE_E_ELEMENTNOTFOUND)
                    {
                        throw new ExplorerControlException(ErrorMessages.ExplorerGetSelectedItemCountError);
                    }
                }
                return result;
            }
        }

        private IFolderView2 Create()
        {
            if (this.explorerBrowser.ExplorerBrowserNative == null)
            {
                return null;
            }

            var iid = new Guid(ExplorerBrowserIIDGuid.IFolderView2);
            IntPtr result;
            var hr = this.explorerBrowser.ExplorerBrowserNative.GetCurrentView(ref iid, out result);
            if (HRESULT.Failed(hr))
            {
                if (hr == COMErrorCodes.E_NOINTERFACE || hr == COMErrorCodes.E_FAIL)
                {
                    return null;
                }
                throw new ExplorerControlException(ErrorMessages.ExplorerGetViewError, hr);
            }

            return (IFolderView2)Marshal.GetObjectForIUnknown(result);
        }

        public IEnumerable<ShellObject> GetItems()
        {
            var shellItemArray = GetItemsArray();

            var count = ShellItemArray.GetShellItemCount(shellItemArray);
            for (var index = 0; index < count; ++index)
            {
                var shellItem = ShellItemArray.GetShellItemAt(shellItemArray, index);
                var shellObject = ShellFactory.FromShellItem(new ShellItem((IShellItem2)shellItem));
                if (shellObject != null)
                {
                    yield return shellObject;
                }
            }
        }

        public IEnumerable<ShellObject> GetSelectedItems()
        {
            var shellItemArray = GetSelectedItemsArray();

            var count = ShellItemArray.GetShellItemCount(shellItemArray);
            for (var index = 0; index < count; ++index)
            {
                var shellItem = ShellItemArray.GetShellItemAt(shellItemArray, index);
                var shellObject = ShellFactory.FromShellItem(new ShellItem((IShellItem2)shellItem));
                if (shellObject != null)
                {
                    yield return shellObject;
                }
            }
        }

        private IShellItemArray GetItemsArray()
        {
            Contract.Ensures(Contract.Result<IShellItemArray>() != null);

            object result = null;

            if (this.folderViewNative != null)
            {
                var iid = new Guid(ShellIID.IShellItemArray);
                var hr = this.folderViewNative.Items((uint)SVGIO.SVGIO_ALLVIEW, ref iid, out result);
                if (hr != COMErrorCodes.S_OK &&
                    hr != COMErrorCodes.E_FAIL &&
                    hr != COMErrorCodes.TYPE_E_ELEMENTNOTFOUND &&
                    hr != COMErrorCodes.E_INVALIDARG)
                {
                    throw new ExplorerControlException(ErrorMessages.ExplorerGetItemsError, hr);
                }
            }
            return result as IShellItemArray;
        }

        private IShellItemArray GetSelectedItemsArray()
        {
            object result = null;

            if (this.folderViewNative != null)
            {
                var iid = new Guid(ShellIID.IShellItemArray);
                var hr = this.folderViewNative.Items((uint)SVGIO.SVGIO_SELECTION, ref iid, out result);
                if (hr != COMErrorCodes.S_OK &&
                    hr != COMErrorCodes.E_FAIL &&
                    hr != COMErrorCodes.TYPE_E_ELEMENTNOTFOUND)
                {
                    throw new ExplorerControlException(ErrorMessages.ExplorerGetSelectedItemsError);
                }
            }

            return result as IShellItemArray;
        }
    }
}