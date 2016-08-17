using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Media.Imaging
{
    /// <summary>
    ///     <see cref="ShellImageSource" />イメージファクトリを定義します。
    /// </summary>
    public static class ShellImageSourceFactory
    {
        /// <summary>
        ///     指定した<see cref="ShellImageSource" />からアイコン・サムネイルイメージを取得し、
        ///     <see cref="ShellImageSource" />の各プロパティに設定します。
        /// </summary>
        /// <param name="imageSource">取得したアイコン・サムネイルイメージを設定する<see cref="ShellImageSource" />。</param>
        /// <returns></returns>
        public static async Task LoadAsync(ShellImageSource imageSource)
        {
            Contract.Requires<ArgumentNullException>(imageSource != null);

            await GetDefaultIconAsync(imageSource);
            await GetDefaultIconWithOverlayAsync(imageSource);

            if (imageSource.Width >= 32)
            {
                await GetThumbnailAsync(imageSource);
            }
        }

        /// <summary>
        ///     デフォルトアイコンを取得します。
        /// </summary>
        /// <param name="imageSource"></param>
        /// <returns></returns>
        /// <remarks>
        ///     <see cref="ShellImageSource" />の<c>ImageSource</c>および<c>DefaultImage</c>プロパティに取得したイメージを設定します。
        /// </remarks>
        public static async Task GetDefaultIconAsync(ShellImageSource imageSource)
        {
            Contract.Requires<ArgumentNullException>(imageSource != null);

            var bitmapSource = await GetIconBitmapSourceAsync(imageSource);

            imageSource.ImageSource = bitmapSource;
            imageSource.DefaultImage = bitmapSource;
        }

        /// <summary>
        ///     アイコンとオーバーレイアイコンを取得します。
        /// </summary>
        /// <param name="imageSource"></param>
        /// <returns></returns>
        /// <remarks>
        ///     <see cref="ShellImageSource" />の<c>ImageSource</c>および<c>DefaultImage</c>プロパティに取得したイメージを設定します。
        /// </remarks>
        public static async Task GetDefaultIconWithOverlayAsync(ShellImageSource imageSource)
        {
            Contract.Requires<ArgumentNullException>(imageSource != null);

            var bitmapSource = await GetIconWithOverlayBitmapSourceAsync(imageSource);

            imageSource.ImageSource = bitmapSource;
            imageSource.DefaultImage = bitmapSource;
        }

        /// <summary>
        ///     サムネイルイメージを取得します。
        /// </summary>
        /// <param name="imageSource"></param>
        /// <returns></returns>
        /// <remarks>
        ///     <see cref="ShellImageSource" />の<c>ImageSource</c>および<c>ThumbnailImage</c>プロパティに取得したイメージを設定します。
        /// </remarks>
        public static async Task GetThumbnailAsync(ShellImageSource imageSource)
        {
            Contract.Requires<ArgumentNullException>(imageSource != null);

            var dg = new DrawingGroup();

            // サムネイルイメージ取得
            var thumbnailBitmapSource = await GetThumbnailBitmapSourceAsync(imageSource);

            // フレーム
            var thumbnailFrame =
                new RectangleGeometry(new Rect(0, 0, thumbnailBitmapSource.Width + 2, thumbnailBitmapSource.Height + 2));
            dg.Children.Add(new GeometryDrawing(Brushes.Transparent, null, thumbnailFrame));

            // サムネイル
            var thumbnailRect = new Rect(1, 1, thumbnailBitmapSource.PixelWidth, thumbnailBitmapSource.PixelHeight);
            dg.Children.Add(new ImageDrawing(thumbnailBitmapSource, thumbnailRect));

            // オーバーレイアイコン
            DrawingImage overlayDrawingImage = null;
            if (imageSource.OverlayIndex > 0)
            {
                // オーバーレイアイコン取得
                var overlayBitmapSource = await GetOverlayIconBitmapSourceAsync(imageSource);
                var overlayIconRect = new Rect(0, 0, overlayBitmapSource.PixelWidth, overlayBitmapSource.PixelHeight);

                var rect = overlayIconRect;
                if (imageSource.Width < 256)
                {
                    overlayIconRect.Offset(-2.0, thumbnailRect.Height - overlayIconRect.Height + 2.0);

                    rect.Offset(0, thumbnailRect.Height - overlayIconRect.Height);
                }
                else
                {
                    var scale = (imageSource.Width * 0.38)/overlayIconRect.Width;

                    overlayIconRect.Scale(scale, scale);
                    overlayIconRect.Offset(-4.0, thumbnailRect.Height - overlayIconRect.Height + 4.0);

                    rect.Scale(scale, scale);
                    rect.Offset(0, thumbnailRect.Height - rect.Height);
                }

                dg.Children.Add(new ImageDrawing(overlayBitmapSource, overlayIconRect));

                // OverlayImage作成
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

            imageSource.ImageSource = new DrawingImage(dg);
            imageSource.ThumbnailImage = thumbnailBitmapSource;
            if (overlayDrawingImage != null)
            {
                imageSource.OverlayImage = overlayDrawingImage;
            }
        }

        #region Private Methods

        /// <summary>
        ///     取得するアイコンのサイズオプションを計算します。
        /// </summary>
        /// <returns></returns>
        private static int GetSizeOption(ShellImageSource imageSource)
        {
            var width = imageSource.Width;
            if (width <= 16)
            {
                return ImageListTypes.SHIL_SMALL;
            }
            else if (width <= 32)
            {
                return ImageListTypes.SHIL_LARGE;
            }
            else if (width <= 48)
            {
                return ImageListTypes.SHIL_EXTRALARGE;
            }
            return ImageListTypes.SHIL_JUMBO;
        }

        /// <summary>
        ///     取得するオーバーレイアイコンのサイズオプションを取得します。
        /// </summary>
        /// <returns></returns>
        private static int GetOverlayIconSizeOption(ShellImageSource imageSource)
        {
            var width = imageSource.Width;
            if (width <= 16)
            {
                return ImageListTypes.SHIL_SMALL;
            }
            else if (width <= 32)
            {
                return ImageListTypes.SHIL_LARGE;
            }
            else if (width <= 48)
            {
                return ImageListTypes.SHIL_LARGE;
            }
            else if (width < 256)
            {
                return ImageListTypes.SHIL_EXTRALARGE;
            }
            return ImageListTypes.SHIL_JUMBO;
        }

        private static Task<BitmapSource> GetThumbnailBitmapSourceAsync(ShellImageSource imageSource)
        {
            Contract.Requires(imageSource != null);

            return Application.Current.Dispatcher.InvokeAsync(() =>
                System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    imageSource.ImageHandle,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions())
                ).Task;
        }

        private static Task<BitmapSource> GetIconBitmapSourceAsync(ShellImageSource imageSource)
        {
            Contract.Requires(imageSource != null);

            return Application.Current.Dispatcher.InvokeAsync(() =>
            {
                using (var imageList = new ShellImageList(GetSizeOption(imageSource)))
                {
                    using (var icon = imageList.GetIcon(imageSource.IconIndex))
                    {
                        return ShellIconFactory.CreateBitmapSource(icon);
                    }
                }
            }).Task;
        }

        private static Task<BitmapSource> GetOverlayIconBitmapSourceAsync(ShellImageSource imageSource)
        {
            Contract.Requires(imageSource != null);

            return Application.Current.Dispatcher.InvokeAsync(() =>
            {
                using (var imageList = new ShellImageList(GetOverlayIconSizeOption(imageSource)))
                {
                    using (var icon = imageList.GetOverlayIcon(imageSource.OverlayIndex))
                    {
                        return ShellIconFactory.CreateBitmapSource(icon);
                    }
                }
            }).Task;
        }

        private static Task<BitmapSource> GetIconWithOverlayBitmapSourceAsync(ShellImageSource imageSource)
        {
            Contract.Requires(imageSource != null);

            return Application.Current.Dispatcher.InvokeAsync(() =>
            {
                using (var imageList = new ShellImageList(GetSizeOption(imageSource)))
                {
                    using (var icon = imageList.GetIcon(imageSource.IconIndex, imageSource.OverlayIndex))
                    {
                        return ShellIconFactory.CreateBitmapSource(icon);
                    }
                }
            }).Task;
        }

        #endregion
    }
}