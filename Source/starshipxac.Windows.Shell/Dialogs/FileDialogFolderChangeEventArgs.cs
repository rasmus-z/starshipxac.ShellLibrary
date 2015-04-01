using System;
using System.ComponentModel;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    /// フォルダ変更イベントデータを定義します。
    /// </summary>
    public class FileDialogFolderChangeEventArgs : CancelEventArgs
    {
        /// <summary>
        /// <see cref="FileDialogFolderChangeEventArgs"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="folder">フォルダー情報。</param>
        public FileDialogFolderChangeEventArgs(ShellFolder folder)
        {
            this.ShellFolder = folder;
        }

        /// <summary>
        /// フォルダー情報を取得します。
        /// </summary>
        public ShellFolder ShellFolder { get; private set; }
    }
}