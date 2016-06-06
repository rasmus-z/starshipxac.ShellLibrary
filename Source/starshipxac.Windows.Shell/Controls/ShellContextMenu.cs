using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using starshipxac.Shell;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Controls.Interop;
using starshipxac.Windows.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls
{
    public class ShellContextMenu : IDisposable
    {
        private bool disposed = false;
        private IntPtr popupMenu;

        internal ShellContextMenu(IContextMenu3 contextMenu)
        {
            Contract.Requires<ArgumentNullException>(contextMenu != null);

            this.ContextMenuNative = contextMenu;

            // PopupMenu作成
            this.popupMenu = MenuNativeMethods.CreatePopupMenu();
            this.ContextMenuNative.QueryContextMenu(popupMenu, 0,
                this.CommandFirst, this.CommandLast, 0x0114);
        }

        ~ShellContextMenu()
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
                    if (this.popupMenu != IntPtr.Zero)
                    {
                        MenuNativeMethods.DestroyMenu(this.popupMenu);
                        this.popupMenu = IntPtr.Zero;
                    }
                }

                this.disposed = true;
            }
        }

        public static ShellContextMenu FromShellFile(ShellFile shellFile)
        {
            Contract.Requires<ArgumentNullException>(shellFile != null);

            var shellFolder = shellFile.Parent as ShellFolder;
            if (shellFolder == null)
            {
                return null;
            }

            // 対象ファイルの親フォルダーからの相対PIDLを取得する。
            UInt32 pchEaten = 0;
            IntPtr filePIDL;
            UInt32 attributes = 0;
            shellFolder.ShellFolderInterface.ParseDisplayName(
                IntPtr.Zero,
                IntPtr.Zero,
                shellFile.Name,
                ref pchEaten,
                out filePIDL,
                ref attributes);

            // IContextMenuへのポインターを取得する。
            var childPidls = new[] {filePIDL};
            var riid = new Guid(ShellIID.IContextMenu);
            IntPtr contextMenuPtr;
            shellFolder.ShellFolderInterface.GetUIObjectOf(
                IntPtr.Zero,
                (uint)childPidls.Length,
                childPidls,
                ref riid,
                IntPtr.Zero,
                out contextMenuPtr);

            // IContextMenu3へのポインターを取得する。
            IntPtr contextMenuPtr3;
            var riid3 = new Guid(ShellIID.IContextMenu2);
            Marshal.QueryInterface(contextMenuPtr, ref riid3, out contextMenuPtr3);
            var contextMenu3 = (IContextMenu3)Marshal.GetTypedObjectForIUnknown(contextMenuPtr3, typeof(IContextMenu3));

            return new ShellContextMenu(contextMenu3);
        }

        internal IContextMenu3 ContextMenuNative { get; }

        private int CommandFirst => 1;

        private int CommandLast => 300000;

        public string GetText(int commandId)
        {
            var result = new StringBuilder(256);
            MenuNativeMethods.GetMenuString(this.popupMenu, (UInt32)commandId,
                result, result.Capacity,
                MenuNativeMethods.MF_BYCOMMAND);
            return result.ToString();
        }

        public string GetVerb(int commandId)
        {
            var result = new StringBuilder(256);
            this.ContextMenuNative.GetCommandString((UIntPtr)commandId - this.CommandFirst,
                GCS.GCS_VERBW,
                UIntPtr.Zero, result, (uint)result.Capacity);
            return result.ToString();
        }

        public string GetHelpText(int commandId)
        {
            var result = new StringBuilder(256);
            this.ContextMenuNative.GetCommandString((UIntPtr)commandId - this.CommandFirst,
                GCS.GCS_HELPTEXTW,
                UIntPtr.Zero, result, (uint)result.Capacity);
            return result.ToString();
        }

        public MenuItemInfo GetItemInfo(int commandId)
        {
            var result = MENUITEMINFO.Create();
            result.fMask = MIIM.MIIM_BITMAP;
            MenuNativeMethods.GetMenuItemInfo(this.popupMenu, (UInt32)commandId, false, ref result);
            return MenuItemInfo.Create(result);
        }

        public int TrackPopupMenu(Window window, Point point)
        {
            Contract.Requires<ArgumentNullException>(window != null);

            var windowHelper = new WindowInteropHelper(window);

            var result = MenuNativeMethods.TrackPopupMenuEx(this.popupMenu,
                TPM.TPM_RETURNCMD,
                (int)point.X,
                (int)point.Y,
                windowHelper.Handle,
                IntPtr.Zero);
            return Convert.ToInt32(result);
        }
    }
}