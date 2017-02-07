using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;
using starshipxac.Shell.Internal;
using starshipxac.Shell.Interop.KnownFolder;
using starshipxac.Shell.Resources;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define shell known folder.
    /// </summary>
    public sealed class ShellKnownFolder : ShellFolder
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellKnownFolder" /> class
        ///     to the specified <see cref="ShellItem" />, <see cref="IKnownFolder" /> and <see cref="FolderProperties" />.
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />.</param>
        /// <param name="knownFolderInterface"><see cref="IKnownFolder" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     <para>
        ///         <paramref name="=shellItem" /> is a <c>null</c> reference.
        ///     </para>
        ///     or
        ///     <para>
        ///         <paramref name="knownFolderInterface" /> is a <c>null</c> reference.
        ///     </para>
        /// </exception>
        internal ShellKnownFolder(ShellItem shellItem, IKnownFolder knownFolderInterface)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Requires<ArgumentNullException>(knownFolderInterface != null);

            this.KnownFolderInterface = knownFolderInterface;
            this.KnownFolderProperties = new FolderProperties(knownFolderInterface);
        }

        /// <summary>
        ///     Release all resources used by <see cref="ShellKnownFolder" />,
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
                if (disposing)
                {
                    // Release managed resources.
                }

                // Release unmanaged resources.
                Marshal.ReleaseComObject(this.KnownFolderInterface);
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        internal IKnownFolder KnownFolderInterface { get; }

        internal FolderProperties KnownFolderProperties { get; }

        /// <summary>
        ///     Get the path on the file system of the known folder.
        /// </summary>
        public override string Path => this.KnownFolderProperties.Path;

        /// <summary>
        ///     Get a value that determines whether or not a path on the file system exists.
        /// </summary>
        public override bool PathExists => this.KnownFolderProperties.PathExists;

        /// <summary>
        ///     Get the folder category.
        /// </summary>
        public KnownFolderCategories Category => this.KnownFolderProperties.Category;

        /// <summary>
        ///     Get a canonical name.
        /// </summary>
        public string CanonicalName => this.KnownFolderProperties.CanonicalName;

        /// <summary>
        ///     Get a folder description.
        /// </summary>
        public string Description => this.KnownFolderProperties.Description;

        /// <summary>
        ///     Get a parent folder <see cref="Guid"/>.
        /// </summary>
        public Guid ParentId => this.KnownFolderProperties.ParentId;

        /// <summary>
        ///     Get the relative path.
        /// </summary>
        public string RelativePath => this.KnownFolderProperties.RelativePath;

        /// <summary>
        ///     Get the tool tip text.
        /// </summary>
        public string ToolTip => this.KnownFolderProperties.ToolTip;

        public StringReference ToolTipReference => this.KnownFolderProperties.ToolTipResource;

        /// <summary>
        ///     Get the localized name.
        /// </summary>
        public string LocalizedName => this.KnownFolderProperties.LocalizedName;

        /// <summary>
        ///     Get security information.
        /// </summary>
        public string Security => this.KnownFolderProperties.Security;

        /// <summary>
        ///     Get the attribute.
        /// </summary>
        public FileAttributes Attributes => this.KnownFolderProperties.FileAttributes;

        /// <summary>
        ///     Get the behavior of the known folder.
        /// </summary>
        public FolderDefinitionFlags FolderDefinitionFlag => this.KnownFolderProperties.FolderDefinitionFlags;

        /// <summary>
        ///     Get the folder type ID.
        /// </summary>
        internal Guid FolderTypeId => this.KnownFolderProperties.FolderTypeId;

        /// <summary>
        ///     Get the folder type.
        /// </summary>
        public string FolderType => this.KnownFolderProperties.FolderType;

        /// <summary>
        ///     Get the folder ID.
        /// </summary>
        internal Guid FolderId => this.KnownFolderProperties.FolderId;

        /// <summary>
        ///     Get <see cref="RedirectionCapability" />.
        /// </summary>
        public RedirectionCapability Redirection => this.KnownFolderProperties.Redirection;
    }
}