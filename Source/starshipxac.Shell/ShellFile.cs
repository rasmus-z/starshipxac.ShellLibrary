using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.PropertySystem;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define shell file class.
    /// </summary>
    public class ShellFile : ShellObject
    {
        private string extension;
        private ShellProperty<UInt64?> sizeProperty;
        private ShellFolder folder;

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellFile" /> class
        ///     to the specified <see cref="ShellItem" />.
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />.</param>
        protected internal ShellFile(ShellItem shellItem)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
        }

        /// <summary>
        ///     Get the file path.
        /// </summary>
        public virtual string Path
        {
            get
            {
                return this.ParsingName;
            }
        }

        /// <summary>
        ///     Get the file extension.
        /// </summary>
        public string Extension
        {
            get
            {
                if (this.extension == null)
                {
                    this.extension = System.IO.Path.GetExtension(this.Path);
                }
                return this.extension;
            }
        }

        /// <summary>
        ///     Get a value that determines if it is a stream.
        /// </summary>
        public bool IsStream => this.ShellItem.IsStream;

        /// <summary>
        ///     Get the file size.
        /// </summary>
        public UInt64 Size
        {
            get
            {
                if (this.sizeProperty == null)
                {
                    this.sizeProperty = new ShellProperty<UInt64?>(this, "System.Size");
                }

                return this.sizeProperty.Value.GetValueOrDefault(0);
            }
        }

        /// <summary>
        ///     Get an instance of the folder.
        /// </summary>
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