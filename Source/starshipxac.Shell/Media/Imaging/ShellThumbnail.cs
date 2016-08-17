using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Shell.Media.Imaging
{
    public sealed class ShellThumbnail
    {
        internal ShellThumbnail(ShellObject shellObject, ThumbnailMode thumbnailMode)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);

            this.ShellObject = shellObject;

            double width;
            double height;
            GetSize(thumbnailMode, out width, out height);
            this.OriginalWidth = width;
            this.OriginalHeight = height;
            this.ImageHandle = this.ShellObject.ImageFactory.GetImageHandle(this.OriginalWidth, this.OriginalHeight);
        }

        internal ShellObject ShellObject { get; }

        internal IntPtr ImageHandle { get; }

        public double OriginalWidth { get; }

        public double OriginalHeight { get; }

        private static void GetSize(ThumbnailMode mode, out double width, out double height)
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