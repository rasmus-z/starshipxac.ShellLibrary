using System;
using System.Threading.Tasks;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     Define dialog for selecting a save file.
    /// </summary>
    public sealed class SaveFileSelector : FileSaveDialogBase
    {
        private bool overwritePrompt = true;
        private bool createPrompt = false;
        private bool isExpandedMode = false;

        /// <summary>
        ///     Initialize a new instance of the <see cref="SaveFileSelector" /> class.
        /// </summary>
        public SaveFileSelector()
        {
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="SaveFileSelector" /> class
        ///     to the specified dialog tilte.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        public SaveFileSelector(string title)
            : base(title)
        {
        }

        /// <summary>
        ///     <para>
        ///         Get or set a value indicating whether to display a warning message
        ///         if the user specifies a file that already exists.
        ///     </para>
        ///     <para>
        ///         ユーザーが、すでに存在するファイルを指定した場合に、
        ///         警告メッセージを表示するかどうかを示す値を取得または設定します。
        ///     </para>
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
        ///     <para>
        ///         Get or set a value indicating whether or not to display a message confirming the creation of a file
        ///         if the user specifies a file that does not exist.
        ///     </para>
        ///     <para>
        ///         ユーザーが、存在しないファイルを指定した場合に、
        ///         ファイルを作成することを確認するメッセージを表示するかどうかを示す値を取得または設定します。
        ///     </para>
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
        ///     <para>
        ///         Get or set a value indicating whether the dialog is displayed in extended mode.
        ///     </para>
        ///     <para>
        ///         ダイアログを拡張モードで表示するかどうかを示す値を取得または設定します。
        ///     </para>
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
        ///     Displays a dialog for selecting single save file.
        /// </summary>
        /// <returns>The selected save file. If the user canceled it will return <c>null</c>.</returns>
        public async Task<ShellFile> SelectSingleFileAsync()
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

        /// <summary>
        ///     Get the <see cref="FileDialogOptions" />.
        /// </summary>
        /// <returns><see cref="FileDialogOptions"/>.</returns>
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