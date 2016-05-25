using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Interop;
using starshipxac.Shell;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Controls.Explorers.Interop;
using starshipxac.Windows.Shell.Interop;
using IServiceProvider = starshipxac.Windows.Shell.Controls.Explorers.Interop.IServiceProvider;

namespace starshipxac.Windows.Shell.Controls.Explorers
{
    public abstract class ExplorerBrowserBase
        : HwndHost, IServiceProvider, IExplorerPaneVisibility, IExplorerBrowserEvents, ICommDlgBrowser3
    {
        private HwndSource hwndSource;
        private ExplorerBrowserViewEvents viewEvents;
        private string propertyBagName;
        private uint eventCookie;

        // ReSharper disable once InconsistentNaming
        private const UInt32 WS_CHILD = 0x40000000;

        protected ExplorerBrowserBase()
        {
            this.PaneVisibility = new ExplorerBrowserPaneVisibility();
            this.FolderSettings = new FOLDERSETTINGS();
        }

        public string PropertyBagName
        {
            get
            {
                return this.propertyBagName;
            }
            set
            {
                this.propertyBagName = value;
                this.ExplorerBrowserNative?.SetPropertyBag(this.propertyBagName);
            }
        }

        internal ExplorerBrowserNative ExplorerBrowserNative { get; private set; }

        internal ExplorerBrowserPaneVisibility PaneVisibility { get; }

        internal FOLDERSETTINGS FolderSettings { get; }

        private void Create()
        {
            this.ExplorerBrowserNative = new ExplorerBrowserNative();
            ShellNativeMethods.IUnknown_SetSite(this.ExplorerBrowserNative, this);

            this.ExplorerBrowserNative.Advise(
                Marshal.GetComInterfaceForObject(this, typeof(IExplorerBrowserEvents)),
                out this.eventCookie);

            this.viewEvents = new ExplorerBrowserViewEvents(this);

            var rect = RECT.Create(0, 0, this.ActualWidth, this.ActualHeight);
            this.ExplorerBrowserNative.Initialize(this.hwndSource.Handle, ref rect, null);
            this.ExplorerBrowserNative.SetOptions(EXPLORER_BROWSER_OPTIONS.EBO_SHOWFRAMES);
            this.ExplorerBrowserNative.SetPropertyBag(this.PropertyBagName);
        }

        private void Destroy()
        {
            if (this.ExplorerBrowserNative != null)
            {
                this.viewEvents.DisconnectFromView();
                this.ExplorerBrowserNative.Unadvise(this.eventCookie);
                ShellNativeMethods.IUnknown_SetSite(this.ExplorerBrowserNative, null);

                this.ExplorerBrowserNative.Destroy();

                Marshal.ReleaseComObject(this.ExplorerBrowserNative);
                this.ExplorerBrowserNative = null;
            }
        }

        protected virtual void OnNavigationPending(NavigationPendingEventArgs args)
        {
        }

        protected virtual void OnNavigationCompleted(NavigationCompletedEventArgs args)
        {
        }

        protected virtual void OnNavigationFailed(NavigationFailedEventArgs args)
        {
        }

        protected virtual void OnSelectionChanged()
        {
        }

        protected virtual void OnContentChanged()
        {
        }

        internal void SetFolderSettings(FOLDERSETTINGS folderSettings)
        {
            this.ExplorerBrowserNative?.SetFolderSettings(folderSettings);
        }

        internal bool GetFolderSettingsFlag(FOLDERFLAGS flag)
        {
            return (this.FolderSettings.fFlags & flag) != 0;
        }

        internal void SetFolderSettingsFlag(FOLDERFLAGS flag, bool value)
        {
            if (value)
            {
                this.FolderSettings.fFlags = flag | FOLDERFLAGS.FWF_USESEARCHFOLDER | FOLDERFLAGS.FWF_NOWEBVIEW;
            }
            else
            {
                this.FolderSettings.fFlags = this.FolderSettings.fFlags & ~flag;
            }
            SetFolderSettings(this.FolderSettings);
        }

        internal EXPLORER_BROWSER_OPTIONS GetOptions()
        {
            var result = new EXPLORER_BROWSER_OPTIONS();

            if (this.ExplorerBrowserNative != null)
            {
                this.ExplorerBrowserNative.GetOptions(out result);
            }

            return result;
        }

        internal void SetOptions(EXPLORER_BROWSER_OPTIONS value)
        {
            this.ExplorerBrowserNative?.SetOptions(value | EXPLORER_BROWSER_OPTIONS.EBO_SHOWFRAMES);
        }

        internal bool GetOptionFlag(EXPLORER_BROWSER_OPTIONS option)
        {
            var result = GetOptions();
            return (result & option) != EXPLORER_BROWSER_OPTIONS.EBO_NONE;
        }

        internal void SetOptionFlag(EXPLORER_BROWSER_OPTIONS option, bool value)
        {
            var options = GetOptions();
            if (value)
            {
                options |= option;
            }
            else
            {
                options = options & ~option;
            }
            SetOptions(options);
        }

        #region HwndHost Methods

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            var parameters = new HwndSourceParameters
            {
                WindowName = "ExplorerBrowser",
                ParentWindow = hwndParent.Handle,
                WindowStyle = (int)WS_CHILD,
                PositionX = 0,
                PositionY = 0
            };
            this.hwndSource = new HwndSource(parameters);

            Create();

            return new HandleRef(this.hwndSource, this.hwndSource.Handle);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            Destroy();

            if (this.hwndSource != null)
            {
                this.hwndSource.Dispose();
                this.hwndSource = null;
            }
        }

        protected override void OnWindowPositionChanged(System.Windows.Rect rcBoundingBox)
        {
            base.OnWindowPositionChanged(rcBoundingBox);

            if (this.ExplorerBrowserNative != null)
            {
                var ptr = IntPtr.Zero;
                var rect = RECT.Create(rcBoundingBox.Left, rcBoundingBox.Top, rcBoundingBox.Right, rcBoundingBox.Bottom);
                this.ExplorerBrowserNative.SetRect(ref ptr, rect);
            }
        }

        #endregion

        #region IServiceProvider Methods

        HRESULT IServiceProvider.QueryService(ref Guid guidService, ref Guid riid, out IntPtr ppvObject)
        {
            var result = COMErrorCodes.S_OK;

            if (guidService == ExplorerBrowserGuid.IExplorerPaneVisibility)
            {
                // IExplorerPaneVisibility
                ppvObject = Marshal.GetComInterfaceForObject(this, typeof(IExplorerPaneVisibility));
                result = COMErrorCodes.S_OK;
            }
            else if (guidService == ExplorerBrowserGuid.ICommDlgBrowser)
            {
                if (riid == ExplorerBrowserGuid.ICommDlgBrowser)
                {
                    // ICommDlgBrowser
                    ppvObject = Marshal.GetComInterfaceForObject(this, typeof(ICommDlgBrowser3));
                    result = COMErrorCodes.S_OK;
                }
                    // ICommDlgBrowser2を取得すると AccessViolationExceptionが発生する。
                    //else if (riid == ExplorerBrowserGuid.ICommDlgBrowser2)
                    //{
                    //	// ICommDlgBrowser2
                    //	ppvObject = Marshal.GetComInterfaceForObject(this, typeof(ICommDlgBrowser3));
                    //	result = HRESULT.Ok;
                    //}
                else if (riid == ExplorerBrowserGuid.ICommDlgBrowser3)
                {
                    // ICommDlgBrowser3
                    ppvObject = Marshal.GetComInterfaceForObject(this, typeof(ICommDlgBrowser3));
                    result = COMErrorCodes.S_OK;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("riid = " + riid);
                    ppvObject = IntPtr.Zero;
                    result = COMErrorCodes.E_NOINTERFACE;
                }
            }
            else
            {
                ppvObject = IntPtr.Zero;
                result = COMErrorCodes.E_NOINTERFACE;
            }

            return result;
        }

        #endregion

        #region IExplorerPaneVisibility Methods

        HRESULT IExplorerPaneVisibility.GetPaneState(ref Guid explorerPane, out EXPLORERPANESTATE peps)
        {
            peps = this.PaneVisibility.GetPaneState(explorerPane);
            return COMErrorCodes.S_OK;
        }

        #endregion

        #region IExplorerBrowserEvents Methods

        HRESULT IExplorerBrowserEvents.OnNavigationPending(IntPtr pidlFolder)
        {
            var pendingLocation = ShellFactory.FromShellItem(ShellItem.FromPIDL((PIDL)pidlFolder));
            var args = new NavigationPendingEventArgs(pendingLocation);
            OnNavigationPending(args);

            return args.Cancel ? COMErrorCodes.Cancelled : COMErrorCodes.S_OK;
        }

        HRESULT IExplorerBrowserEvents.OnViewCreated(object psv)
        {
            this.viewEvents.ConnectToView((IShellView)psv);
            return COMErrorCodes.S_OK;
        }

        HRESULT IExplorerBrowserEvents.OnNavigationComplete(IntPtr pidlFolder)
        {
            using (var folderView = FolderView.Create(this))
            {
                this.FolderSettings.ViewMode = folderView.ViewMode;
            }

            var newLocation = ShellFactory.FromShellItem(ShellItem.FromPIDL((PIDL)pidlFolder));
            var args = new NavigationCompletedEventArgs(newLocation);
            OnNavigationCompleted(args);

            return COMErrorCodes.S_OK;
        }

        HRESULT IExplorerBrowserEvents.OnNavigationFailed(IntPtr pidlFolder)
        {
            var failedLocation = ShellFactory.FromShellItem(ShellItem.FromPIDL((PIDL)pidlFolder));
            var args = new NavigationFailedEventArgs(failedLocation);
            OnNavigationFailed(args);

            return COMErrorCodes.S_OK;
        }

        #endregion

        #region ICommDlgBrowser Methods

        HRESULT ICommDlgBrowser3.IncludeObject(IntPtr ppshv, IntPtr pidl)
        {
            OnContentChanged();

            return COMErrorCodes.S_OK;
        }

        HRESULT ICommDlgBrowser3.OnDefaultCommand(IntPtr ppshv)
        {
            return COMErrorCodes.S_FALSE;
        }

        HRESULT ICommDlgBrowser3.OnStateChange(IntPtr ppshv, UInt32 uChange)
        {
            if (uChange == CommDlgBrowserStateChanges.CDBOSC_SELCHANGE)
            {
                OnSelectionChanged();
            }

            return COMErrorCodes.S_OK;
        }

        HRESULT ICommDlgBrowser3.GetDefaultMenuText(IShellView shellView, IntPtr buffer, int bufferMaxLength)
        {
            return COMErrorCodes.S_FALSE;
        }

        HRESULT ICommDlgBrowser3.GetViewFlags(out uint pdwFlags)
        {
            pdwFlags = CommDlgBrowser2ViewFlags.CDB2GVF_SHOWALLFILES;
            return COMErrorCodes.S_OK;
        }

        HRESULT ICommDlgBrowser3.Notify(IntPtr pshv, UInt32 notifyType)
        {
            return COMErrorCodes.S_OK;
        }

        HRESULT ICommDlgBrowser3.GetCurrentFilter(StringBuilder pszFileSpec, int cchFileSpec)
        {
            return COMErrorCodes.S_OK;
        }

        HRESULT ICommDlgBrowser3.OnColumnClicked(IShellView ppshv, int iColumn)
        {
            return COMErrorCodes.S_OK;
        }

        HRESULT ICommDlgBrowser3.OnPreViewCreated(IShellView ppshv)
        {
            return COMErrorCodes.S_OK;
        }

        #endregion
    }
}