using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Media.Imaging
{
    public sealed class ShellThumbnail : IDisposable
    {
        private bool disposed = false;

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

        ~ShellThumbnail()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                Win32Api.DeleteObject(this.ImageHandle);

                this.disposed = true;
            }
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