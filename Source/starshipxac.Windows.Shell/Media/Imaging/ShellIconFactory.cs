using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Media.Imaging;
using starshipxac.Shell.Media.Imaging;

namespace starshipxac.Windows.Shell.Media.Imaging
{
    /// <summary>
    ///     ファイルアイコンのイメージを作成します。
    /// </summary>
    public static class ShellIconFactory
    {
        public static BitmapSource CreateBitmapSource(ShellIcon icon)
        {
            Contract.Requires<ArgumentNullException>(icon != null);

            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
    }
}