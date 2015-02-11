using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDual)]
	internal class ExplorerBrowserViewEvents : IDisposable
	{
		bool disposed = false;

		private ExplorerBrowserBase explorerBrowser;
		private uint viewConnectionPointCookie;
		private object viewDispatch;

		private Guid IID_DShellFolderViewEvents = new Guid(ExplorerBrowserIIDGuid.DShellFolderViewEvents);
		private Guid IID_IDispatch = new Guid(ExplorerBrowserIIDGuid.IDispatch);

		public ExplorerBrowserViewEvents(ExplorerBrowserBase explorerBrowser)
		{
			this.explorerBrowser = explorerBrowser;
		}

		~ExplorerBrowserViewEvents()
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
				this.disposed = true;

				if (disposing)
				{
					DisconnectFromView();
				}
			}
		}

		internal void ConnectToView(IShellView shellView)
		{
			Contract.Requires<ArgumentNullException>(shellView != null);

			DisconnectFromView();

			var hr = shellView.GetItemObject(SVGIO.SVGIO_BACKGROUND,
				ref this.IID_IDispatch, out this.viewDispatch);
			if (HRESULT.Failed(hr))
			{
				return;
			}

			var ptr = IntPtr.Zero;
			hr = ExplorerBrowserNativeMethods.ConnectToConnectionPoint(this,
				ref this.IID_DShellFolderViewEvents,
				true,
				this.viewDispatch,
				ref this.viewConnectionPointCookie,
				ref ptr);
			if (HRESULT.Failed(hr))
			{
				Marshal.ReleaseComObject(this.viewDispatch);
				this.viewDispatch = null;
			}
		}

		internal void DisconnectFromView()
		{
			if (this.viewDispatch != null)
			{
				var ptr = IntPtr.Zero;
				ExplorerBrowserNativeMethods.ConnectToConnectionPoint(IntPtr.Zero,
					ref this.IID_DShellFolderViewEvents,
					false,
					this.viewDispatch,
					ref this.viewConnectionPointCookie,
					ref ptr);

				Marshal.ReleaseComObject(this.viewDispatch);
				this.viewDispatch = null;
				this.viewConnectionPointCookie = 0;
			}
		}

		[DispId(ExplorerBrowserViewDispatchIds.SelectionChanged)]
		public void ViewSelectionChanged()
		{
			// this.explorerBrowser.OnSelectionChanged();
		}

		[DispId(ExplorerBrowserViewDispatchIds.ContentsChanged)]
		public void ViewContentsChanged()
		{
		}

		[DispId(ExplorerBrowserViewDispatchIds.FileListEnumDone)]
		public void ViewFileListEnumDone()
		{
		}

		[DispId(ExplorerBrowserViewDispatchIds.SelectedItemChanged)]
		public void ViewSelectedItemChanged()
		{
		}
	}
}