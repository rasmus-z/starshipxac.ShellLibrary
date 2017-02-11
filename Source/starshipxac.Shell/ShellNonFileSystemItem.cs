using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define non file system item class.
    /// </summary>
    public class ShellNonFileSystemItem : ShellObject
    {
        private ShellFolder folder;

        internal ShellNonFileSystemItem(ShellItem shellItem)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
        }

        public ShellFolder Folder
        {
            get
            {
                if (this.folder == null)
                {
                    this.folder = GetFolder();
                }
                return this.folder;
            }
        }
    }
}