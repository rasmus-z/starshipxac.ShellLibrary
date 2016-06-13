using System;
using System.Diagnostics.Contracts;
using System.Windows;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellExplorerSample.ViewModels.Shell
{
    /// <summary>
    /// <see cref="ShellObject"/>の<c>ViewModel</c>を作成します。
    /// </summary>
    public static class ShellViewModelFactory
    {
        static ShellViewModelFactory()
        {
            // 注意！！
            // ここで初期化すると、スレッドが違うせいか、特定のフォルダーの取得で
            // InvalidOperationExceptionが発生する。

            //ContainerThumbnailFactory = new ShellThumbnailFactory(new Size(32, 32));
            //ThumbnailFactory = new ShellThumbnailFactory(new Size(64, 64));
        }

        public static ShellThumbnailFactory ContainerThumbnailFactory { get; private set; }
        public static ShellThumbnailFactory ThumbnailFactory { get; private set; }

        /// <summary>
        /// <see cref="ShellViewModelFactory"/>を初期化します。
        /// </summary>
        public static void CreateFactory()
        {
            ContainerThumbnailFactory = new ShellThumbnailFactory(new Size(16, 16));
            ThumbnailFactory = new ShellThumbnailFactory(new Size(64, 64));
        }

        public static ShellRootViewModel CreateRoot()
        {
            Contract.Ensures(Contract.Result<ShellRootViewModel>() != null);

            return new ShellRootViewModel(ContainerThumbnailFactory);
        }

        /// <summary>
        /// <see cref="ShellFolder"/>から、フォルダーの<c>ViewModel</c>を作成します。
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static ShellFolderViewModel CreateFolder(ShellFolder folder)
        {
            Contract.Requires<ArgumentNullException>(folder != null);
            Contract.Ensures(Contract.Result<ShellFolderViewModel>() != null);

            return new ShellFolderViewModel(folder, ContainerThumbnailFactory);
        }

        /// <summary>
        /// <see cref="ShellObject"/>から、ファイルまたはフォルダーの<c>ViewModel</c>を作成します。
        /// </summary>
        /// <param name="shellObject"></param>
        /// <returns></returns>
        public static ShellObjectViewModel Create(ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);
            Contract.Ensures(Contract.Result<ShellObjectViewModel>() != null);

            if (shellObject is ShellFolder)
            {
                return new ShellFolderViewModel((ShellFolder)shellObject, ThumbnailFactory);
            }
            else if (shellObject is ShellFile)
            {
                return new ShellFileViewModel((ShellFile)shellObject, ThumbnailFactory);
            }
            else
            {
                return new ShellNonFileSystemItemViewModel(shellObject, ThumbnailFactory);
            }
        }
    }
}