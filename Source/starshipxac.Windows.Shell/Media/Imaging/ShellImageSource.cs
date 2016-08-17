using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Media.Imaging;

namespace starshipxac.Windows.Shell.Media.Imaging
{
    /// <summary>
    ///     <c>Shellobject</c>イメージを保持します。
    /// </summary>
    public class ShellImageSource : INotifyPropertyChanged, IDisposable
    {
        private bool disposed = false;

        private ImageSource imageSource = null;
        private ImageSource defaultImage = null;
        private ImageSource thumbnailImage = null;
        private ImageSource overlyaImage = null;

        private static readonly ConcurrentDictionary<string, PropertyChangedEventArgs> PropertyChangedEventArgsDictionary =
            new ConcurrentDictionary<string, PropertyChangedEventArgs>();

        /// <summary>
        ///     <see cref="ShellImageSource" />クラス新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellThumbnail"></param>
        public ShellImageSource(ShellThumbnail shellThumbnail)
        {
            Contract.Requires<ArgumentNullException>(shellThumbnail != null);

            this.ShellThumbnail = shellThumbnail;

            var sfi = new SHFILEINFO();
            const UInt32 flags = SHGFI.SHGFI_PIDL |
                                 SHGFI.SHGFI_ICON |
                                 SHGFI.SHGFI_SYSICONINDEX |
                                 SHGFI.SHGFI_OVERLAYINDEX;
            ShellNativeMethods.SHGetFileInfo(this.ShellThumbnail.ShellObject.ShellItem.PIDL, 0, ref sfi, flags);

            this.IconIndex = sfi.iIcon & 0x00FFFFFF;
            this.OverlayIndex = (sfi.iIcon >> 24) & 0xFF;
        }

        ~ShellImageSource()
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
                    this.ShellThumbnail.Dispose();
                }

                this.disposed = true;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ShellThumbnail != null);
        }

        private ShellThumbnail ShellThumbnail { get; }

        internal IntPtr ImageHandle => this.ShellThumbnail.ImageHandle;

        /// <summary>
        ///     アイコンインデックスを取得します。
        /// </summary>
        internal int IconIndex { get; }

        /// <summary>
        ///     オーバーレイアイコンインデックスを取得します。
        /// </summary>
        internal int OverlayIndex { get; }

        public double Width => this.ShellThumbnail.OriginalWidth;

        public double Height => this.ShellThumbnail.OriginalHeight;

        /// <summary>
        ///     イメージを段階的に取得します。
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                if (this.imageSource == null)
                {
                    Application.Current.Dispatcher.InvokeAsync(async () => await ShellImageSourceFactory.LoadAsync(this));
                }
                return this.imageSource;
            }
            set
            {
                this.imageSource = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     デフォルトアイコンイメージを取得します。
        /// </summary>
        public ImageSource DefaultImage
        {
            get
            {
                if (this.defaultImage == null)
                {
                    Application.Current.Dispatcher.InvokeAsync(async () => await ShellImageSourceFactory.GetDefaultIconWithOverlayAsync(this));
                }
                return this.defaultImage;
            }
            set
            {
                this.defaultImage = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     サムネイルイメージを取得します。
        /// </summary>
        public ImageSource ThumbnailImage
        {
            get
            {
                if (this.thumbnailImage == null)
                {
                    Application.Current.Dispatcher.InvokeAsync(async () => await ShellImageSourceFactory.GetThumbnailAsync(this));
                }
                return this.thumbnailImage;
            }
            set
            {
                this.thumbnailImage = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     オーバーレイイメージを取得します。
        /// </summary>
        public ImageSource OverlayImage
        {
            get
            {
                if (this.overlyaImage == null)
                {
                    Application.Current.Dispatcher.InvokeAsync(async () => await ShellImageSourceFactory.GetThumbnailAsync(this));
                }
                return this.overlyaImage;
            }
            set
            {
                this.overlyaImage = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     プロパティの値が変更されたことを通知します。
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            Contract.Requires<ArgumentNullException>(propertyName != null);

            this.PropertyChanged?.Invoke(
                this,
                PropertyChangedEventArgsDictionary.GetOrAdd(propertyName, name => new PropertyChangedEventArgs(name)));
        }
    }
}