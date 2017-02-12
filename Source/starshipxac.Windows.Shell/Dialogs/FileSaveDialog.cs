using System;
using System.Diagnostics.Contracts;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     Define file save dialog.
    /// </summary>
    public class FileSaveDialog : FileSaveDialogBase
    {
        private bool overwritePrompt = true;
        private bool createPrompt = false;
        private bool isExpandedMode = false;

        private ShellFile shellFile;

        /// <summary>
        ///     Initialize a new instance of the <see cref="FileSaveDialog" /> class.
        /// </summary>
        public FileSaveDialog()
        {
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="FileSaveDialog" /> class
        ///     to the specified dialog title.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        public FileSaveDialog(string title)
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
        ///     Get the selected file.
        /// </summary>
        public ShellFile ShellFile
        {
            get
            {
                if (this.shellFile == null)
                {
                    this.shellFile = GetShellFile();
                }
                return this.shellFile;
            }
        }

        /// <summary>
        ///     Show file save dialog.
        /// </summary>
        /// <returns>Dialog result.</returns>
        public FileDialogResult Show()
        {
            return ShowDialog();
        }

        /// <summary>
        ///     Show file save dialog.
        /// </summary>
        /// <param name="parentWindow">Parent window.</param>
        /// <returns>Dialog result.</returns>
        public FileDialogResult Show(Window parentWindow)
        {
            Contract.Requires<ArgumentNullException>(parentWindow != null);

            return ShowDialog(parentWindow);
        }

        /// <summary>
        ///     Get the <see cref="FileDialogOptions" />.
        /// </summary>
        /// <returns></returns>
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
    }
}