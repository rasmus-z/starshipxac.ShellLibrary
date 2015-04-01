using System;
using System.Runtime.InteropServices;
using System.Windows;
using starshipxac.Shell.Media.Imaging;
using starshipxac.Windows.Shell.Controls.Interop;
using starshipxac.Windows.Shell.Interop;

namespace starshipxac.Windows.Shell.Media.Imaging
{
    /// <summary>
    /// シェルイメージリストを定義します。
    /// </summary>
    public class ShellImageList : IDisposable
    {
        private bool disposed = false;

        private IImageList imageList;

        public ShellImageList(int size)
        {
            var iid = ControlGuid.IID_IImageList;
            WindowsShellNativeMethods.SHGetImageList(size, ref iid, out this.imageList);
        }

        ~ShellImageList()
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
                }

                if (this.imageList != null)
                {
                    Marshal.ReleaseComObject(this.imageList);
                    this.imageList = null;
                }
            }
        }

        public ShellIcon GetIcon(int iconIndex)
        {
            var hIcon = IntPtr.Zero;
            this.imageList.GetIcon(iconIndex, IMAGELISTDRAWFLAGS.IDL_TRANSPARENT, ref hIcon);
            return new ShellIcon(hIcon);
        }

        public ShellIcon GetIcon(int iconIndex, int overlayIndex)
        {
            var hIcon = IntPtr.Zero;
            this.imageList.GetIcon(iconIndex, ControlNativeMethods.INDEXTOOVERLAYMASK((uint)overlayIndex), ref hIcon);
            return new ShellIcon(hIcon);
        }

        public ShellIcon GetOverlayIcon(int overlayIndex)
        {
            var index = 0;
            this.imageList.GetOverlayImage(overlayIndex, ref index);

            var hIcon = IntPtr.Zero;
            this.imageList.GetIcon(index, IMAGELISTDRAWFLAGS.IDL_TRANSPARENT, ref hIcon);
            return new ShellIcon(hIcon);
        }

        public Rect GetIconRect()
        {
            var cx = 0;
            var cy = 0;
            this.imageList.GetIconSize(ref cx, ref cy);

            return new Rect(0, 0, cx, cy);
        }
    }
}