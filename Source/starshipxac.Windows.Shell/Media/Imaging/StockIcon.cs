using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Imaging;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Media.Imaging.Interop;
using starshipxac.Windows.Shell.Properties;

namespace starshipxac.Windows.Shell.Media.Imaging
{
    /// <summary>
    ///     Define stock icon class.
    /// </summary>
    public class StockIcon : IDisposable
    {
        private bool disposed = false;

        private IntPtr hIcon = IntPtr.Zero;

        /// <summary>
        ///     Initialize a new instance of the <see cref="StockIcon" /> class
        ///     to the specified stock icon ID, stock icon size, overlay flag and selected flag.
        /// </summary>
        /// <param name="stockIconId"></param>
        /// <param name="size"></param>
        /// <param name="isLinkOverlay"></param>
        /// <param name="isSelected"></param>
        internal StockIcon(SHSTOCKICONID stockIconId, StockIconSize size, bool isLinkOverlay, bool isSelected)
        {
            this.Id = stockIconId;
            this.Size = StockIconSize.Large;
            this.LinkOverlay = isLinkOverlay;
            this.Selected = isSelected;
            this.Size = size;
        }

        /// <summary>
        ///     Finalizer.
        /// </summary>
        ~StockIcon()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Release all resources used by <see cref="StockIcon" />.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Release all resources used by <see cref="StockIcon" />,
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

                StockIconsNativeMethods.DestroyIcon(this.hIcon);
            }
        }

        /// <summary>
        ///     Get the stock icon ID.
        /// </summary>
        internal SHSTOCKICONID Id { get; }

        /// <summary>
        ///     Get the stock icon size.
        /// </summary>
        public StockIconSize Size { get; }

        /// <summary>
        ///     Get a value that determines whether to acquire link overlay.
        /// </summary>
        public bool LinkOverlay { get; }

        /// <summary>
        ///     Gets a value that determines whether to acquire the selection status icon.
        /// </summary>
        public bool Selected { get; }

        /// <summary>
        ///     Get the icon's <see cref="BitmapSource" />.
        /// </summary>
        public BitmapSource BitmapSource
        {
            get
            {
                if (this.hIcon == IntPtr.Zero)
                {
                    this.hIcon = GetHIcon();
                }

                return (this.hIcon != IntPtr.Zero)
                    ? System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(this.hIcon, Int32Rect.Empty, null)
                    : null;
            }
        }

        #region Private Methods

        private IntPtr GetHIcon()
        {
            var flags = SHGSI.SHGSI_ICONLOCATION;

            if (this.Size == StockIconSize.Small)
            {
                flags |= SHGSI.SHGSI_SMALLICON;
            }
            else if (this.Size == StockIconSize.ShellSize)
            {
                flags |= SHGSI.SHGSI_SHELLICONSIZE;
            }
            else
            {
                flags |= SHGSI.SHGSI_LARGEICON;
            }

            if (this.Selected)
            {
                flags |= SHGSI.SHGSI_SELECTED;
            }

            if (this.LinkOverlay)
            {
                flags |= SHGSI.SHGSI_LINKOVERLAY;
            }

            var info = SHSTOCKICONINFO.Create();

            var hr = StockIconsNativeMethods.SHGetStockIconInfo(this.Id, flags, ref info);

            if (hr != COMErrorCodes.S_OK)
            {
                if (hr == COMErrorCodes.E_INVALIDARG)
                {
                    throw new InvalidOperationException(
                        string.Format(CultureInfo.InvariantCulture,
                            ErrorMessages.StockIconInvalidGuid,
                            this.Id));
                }

                return IntPtr.Zero;
            }

            return info.hIcon;
        }

        #endregion
    }
}