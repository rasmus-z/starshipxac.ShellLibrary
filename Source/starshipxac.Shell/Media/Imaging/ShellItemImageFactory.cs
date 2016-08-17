using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Properties;

namespace starshipxac.Shell.Media.Imaging
{
    /// <summary>
    ///     シェルイメージファクトリを定義します。
    /// </summary>
    public class ShellItemImageFactory
    {
        private readonly IShellItemImageFactory shllItemImageFactoryInterface;

        internal ShellItemImageFactory(IShellItemImageFactory shllItemImageFactoryInterface)
        {
            Contract.Requires<ArgumentNullException>(shllItemImageFactoryInterface != null);

            this.shllItemImageFactoryInterface = shllItemImageFactoryInterface;
            this.SizeOption = ShellItemImageSizeOptions.ResizeToFit;
            this.RetrievalOption = ShellItemImageRetrievalOptions.Default;
            this.FormatOption = ShellItemImageFormatOptions.Default;
        }

        public ShellItemImageSizeOptions SizeOption { get; set; }

        public ShellItemImageRetrievalOptions RetrievalOption { get; set; }

        public ShellItemImageFormatOptions FormatOption { get; set; }

        internal IntPtr GetImageHandle(double width, double height)
        {
            var size = new SIZE(width, height);
            var flags = GetFlags();
            IntPtr result;
            var hr = this.shllItemImageFactoryInterface.GetImage(size, flags, out result);
            if (HRESULT.Failed(hr))
            {
                if (hr == 0x8004B200 && this.FormatOption == ShellItemImageFormatOptions.ThumbnailOnly)
                {
                    throw new InvalidOperationException(
                        ErrorMessages.ShellThumbnailDoesNotHaveThumbnail, Marshal.GetExceptionForHR(hr));
                }
                if (hr == 0x80040154)
                {
                    throw new NotSupportedException(ErrorMessages.ShellThumbnailNoHandler, Marshal.GetExceptionForHR(hr));
                }
                throw ShellException.FromHRESULT(hr);
            }

            return result;
        }

        private SIIGBF GetFlags()
        {
            SIIGBF result = 0x0000;

            if (this.SizeOption == ShellItemImageSizeOptions.ResizeToFit)
            {
                result |= SIIGBF.SIIGBF_RESIZETOFIT;
            }
            else if (this.SizeOption == ShellItemImageSizeOptions.BiggerSizeOk)
            {
                result |= SIIGBF.SIIGBF_BIGGERSIZEOK;
            }

            if (this.RetrievalOption == ShellItemImageRetrievalOptions.CacheOnly)
            {
                result |= SIIGBF.SIIGBF_INCACHEONLY;
            }
            else if (this.RetrievalOption == ShellItemImageRetrievalOptions.MemoryOnly)
            {
                result |= SIIGBF.SIIGBF_MEMORYONLY;
            }

            if (this.FormatOption == ShellItemImageFormatOptions.IconOnly)
            {
                result |= SIIGBF.SIIGBF_INCACHEONLY;
            }
            else if (this.FormatOption == ShellItemImageFormatOptions.ThumbnailOnly)
            {
                result |= SIIGBF.SIIGBF_THUMBNAILONLY;
            }

            return result;
        }
    }
}