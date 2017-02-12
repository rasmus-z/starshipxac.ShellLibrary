using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     Define folder select dialog.
    /// </summary>
    public sealed class FolderSelectDialog : FolderSelectDialogBase
    {
        private IEnumerable<ShellFolder> shellFolders;

        /// <summary>
        ///     Initialize a new instance of the <see cref="FolderSelectDialog" /> class.
        /// </summary>
        public FolderSelectDialog()
        {
            this.Multiselect = false;
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="FolderSelectDialog" /> class
        ///     to the specified dialog title.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        public FolderSelectDialog(string title)
            : base(title)
        {
        }

        /// <summary>
        ///     <para>
        ///         Get or set a value indicating whether multiple folders can be selected.
        ///     </para>
        ///     <para>
        ///         フォルダーを複数選択可能かどうかを示す値を取得または設定します。
        ///     </para>
        /// </summary>
        public bool Multiselect { get; set; }

        /// <summary>
        ///     Get a collection of selected folders.
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
        ///     Show folder select dialog.
        /// </summary>
        /// <returns>Dialog result.</returns>
        public FileDialogResult Show()
        {
            return ShowDialog();
        }

        /// <summary>
        ///     Show folder select dialog.
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

            if (this.Multiselect)
            {
                result |= FileDialogOptions.MultiSelect;
            }

            return result;
        }
    }
}