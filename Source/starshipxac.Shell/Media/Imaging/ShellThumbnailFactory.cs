using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace starshipxac.Shell.Media.Imaging
{
    public static class ShellThumbnailFactory
    {
        public static Task<ShellThumbnail> CreateAsync(ShellItem shellItem, IntPtr imageHandle, double width, double height)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Requires<ArgumentOutOfRangeException>(0.0 <= width);
            Contract.Requires<ArgumentOutOfRangeException>(0.0 <= height);

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
