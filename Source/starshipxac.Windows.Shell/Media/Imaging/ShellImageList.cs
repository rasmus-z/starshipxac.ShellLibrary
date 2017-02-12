using System;
using System.Runtime.InteropServices;
using System.Windows;
using starshipxac.Shell.Media.Imaging;
using starshipxac.Windows.Shell.Controls.Interop;
using starshipxac.Windows.Shell.Interop;

namespace starshipxac.Windows.Shell.Media.Imaging
{
    /// <summary>
    ///     Define shell image list class.
    /// </summary>
    public class ShellImageList : IDisposable
    {
        private bool disposed = false;

        private IImageList imageList;

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellImageList" /> class
        ///     to the specified image size.
        /// </summary>
        /// <param name="size">Image size.</param>
        public ShellImageList(int size)
        {
            var iid = ControlGuid.IID_IImageList;
            WindowsShellNativeMethods.SHGetImageList(size, ref iid, out this.imageList);
        }

        /// <summary>
        ///     Finalizer.
        /// </summary>
        ~ShellImageList()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Release all resources used by <see cref="ShellImageList" />.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Release all resources used by <see cref="ShellImageList" />,
        ///     and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources.
        ///     <c>false</c> to release only unmanaged resources.
        /// </param>
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

        /// <summary>
        ///     Get the icon.
        /// </summary>
        /// <param name="iconIndex">Index of icon.</param>
        /// <returns><see cref="ShellIcon" />.</returns>
        public ShellIcon GetIcon(int iconIndex)
        {
            var hIcon = IntPtr.Zero;
            this.imageList.GetIcon(iconIndex, IMAGELISTDRAWFLAGS.IDL_TRANSPARENT, ref hIcon);
            return new ShellIcon(hIcon);
        }

        /// <summary>
        ///     Get the icon.
        /// </summary>
        /// <param name="iconIndex">Index of icon.</param>
        /// <param name="overlayIndex">Index of overlay icon.</param>
        /// <returns><see cref="ShellIcon" />.</returns>
        public ShellIcon GetIcon(int iconIndex, int overlayIndex)
        {
            var hIcon = IntPtr.Zero;
            this.imageList.GetIcon(iconIndex, ControlNativeMethods.INDEXTOOVERLAYMASK((uint)overlayIndex), ref hIcon);
            return new ShellIcon(hIcon);
        }

        /// <summary>
        ///     Get the overlay icon.
        /// </summary>
        /// <param name="overlayIndex">Index of overlay icon.</param>
        /// <returns><see cref="ShellIcon" />.</returns>
        public ShellIcon GetOverlayIcon(int overlayIndex)
        {
            var index = 0;
            this.imageList.GetOverlayImage(overlayIndex, ref index);

            var hIcon = IntPtr.Zero;
            this.imageList.GetIcon(index, IMAGELISTDRAWFLAGS.IDL_TRANSPARENT, ref hIcon);
            return new ShellIcon(hIcon);
        }

        /// <summary>
        ///     Get the icon rectangle.
        /// </summary>
        /// <returns><see cref="Rect" />.</returns>
        public Rect GetIconRect()
        {
            var cx = 0;
            var cy = 0;
            this.imageList.GetIconSize(ref cx, ref cy);

            return new Rect(0, 0, cx, cy);
        }
    }
}