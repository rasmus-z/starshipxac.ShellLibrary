using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using starshipxac.Shell;

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

        /// <summary>
        /// <see cref="ShellViewModelFactory"/>を初期化します。
        /// </summary>
        public static void CreateFactory()
        {
        }

        public static async Task<ShellRootViewModel> CreateRootAsync()
        {
            Contract.Ensures(Contract.Result<ShellRootViewModel>() != null);

            return await ShellRootViewModel.CreateAsync();
        }

        /// <summary>
        /// <see cref="ShellFolder"/>から、フォルダーの<c>ViewModel</c>を作成します。
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static async Task<ShellFolderViewModel> CreateFolderAsync(ShellFolder folder)
        {
            Contract.Requires<ArgumentNullException>(folder != null);
            Contract.Ensures(Contract.Result<ShellFolderViewModel>() != null);

            return await ShellFolderViewModel.CreateAsync(folder);
        }

        /// <summary>
        /// <see cref="ShellObject"/>から、ファイルまたはフォルダーの<c>ViewModel</c>を作成します。
        /// </summary>
        /// <param name="shellObject"></param>
        /// <returns></returns>
        public static async Task<ShellObjectViewModel> CreateAsync(ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);
            Contract.Ensures(Contract.Result<ShellObjectViewModel>() != null);

            if (shellObject is ShellFolder)
            {
                return await ShellFolderViewModel.CreateAsync((ShellFolder)shellObject);
            }
            else if (shellObject is ShellFile)
            {
                return await ShellFileViewModel.CreateAsync((ShellFile)shellObject);
            }
            else
            {
                return await ShellNonFileSystemItemViewModel.CreateAsync(shellObject);
            }
        }
    }
}