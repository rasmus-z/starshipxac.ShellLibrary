using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     フォルダー選択するダイアログを表示します。
    /// </summary>
    public sealed class FolderSelectDialog : FolderSelectDialogBase
    {
        private IEnumerable<ShellFolder> shellFolders;

        /// <summary>
        ///     <see cref="FolderSelectDialog" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        public FolderSelectDialog()
        {
            this.Multiselect = false;
        }

        /// <summary>
        ///     ダイアログのタイトルを指定して、
        ///     <see cref="FolderSelectDialog" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="title">ダイアログタイトル。</param>
        public FolderSelectDialog(string title)
            : base(title)
        {
        }

        /// <summary>
        ///     フォルダーを複数選択可能かどうかを示す値を取得または設定します。
        /// </summary>
        public bool Multiselect { get; set; }

        /// <summary>
        ///     選択したフォルダーのコレクションを取得します。
        /// </summary>
        public IEnumerable<ShellFolder> ShellFolders
        {
            get
            {
                if (this.shellFolders == null)
                {
                    this.shellFolders = GetShellFolders();
                }
                return this.shellFolders;
            }
        }

        /// <summary>
        ///     フォルダー選択ダイアログを表示します。
        /// </summary>
        /// <returns>ダイアログ実行結果。</returns>
        public FileDialogResult Show()
        {
            return ShowDialog();
        }

        /// <summary>
        ///     親ウィンドウを指定して、フォルダー選択ダイアログを表示します。
        /// </summary>
        /// <param name="parentWindow">親ウィンドウ。</param>
        /// <returns>ダイアログ実行結果。</returns>
        public FileDialogResult Show(Window parentWindow)
        {
            Contract.Requires<ArgumentNullException>(parentWindow != null);

            return ShowDialog(parentWindow);
        }

        protected override FileDialogOptions GetDialogOptions()
        {
            var result = base.GetDialogOptions();

            if (this.Multiselect)
            {
                result |= FileDialogOptions.MultiSelect;
            }

            return result;
        }
    }
}