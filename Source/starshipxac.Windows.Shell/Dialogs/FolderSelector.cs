using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    /// フォルダーを選択するダイアログを表示します。
    /// </summary>
    public sealed class FolderSelector : FolderSelectDialogBase
    {
        private bool multiSelect = false;

        /// <summary>
        /// <see cref="FolderSelector"/>クラスを初期化します。
        /// </summary>
        static FolderSelector()
        {
            EmptyShellFolders = new List<ShellFolder>();
        }

        /// <summary>
        /// <see cref="FolderSelector"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        public FolderSelector()
        {
        }

        /// <summary>
        /// ダイアログのタイトルを指定して、
        /// <see cref="FolderSelector"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="title">ダイアログのタイトル。</param>
        public FolderSelector(string title)
            : base(title)
        {
        }

        /// <summary>
        /// 空のフォルダコレクションを取得または設定します。
        /// </summary>
        private static IEnumerable<ShellFolder> EmptyShellFolders { get; set; }

        /// <summary>
        /// 1つのフォルダを選択するダイアログを表示します。
        /// </summary>
        /// <returns>選択したフォルダ。ユーザーがキャンセルした場合は<c>null</c>。</returns>
        public async Task<ShellFolder> SelectSingleFolderAsync()
        {
            ShellFolder result = null;

            this.multiSelect = false;

            await InvokeAsync(() =>
            {
                var dialogResult = ShowDialog();
                if (dialogResult == FileDialogResult.Ok)
                {
                    result = GetShellFolders().FirstOrDefault();
                }
            });

            return result;
        }

        /// <summary>
        /// 複数のフォルダを選択するダイアログを表示します。
        /// </summary>
        /// <returns>選択したフォルダのコレクション。</returns>
        public async Task<IEnumerable<ShellFolder>> SelectMultipleFoldersAsync()
        {
            Contract.Ensures(Contract.Result<IEnumerable<ShellFolder>>() != null);

            var result = EmptyShellFolders;

            this.multiSelect = true;

            await InvokeAsync(() =>
            {
                var dialogResult = ShowDialog();
                if (dialogResult == FileDialogResult.Ok)
                {
                    result = GetShellFolders();
                }
            });

            return result;
        }

        protected override FileDialogOptions GetDialogOptions()
        {
            var result = base.GetDialogOptions();

            if (this.multiSelect)
            {
                result |= FileDialogOptions.MultiSelect;
            }

            return result;
        }

        private async Task InvokeAsync(Action action)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                await Application.Current.Dispatcher.InvokeAsync(action);
            }
        }
    }
}