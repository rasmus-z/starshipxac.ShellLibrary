using System;
using System.Diagnostics.Contracts;

namespace ShellExplorerSample.ViewModels.Shell
{
    /// <summary>
    /// フォルダーを展開する前のダミーフォルダーを定義します。
    /// </summary>
    internal class PlaceholderViewModel : ShellFolderViewModel
    {
        /// <summary>
        /// 親フォルダーを指定して、
        /// <see cref="PlaceholderViewModel"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="parentFolder"></param>
        public PlaceholderViewModel(ShellFolderViewModel parentFolder)
            : base(parentFolder)
        {
            Contract.Requires<ArgumentNullException>(parentFolder != null);
        }
    }
}