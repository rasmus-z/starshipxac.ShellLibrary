using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Media.Imaging.Internal;

namespace starshipxac.Shell.Media.Imaging
{
    /// <summary>
    ///     <see cref="ShellThumbnail" />を作成します。
    /// </summary>
    [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
    public class ShellThumbnailFactory
    {
        private readonly ShellItem shellItem;
        private readonly ShellItemImageFactory imageFactory;

        /// <summary>
        /// <see cref="ShellItem"/>を指定して、
        /// <see cref="ShellThumbnailFactory"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellItem"></param>
        public ShellThumbnailFactory(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);

            this.shellItem = shellItem;
            this.imageFactory = new ShellItemImageFactory((IShellItemImageFactory)this.shellItem.ShellItemInterface);
        }

        /// <summary>
        /// 作成するサムネイルの種類を指定して、<see cref="ShellThumbnail"/>クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="thumbnailMode">作成するサムネイルの種類。</param>
        /// <returns></returns>
        public async Task<ShellThumbnail> CreateAsync(ThumbnailMode thumbnailMode)
        {
            Contract.Ensures(Contract.Result<ShellThumbnail>() != null);

            double width;
            double height;
            GetSize(thumbnailMode, out width, out height);
            var imageHandle = this.imageFactory.GetImageHandle(width, height);

            return await CreateAsync(this.shellItem, imageHandle, width, height);
        }

        /// <summary>
        /// 作成するサムネイルの幅と高さを指定して、<see cref="ShellThumbnail"/>クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="width">作成するサムネイルの幅。</param>
        /// <param name="height">作成するサムネイルの高さ。</param>
        /// <returns></returns>
        public async Task<ShellThumbnail> CreateAsync(double width, double height)
        {
            Contract.Requires<ArgumentOutOfRangeException>(0.0 <= width);
            Contract.Requires<ArgumentOutOfRangeException>(0.0 <= height);
            Contract.Ensures(Contract.Result<ShellThumbnail>() != null);

            var imageHandle = this.imageFactory.GetImageHandle(width, height);

            return await CreateAsync(this.shellItem, imageHandle, width, height);
        }

        private static Task<ShellThumbnail> CreateAsync(ShellItem shellItem, IntPtr imageHandle, double width, double height)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Requires<ArgumentOutOfRangeException>(0.0 <= width);
            Contract.Requires<ArgumentOutOfRangeException>(0.0 <= height);
            Contract.Ensures(Contract.Result<ShellThumbnail>() != null);

            return Task.Run(() => new ShellThumbnail(shellItem, imageHandle, width, height));
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