using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     ファイル選択ダイアログを表示します。
    /// </summary>
    public sealed class OpenFileSelector : FileOpenDialogBase
    {
        private bool multiSelect = false;

        /// <summary>
        ///     <see cref="OpenFileSelector" />クラスを初期化します。
        /// </summary>
        static OpenFileSelector()
        {
            EmptyShellFiles = new List<ShellFile>();
        }

        /// <summary>
        ///     <see cref="OpenFileSelector" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        public OpenFileSelector()
        {
        }

        /// <summary>
        ///     ダイアログのタイトルを指定して、
        ///     <see cref="OpenFileSelector" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="title">ダイアログのタイトル。</param>
        public OpenFileSelector(string title)
            : base(title)
        {
        }

        /// <summary>
        ///     読み込み専用ファイルのみ選択可能にするかどうかを示す値を取得または設定します。
        /// </summary>
        public bool EnsureReadOnly { get; set; }

        /// <summary>
        ///     ファイルシステム以外のアイテムを選択可能にするかどうかを示す値を取得または設定します。
        /// </summary>
        public bool AllowNonFileSystemItem { get; set; }

        /// <summary>
        ///     空のファイルコレクションを取得または設定します。
        /// </summary>
        private static IEnumerable<ShellFile> EmptyShellFiles { get; }

        /// <summary>
        ///     1つのファイルを選択できるダイアログを表示します。
        /// </summary>
        /// <returns>選択したファイル情報。ユーザーがキャンセルした場合は<c>null</c>。</returns>
        public async Task<ShellFile> SelectSingleFileAsync()
        {
            ShellFile result = null;

            this.multiSelect = false;

            await InvokeAsync(() =>
            {
                var dialogResult = ShowDialog();
                if (dialogResult == FileDialogResult.Ok)
                {
                    result = GetShellFiles().FirstOrDefault();
                }
            });

            return result;
        }

        /// <summary>
        ///     複数のファイルを選択できるダイアログを表示します。
        /// </summary>
        /// <returns>選択したファイル情報のコレクション。</returns>
        public async Task<IEnumerable<ShellFile>> SelectMultipleFilesAsync()
        {
            var result = EmptyShellFiles;

            this.multiSelect = true;

            await InvokeAsync(() =>
            {
                var dialogResult = ShowDialog();
                if (dialogResult == FileDialogResult.Ok)
                {
                    result = GetShellFiles();
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
            if (this.EnsureReadOnly)
            {
                result |= FileDialogOptions.EnsureReadOnly;
            }
            if (!this.AllowNonFileSystemItem)
            {
                result |= FileDialogOptions.ForceFileSystem;
            }
            else
            {
                result |= FileDialogOptions.AllNonStotageItems;
            }

            return result;
        }

        private static async Task InvokeAsync(Action action)
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