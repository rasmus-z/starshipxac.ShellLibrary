using System;
using System.Diagnostics.Contracts;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellExplorerSample.ViewModels.Shell
{
    /// <summary>
    ///     フォルダーを展開する前のダミーフォルダーを定義します。
    /// </summary>
    internal class PlaceholderViewModel : ShellFolderViewModel
    {
        /// <summary>
        ///     親フォルダーを指定して、
        ///     <see cref="PlaceholderViewModel" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        public PlaceholderViewModel(ShellThumbnailFactory thumbnailFactory)
            : base(thumbnailFactory)
        {
            Contract.Requires<ArgumentNullException>(thumbnailFactory != null);
        }
    }
}