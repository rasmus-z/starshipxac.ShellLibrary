using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using starshipxac.Shell;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Media.Imaging
{
    /// <summary>
    ///     <see cref="ShellObject" />サムネイルイメージを保持します。
    /// </summary>
    public class ShellThumbnail : INotifyPropertyChanged
    {
        private ImageSource imageSource = null;
        private ImageSource defaultImage = null;
        private ImageSource thumbnailImage = null;
        private ImageSource overlyaImage = null;

        private static readonly ConcurrentDictionary<string, PropertyChangedEventArgs> PropertyChangedEventArgsDictionary =
            new ConcurrentDictionary<string, PropertyChangedEventArgs>();

        /// <summary>
        ///     <see cref="ShellThumbnail" />クラス新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellObject"></param>
        /// <param name="factory"></param>
        public ShellThumbnail(ShellObject shellObject, ShellThumbnailFactory factory)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            this.ShellObject = shellObject;
            this.Factory = factory;

            var sfi = new SHFILEINFO();
            const UInt32 flags = SHGFI.SHGFI_PIDL |
                                 SHGFI.SHGFI_ICON |
                                 SHGFI.SHGFI_SYSICONINDEX |
                                 SHGFI.SHGFI_OVERLAYINDEX;
            ShellNativeMethods.SHGetFileInfo(this.ShellObject.PIDL, 0, ref sfi, flags);

            this.IconIndex = sfi.iIcon & 0x00FFFFFF;
            this.OverlayIndex = (sfi.iIcon >> 24) & 0xFF;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ShellObject != null);
            Contract.Invariant(this.Factory != null);
        }

        /// <summary>
        ///     <see cref="ShellObject" />を取得します。
        /// </summary>
        public ShellObject ShellObject { get; }

        public ShellThumbnailFactory Factory { get; }

        /// <summary>
        ///     アイコンインデックスを取得します。
        /// </summary>
        public int IconIndex { get; private set; }

        /// <summary>
        ///     オーバーレイアイコンインデックスを取得します。
        /// </summary>
        public int OverlayIndex { get; private set; }

        /// <summary>
        ///     サムネイルイメージを段階的に取得します。
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                if (this.imageSource == null)
                {
                    Application.Current.Dispatcher.InvokeAsync(async () => await this.Factory.LoadAsync(this));
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
                    Application.Current.Dispatcher.InvokeAsync(async () => await this.Factory.GetDefaultIconWithOverlayAsync(this));
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
                    Application.Current.Dispatcher.InvokeAsync(async () => await this.Factory.GetThumbnailAsync(this));
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
                    Application.Current.Dispatcher.InvokeAsync(async () => await this.Factory.GetThumbnailAsync(this));
                }
                return this.overlyaImage;
            }
            set
            {
                this.overlyaImage = value;
                RaisePropertyChanged();
            }
        }

        public Size Size => this.Factory.Size;

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