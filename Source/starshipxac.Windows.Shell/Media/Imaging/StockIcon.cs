using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Media.Imaging.Interop;
using starshipxac.Windows.Shell.Properties;

namespace starshipxac.Windows.Shell.Media.Imaging
{
    /// <summary>
    ///     標準アイコンを保持します。
    /// </summary>
    public class StockIcon : IDisposable
    {
        private bool disposed = false;

        private IntPtr hIcon = IntPtr.Zero;

        internal StockIcon(SHSTOCKICONID stockIconId, StockIconSize size, bool isLinkOverlay, bool isSelected)
        {
            this.Id = stockIconId;
            this.Size = StockIconSize.Large;
            this.LinkOverlay = isLinkOverlay;
            this.Selected = isSelected;
            this.Size = size;
        }

        ~StockIcon()
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

                DestroyIcon(this.hIcon);
            }
        }

        /// <summary>
        ///     標準アイコンIDを取得します。
        /// </summary>
        internal SHSTOCKICONID Id { get; }

        /// <summary>
        ///     アイコンサイズを取得します。
        /// </summary>
        public StockIconSize Size { get; }

        /// <summary>
        ///     リンクオーバーレイを取得するかどうかを判定する値を取得します。
        /// </summary>
        public bool LinkOverlay { get; }

        /// <summary>
        ///     選択状態アイコンを取得するかどうかを判定する値を取得します。
        /// </summary>
        public bool Selected { get; }

        /// <summary>
        ///     アイコンの<see cref="BitmapSource" />を取得します。
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

        #region Native Methods

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyIcon(IntPtr hIcon);

        #endregion
    }
}