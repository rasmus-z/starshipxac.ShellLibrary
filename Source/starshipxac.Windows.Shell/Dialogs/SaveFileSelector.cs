using System;
using System.Threading.Tasks;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    /// 保存するファイルを選択するダイアログを表示します。
    /// </summary>
    public sealed class SaveFileSelector : FileSaveDialogBase
    {
        private bool overwritePrompt = true;
        private bool createPrompt = false;
        private bool isExpandedMode = false;

        /// <summary>
        /// <see cref="SaveFileSelector"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        public SaveFileSelector()
        {
        }

        /// <summary>
        /// ダイアログのタイトルを指定して、
        /// <see cref="SaveFileSelector"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="title">ダイアログのタイトル。</param>
        public SaveFileSelector(string title)
            : base(title)
        {
        }

        /// <summary>
        /// ユーザーが、すでに存在するファイルを指定した場合に、
        /// 警告メッセージを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool OverwritePrompt
        {
            get
            {
                return this.overwritePrompt;
            }
            set
            {
                ThrowIfDialogShowingPropertyCannotBeChanged();
                this.overwritePrompt = value;
            }
        }

        /// <summary>
        /// ユーザーが、存在しないファイルを指定した場合に、
        /// ファイルを作成することを確認するメッセージを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool CreatePrompt
        {
            get
            {
                return this.createPrompt;
            }
            set
            {
                ThrowIfDialogShowingPropertyCannotBeChanged();
                this.createPrompt = value;
            }
        }

        /// <summary>
        /// ダイアログを拡張モードで表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool IsExpandedMode
        {
            get
            {
                return this.isExpandedMode;
            }
            set
            {
                ThrowIfDialogShowingPropertyCannotBeChanged();
                this.isExpandedMode = value;
            }
        }

        /// <summary>
        /// 保存するファイルを選択するダイアログを表示します。
        /// </summary>
        /// <returns>保存するファイル。</returns>
        public async Task<ShellFile> SelectSaveFileAsync()
        {
            ShellFile result = null;

            await InvokeAsync(() =>
            {
                var dialogResult = ShowDialog();
                if (dialogResult == FileDialogResult.Ok)
                {
                    result = GetShellFile();
                }
            });

            return result;
        }

        protected override FileDialogOptions GetDialogOptions()
        {
            var result = base.GetDialogOptions();

            if (this.OverwritePrompt)
            {
                result |= FileDialogOptions.OverwritePrompt;
            }
            if (this.CreatePrompt)
            {
                result |= FileDialogOptions.CreatePrompt;
            }
            if (!this.IsExpandedMode)
            {
                result |= FileDialogOptions.ExpandMode;
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