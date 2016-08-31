﻿using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Media.Imaging;

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
        /// <param name="thumbnail">取得したアイコン・サムネイルイメージを設定する<see cref="ShellImageSource" />。</param>
        /// <param name="updateImageSource"></param>
        /// <returns></returns>
        public static async Task LoadAsync(ShellThumbnail thumbnail, Action<ImageSource> updateImageSource)
        {
            Contract.Requires<ArgumentNullException>(thumbnail != null);

            await GetDefaultIconAsync(thumbnail, updateImageSource);
            await GetDefaultIconWithOverlayAsync(thumbnail, updateImageSource);

            if (thumbnail.OriginalWidth >= 32)
            {
                await GetThumbnailAsync(thumbnail, updateImageSource);
            }
        }

        /// <summary>
        ///     デフォルトアイコンを取得します。
        /// </summary>
        /// <param name="thumbnail"></param>
        /// <param name="updateImageSource"></param>
        /// <returns></returns>
        /// <remarks>
        ///     <see cref="ShellImageSource" />の<c>ImageSource</c>および<c>DefaultImage</c>プロパティに取得したイメージを設定します。
        /// </remarks>
        public static async Task GetDefaultIconAsync(ShellThumbnail thumbnail, Action<ImageSource> updateImageSource)
        {
            Contract.Requires<ArgumentNullException>(thumbnail != null);

            var bitmapSource = await GetIconBitmapSourceAsync(thumbnail);
            updateImageSource.Invoke(bitmapSource);
        }

        /// <summary>
        ///     アイコンとオーバーレイアイコンを取得します。
        /// </summary>
        /// <param name="thumbnail"></param>
        /// <param name="updateImageSource"></param>
        /// <returns></returns>
        /// <remarks>
        ///     <see cref="ShellImageSource" />の<c>ImageSource</c>および<c>DefaultImage</c>プロパティに取得したイメージを設定します。
        /// </remarks>
        public static async Task GetDefaultIconWithOverlayAsync(ShellThumbnail thumbnail, Action<ImageSource> updateImageSource)
        {
            Contract.Requires<ArgumentNullException>(thumbnail != null);

            var bitmapSource = await GetIconWithOverlayBitmapSourceAsync(thumbnail);
            updateImageSource.Invoke(bitmapSource);
        }

        /// <summary>
        ///     サムネイルイメージを取得します。
        /// </summary>
        /// <param name="thumbnail"></param>
        /// <param name="updateImageSource"></param>
        /// <returns></returns>
        /// <remarks>
        ///     <see cref="ShellImageSource" />の<c>ImageSource</c>および<c>ThumbnailImage</c>プロパティに取得したイメージを設定します。
        /// </remarks>
        public static async Task GetThumbnailAsync(ShellThumbnail thumbnail, Action<ImageSource> updateImageSource)
        {
            Contract.Requires<ArgumentNullException>(thumbnail != null);

            var dg = new DrawingGroup();

            // サムネイルイメージ取得
            var thumbnailBitmapSource = await GetThumbnailBitmapSourceAsync(thumbnail);

            // フレーム
            var thumbnailFrame =
                new RectangleGeometry(new Rect(0, 0, thumbnailBitmapSource.Width + 2, thumbnailBitmapSource.Height + 2));
            dg.Children.Add(new GeometryDrawing(Brushes.Transparent, null, thumbnailFrame));

            // サムネイル
            var thumbnailRect = new Rect(1, 1, thumbnailBitmapSource.PixelWidth, thumbnailBitmapSource.PixelHeight);
            dg.Children.Add(new ImageDrawing(thumbnailBitmapSource, thumbnailRect));

            // オーバーレイアイコン
            //DrawingImage overlayDrawingImage = null;
            if (thumbnail.OverlayIndex > 0)
            {
                // オーバーレイアイコン取得
                var overlayBitmapSource = await GetOverlayIconBitmapSourceAsync(thumbnail);
                var overlayIconRect = new Rect(0, 0, overlayBitmapSource.PixelWidth, overlayBitmapSource.PixelHeight);

                var rect = overlayIconRect;
                if (thumbnail.OriginalWidth < 256)
                {
                    overlayIconRect.Offset(-2.0, thumbnailRect.Height - overlayIconRect.Height + 2.0);

                    rect.Offset(0, thumbnailRect.Height - overlayIconRect.Height);
                }
                else
                {
                    var scale = (thumbnail.OriginalWidth * 0.38)/overlayIconRect.Width;

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
            }

            if (dg.CanFreeze)
            {
                dg.Freeze();
            }

            updateImageSource.Invoke(new DrawingImage(dg));
        }

        #region Private Methods

        /// <summary>
        ///     取得するアイコンのサイズオプションを計算します。
        /// </summary>
        /// <returns></returns>
        private static int GetSizeOption(ShellThumbnail thumbnail)
        {
            var width = thumbnail.OriginalWidth;
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
        private static int GetOverlayIconSizeOption(ShellThumbnail thumbnail)
        {
            var width = thumbnail.OriginalWidth;
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

        private static Task<BitmapSource> GetThumbnailBitmapSourceAsync(ShellThumbnail thumbnail)
        {
            Contract.Requires(thumbnail != null);

            return Application.Current.Dispatcher.InvokeAsync(() =>
                System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    thumbnail.ImageHandle,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions())
                ).Task;
        }

        private static Task<BitmapSource> GetIconBitmapSourceAsync(ShellThumbnail thumbnail)
        {
            Contract.Requires(thumbnail != null);

            return Application.Current.Dispatcher.InvokeAsync(() =>
            {
                using (var imageList = new ShellImageList(GetSizeOption(thumbnail)))
                {
                    using (var icon = imageList.GetIcon(thumbnail.IconIndex))
                    {
                        return ShellIconFactory.CreateBitmapSource(icon);
                    }
                }
            }).Task;
        }

        private static Task<BitmapSource> GetOverlayIconBitmapSourceAsync(ShellThumbnail thumbnail)
        {
            Contract.Requires(thumbnail != null);

            return Application.Current.Dispatcher.InvokeAsync(() =>
            {
                using (var imageList = new ShellImageList(GetOverlayIconSizeOption(thumbnail)))
                {
                    using (var icon = imageList.GetOverlayIcon(thumbnail.OverlayIndex))
                    {
                        return ShellIconFactory.CreateBitmapSource(icon);
                    }
                }
            }).Task;
        }

        private static Task<BitmapSource> GetIconWithOverlayBitmapSourceAsync(ShellThumbnail thumbnail)
        {
            Contract.Requires(thumbnail != null);

            return Application.Current.Dispatcher.InvokeAsync(() =>
            {
                using (var imageList = new ShellImageList(GetSizeOption(thumbnail)))
                {
                    using (var icon = imageList.GetIcon(thumbnail.IconIndex, thumbnail.OverlayIndex))
                    {
                        return ShellIconFactory.CreateBitmapSource(icon);
                    }
                }
            }).Task;
        }

        #endregion
    }
}