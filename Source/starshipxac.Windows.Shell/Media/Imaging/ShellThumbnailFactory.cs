using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using starshipxac.Shell;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Media.Imaging;

namespace starshipxac.Windows.Shell.Media.Imaging
{
    public class ShellThumbnailFactory
    {
        private Size size = new Size(96, 96);
        private ShellImageSize imageSize = new ShellImageSize(96, 96);

        public ShellThumbnailFactory(Size initialThumbnailSize)
        {
            Contract.Requires<ArgumentOutOfRangeException>(initialThumbnailSize.Width > 0.0);
            Contract.Requires<ArgumentOutOfRangeException>(initialThumbnailSize.Height > 0.0);

            this.FormatOption = ShellThumbnailFormatOptions.Default;
            this.Size = initialThumbnailSize;
            this.imageSize = new ShellImageSize(this.size.Width, this.size.Height);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Size.Width > 0.0);
            Contract.Invariant(this.Size.Height > 0.0);
        }

        public Size Size
        {
            get
            {
                return this.size;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>((value.Width > 0.0) && (value.Height > 0.0));

                this.size = value;
                this.imageSize = new ShellImageSize(this.size.Width, this.size.Height);
            }
        }

        public ShellThumbnailRetrievalOptions RetrievalOption { get; set; }

        public ShellThumbnailFormatOptions FormatOption { get; set; }

        /// <summary>
        /// <see cref="ShellObject"/>�̃A�C�R���E�T���l�C���C���[�W���擾���A
        /// �w�肵��<see cref="ShellThumbnail"/>�̊e�v���p�e�B�ɐݒ肵�܂��B
        /// </summary>
        /// <param name="shellThumbnail">�擾�����A�C�R���E�T���l�C���C���[�W��ݒ肷��<see cref="ShellThumbnail"/>�B</param>
        /// <returns></returns>
        public virtual async Task LoadAsync(ShellThumbnail shellThumbnail)
        {
            Contract.Requires<ArgumentNullException>(shellThumbnail != null);

            await GetDefaultIconAsync(shellThumbnail);
            await GetDefaultIconWithOverlayAsync(shellThumbnail);

            if (this.Size.Width >= 32)
            {
                await GetThumbnailAsync(shellThumbnail);
            }
        }

        /// <summary>
        /// <see cref="ShellObject"/>�̃f�t�H���g�A�C�R�����擾���܂��B
        /// </summary>
        /// <param name="shellThumbnail"></param>
        /// <returns></returns>
        /// <remarks>
        /// <see cref="ShellThumbnail"/>��<c>ImageSource</c>�����<c>DefaultImage</c>�v���p�e�B�Ɏ擾�����C���[�W��ݒ肵�܂��B
        /// </remarks>
        public virtual async Task GetDefaultIconAsync(ShellThumbnail shellThumbnail)
        {
            Contract.Requires<ArgumentNullException>(shellThumbnail != null);

            var imageSource = await GetIconBitmapSourceAsync(shellThumbnail);

            shellThumbnail.ImageSource = imageSource;
            shellThumbnail.DefaultImage = imageSource;
        }

        /// <summary>
        /// <see cref="ShellObject"/>�̃A�C�R���ƃI�[�o�[���C�A�C�R�����擾���܂��B
        /// </summary>
        /// <param name="shellThumbnail"></param>
        /// <returns></returns>
        /// <remarks>
        /// <see cref="ShellThumbnail"/>��<c>ImageSource</c>�����<c>DefaultImage</c>�v���p�e�B�Ɏ擾�����C���[�W��ݒ肵�܂��B
        /// </remarks>
        public virtual async Task GetDefaultIconWithOverlayAsync(ShellThumbnail shellThumbnail)
        {
            Contract.Requires<ArgumentNullException>(shellThumbnail != null);

            var imageSource = await GetIconWithOverlayBitmapSourceAsync(shellThumbnail);

            shellThumbnail.ImageSource = imageSource;
            shellThumbnail.DefaultImage = imageSource;
        }

        /// <summary>
        /// <see cref="ShellObject"/>�̃T���l�C���C���[�W���擾���܂��B
        /// </summary>
        /// <param name="shellThumbnail"></param>
        /// <returns></returns>
        /// <remarks>
        /// <see cref="ShellThumbnail"/>��<c>ImageSource</c>�����<c>ThumbnailImage</c>�v���p�e�B�Ɏ擾�����C���[�W��ݒ肵�܂��B
        /// </remarks>
        public virtual async Task GetThumbnailAsync(ShellThumbnail shellThumbnail)
        {
            Contract.Requires<ArgumentNullException>(shellThumbnail != null);

            var dg = new DrawingGroup();

            // �T���l�C���C���[�W�擾
            var thumbnailBitmapSource = await GetThumbnailBitmapSourceAsync(shellThumbnail.ShellObject);

            // �t���[��
            var thumbnailFrame =
                new RectangleGeometry(new Rect(0, 0, thumbnailBitmapSource.Width + 2, thumbnailBitmapSource.Height + 2));
            dg.Children.Add(new GeometryDrawing(Brushes.Transparent, null, thumbnailFrame));

            // �T���l�C��
            var thumbnailRect = new Rect(1, 1, thumbnailBitmapSource.PixelWidth, thumbnailBitmapSource.PixelHeight);
            dg.Children.Add(new ImageDrawing(thumbnailBitmapSource, thumbnailRect));

            // �I�[�o�[���C�A�C�R��
            DrawingImage overlayDrawingImage = null;
            if (shellThumbnail.OverlayIndex > 0)
            {
                // �I�[�o�[���C�A�C�R���擾
                var overlayBitmapSource = await GetOverlayIconBitmapSourceAsync(shellThumbnail);
                var overlayIconRect = new Rect(0, 0, overlayBitmapSource.PixelWidth, overlayBitmapSource.PixelHeight);

                var rect = overlayIconRect;
                if (this.Size.Width < 256)
                {
                    overlayIconRect.Offset(-2.0, thumbnailRect.Height - overlayIconRect.Height + 2.0);

                    rect.Offset(0, thumbnailRect.Height - overlayIconRect.Height);
                }
                else
                {
                    var scale = (this.Size.Width * 0.38) / overlayIconRect.Width;

                    overlayIconRect.Scale(scale, scale);
                    overlayIconRect.Offset(-4.0, thumbnailRect.Height - overlayIconRect.Height + 4.0);

                    rect.Scale(scale, scale);
                    rect.Offset(0, thumbnailRect.Height - rect.Height);
                }

                dg.Children.Add(new ImageDrawing(overlayBitmapSource, overlayIconRect));

                // OverlayImage�쐬
                var dgOverlay = new DrawingGroup();

                dgOverlay.Children.Add(new GeometryDrawing(Brushes.Transparent, null, new RectangleGeometry(thumbnailRect)));
                dgOverlay.Children.Add(new ImageDrawing(overlayBitmapSource, rect));

                if (dgOverlay.CanFreeze)
                {
                    dgOverlay.Freeze();
                }

                overlayDrawingImage = new DrawingImage(dgOverlay);
            }

            if (dg.CanFreeze)
            {
                dg.Freeze();
            }

            shellThumbnail.ImageSource = new DrawingImage(dg);
            shellThumbnail.ThumbnailImage = thumbnailBitmapSource;
            if (overlayDrawingImage != null)
            {
                shellThumbnail.OverlayImage = overlayDrawingImage;
            }
        }

        #region Private Methods

        /// <summary>
        /// �擾����A�C�R���̃T�C�Y�I�v�V�������v�Z���܂��B
        /// </summary>
        /// <returns></returns>
        private int GetSizeOption()
        {
            if (this.Size.Width <= 16)
            {
                return ImageListTypes.SHIL_SMALL;
            }
            else if (this.Size.Width <= 32)
            {
                return ImageListTypes.SHIL_LARGE;
            }
            else if (this.Size.Width <= 48)
            {
                return ImageListTypes.SHIL_EXTRALARGE;
            }
            return ImageListTypes.SHIL_JUMBO;
        }

        /// <summary>
        /// �擾����I�[�o�[���C�A�C�R���̃T�C�Y�I�v�V�������擾���܂��B
        /// </summary>
        /// <returns></returns>
        private int GetOverlayIconSizeOption()
        {
            if (this.Size.Width <= 16)
            {
                return ImageListTypes.SHIL_SMALL;
            }
            else if (this.Size.Width <= 32)
            {
                return ImageListTypes.SHIL_LARGE;
            }
            else if (this.Size.Width <= 48)
            {
                return ImageListTypes.SHIL_LARGE;
            }
            else if (this.Size.Width < 256)
            {
                return ImageListTypes.SHIL_EXTRALARGE;
            }
            return ImageListTypes.SHIL_JUMBO;
        }

        private Task<BitmapSource> GetThumbnailBitmapSourceAsync(ShellObject shellObject)
        {
            Contract.Requires(shellObject != null);

            return Application.Current.Dispatcher.InvokeAsync(() =>
            {
                var hBitmap = shellObject.ImageFactory.GetImageHandle(this.imageSize);
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }).Task;
        }

        private Task<BitmapSource> GetIconBitmapSourceAsync(ShellThumbnail shellThumbnail)
        {
            Contract.Requires(shellThumbnail != null);

            return Application.Current.Dispatcher.InvokeAsync(() =>
            {
                using (var imageList = new ShellImageList(GetSizeOption()))
                {
                    using (var icon = imageList.GetIcon(shellThumbnail.IconIndex))
                    {
                        return ShellIconFactory.CreateBitmapSource(icon);
                    }
                }
            }).Task;
        }

        private Task<BitmapSource> GetOverlayIconBitmapSourceAsync(ShellThumbnail shellThumbnail)
        {
            Contract.Requires(shellThumbnail != null);

            return Application.Current.Dispatcher.InvokeAsync(() =>
            {
                using (var imageList = new ShellImageList(GetOverlayIconSizeOption()))
                {
                    using (var icon = imageList.GetOverlayIcon(shellThumbnail.OverlayIndex))
                    {
                        return ShellIconFactory.CreateBitmapSource(icon);
                    }
                }
            }).Task;
        }

        private Task<BitmapSource> GetIconWithOverlayBitmapSourceAsync(ShellThumbnail shellThumbnail)
        {
            Contract.Requires(shellThumbnail != null);

            return Application.Current.Dispatcher.InvokeAsync(() =>
            {
                using (var imageList = new ShellImageList(GetSizeOption()))
                {
                    using (var icon = imageList.GetIcon(shellThumbnail.IconIndex, shellThumbnail.OverlayIndex))
                    {
                        return ShellIconFactory.CreateBitmapSource(icon);
                    }
                }
            }).Task;
        }

        #endregion
    }
}