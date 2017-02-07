using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;
using starshipxac.Shell.Internal;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define shell folder class.
    /// </summary>
    public class ShellFolder : ShellObject
    {
        private ShellFolder parent;

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellFolder" /> class
        ///     to the specified <see cref="ShellItem" />.
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />.</param>
        /// <remarks>
        ///     <para>
        ///         <See cref="ShellItem.IsFolder" /> is not necessarily <c>true</c>.
        ///         For some <c>KnownFolder</c>, <see cref="ShellItem.IsFolder" /> may be <c>false</c>.
        ///     </para>
        /// </remarks>
        internal ShellFolder(ShellItem shellItem)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);

            this.ShellFolderInterface = shellItem.GetShellFolder();
        }

        /// <summary>
        ///     Release all resources used by <see cref="ShellObject" />,
        ///     and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources.
        ///     <c>false</c> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                // Release unmanaged resources.
                Marshal.ReleaseComObject(this.ShellFolderInterface);
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        ///     Get a <see cref="IShellFolder" />.
        /// </summary>
        internal IShellFolder ShellFolderInterface { get; }

        /// <summary>
        ///     Get the parsing name.
        /// </summary>
        public virtual string Path => this.ParsingName;

        /// <summary>
        ///     Get a value that determines whether or not a path on the file system exists.
        /// </summary>
        public virtual bool PathExists => this.IsFileSystem && Directory.Exists(this.Path);

        /// <summary>
        ///     Get a parent folder.
        /// </summary>
        public ShellFolder Parent
        {
            get
            {
                if (this.parent == null)
                {
                    this.parent = GetFolder();
                }
                return this.parent;
            }
        }

        /// <summary>
        ///     Get a collection of <see cref="ShellObject" /> that exists in <see cref="ShellFolder" />.
        /// </summary>
        /// <returns>Collection of <see cref="ShellObject" />.</returns>
        public virtual IEnumerable<ShellObject> EnumerateObjects()
        {
            return new ShellItems(new ShellFolderEnumerator(this));
        }

        /// <summary>
        ///     Get a collection of <see cref="ShellFile" /> that exists in <see cref="ShellFolder" />.
        /// </summary>
        /// <returns>Collection of <see cref="ShellFile" />.</returns>
        public virtual IEnumerable<ShellObject> EnumerateFiles()
        {
            return new ShellItems(new ShellFolderEnumerator(this, SHCONTF.SHCONTF_NONFOLDERS));
        }

        /// <summary>
        ///     Get a collection of <see cref="ShellFolder" /> that exists in <see cref="ShellFolder" />.
        /// </summary>
        /// <returns>Collection of <see cref="ShellFolder" />.</returns>
        public virtual IEnumerable<ShellFolder> EnumerateFolders()
        {
            return new ShellFolders(new ShellFolderEnumerator(this, SHCONTF.SHCONTF_FOLDERS));
        }
    }
}