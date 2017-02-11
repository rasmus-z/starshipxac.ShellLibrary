using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Media.Imaging.Internal;

namespace starshipxac.Shell.Media.Imaging
{
    /// <summary>
    ///     Define <see cref="ShellThumbnail" /> factory.
    /// </summary>
    [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
    public class ShellThumbnailFactory
    {
        private readonly ShellItem shellItem;
        private readonly ShellItemImageFactory imageFactory;

        /// <summary>
        ///     Initialize a instance of the <see cref="ShellThumbnailFactory"/> class
        ///     to the specified <see cref="ShellItem"/>.
        /// </summary>
        /// <param name="shellItem"></param>
        public ShellThumbnailFactory(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);

            this.shellItem = shellItem;
            this.imageFactory = new ShellItemImageFactory((IShellItemImageFactory)this.shellItem.ShellItemInterface);
        }

        /// <summary>
        ///     Create a new instance of the <see cref="ShellThumbnail"/> class
        ///     to the specified the type of thumbnail to be created.
        /// </summary>
        /// <param name="thumbnailMode">Thumbnail mode.</param>
        /// <returns></returns>
        public ShellThumbnail Create(ThumbnailMode thumbnailMode)
        {
            Contract.Ensures(Contract.Result<ShellThumbnail>() != null);

            double width;
            double height;
            GetSize(thumbnailMode, out width, out height);
            var imageHandle = this.imageFactory.GetImageHandle(width, height);

            return Create(this.shellItem, imageHandle, width, height);
        }

        /// <summary>
        ///     作成するサムネイルの幅と高さを指定して、<see cref="ShellThumbnail" />クラスの新しいインスタンスを作成します。
        ///     Create a new instance of the <see cref="ShellThumbnail"/> class
        ///     to the specified the width and height of the thumbnail to be created.
        /// </summary>
        /// <param name="width">Thumbnail image width.</param>
        /// <param name="height">Thumbnail image height.</param>
        /// <returns></returns>
        public ShellThumbnail Create(double width, double height)
        {
            Contract.Requires<ArgumentOutOfRangeException>(0.0 <= width);
            Contract.Requires<ArgumentOutOfRangeException>(0.0 <= height);
            Contract.Ensures(Contract.Result<ShellThumbnail>() != null);

            var imageHandle = this.imageFactory.GetImageHandle(width, height);

            return Create(this.shellItem, imageHandle, width, height);
        }

        private static ShellThumbnail Create(ShellItem shellItem, IntPtr imageHandle, double width, double height)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Requires<ArgumentOutOfRangeException>(0.0 <= width);
            Contract.Requires<ArgumentOutOfRangeException>(0.0 <= height);
            Contract.Ensures(Contract.Result<ShellThumbnail>() != null);

            return new ShellThumbnail(shellItem, imageHandle, width, height);
        }

        public static void GetSize(ThumbnailMode mode, out double width, out double height)
        {
            if (mode == ThumbnailMode.PicturesView)
            {
                width = 190;
                height = 190;
            }
            else if (mode == ThumbnailMode.VideosView)
            {
                width = 190;
                height = 130;
            }
            else if (mode == ThumbnailMode.MusicView)
            {
                width = 40;
                height = 40;
            }
            else if (mode == ThumbnailMode.DocumentsView)
            {
                width = 40;
                height = 40;
            }
            else if (mode == ThumbnailMode.ListView)
            {
                width = 40;
                height = 40;
            }
            else if (mode == ThumbnailMode.SingleItem)
            {
                width = 256;
                height = 256;
            }
            else
            {
                width = 40;
                height = 40;
            }
        }
    }
}