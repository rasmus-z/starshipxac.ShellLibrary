using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Interop.Library;
using starshipxac.Shell.Properties;
using starshipxac.Shell.Resources;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define shell library class.
    /// </summary>
    public sealed class ShellLibrary : ShellFolder
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellLibrary" /> class
        ///     to the specified <see cref="ShellItem" />.
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />.</param>
        /// <param name="shellLibraryInterface">Shell library interface.</param>
        /// <param name="libraryName">Library name.</param>
        internal ShellLibrary(ShellItem shellItem, IShellLibraryNative shellLibraryInterface, string libraryName)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Requires<ArgumentNullException>(shellLibraryInterface != null);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));

            this.ShellLibraryInterface = shellLibraryInterface;
            this.DisplayName = libraryName;
        }

        internal IShellLibraryNative ShellLibraryInterface { get; set; }

        public override string Path => String.Empty;

        public override bool PathExists => false;

        /// <summary>
        ///     Get the library display name.
        /// </summary>
        public override string DisplayName { get; }

        /// <summary>
        ///     Get or set the icon resource.
        /// </summary>
        public IconReference IconReference
        {
            get
            {
                Contract.Ensures(Contract.Result<IconReference>() != null);

                string iconRef;
                this.ShellLibraryInterface.GetIcon(out iconRef);
                return new IconReference(iconRef);
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);

                this.ShellLibraryInterface.SetIcon(value.ReferencePath);
                this.ShellLibraryInterface.Commit();
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Get or set library type ID.
        /// </summary>
        public Guid LibraryType
        {
            get
            {
                Guid folderTypeGuid;
                this.ShellLibraryInterface.GetFolderType(out folderTypeGuid);
                return folderTypeGuid;
            }
            set
            {
                var guid = value;
                this.ShellLibraryInterface.SetFolderType(ref guid);
                this.ShellLibraryInterface.Commit();
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Get or set default save folder.
        /// </summary>
        public ShellFolder DefaultSaveFolder
        {
            get
            {
                Contract.Ensures(Contract.Result<ShellFolder>() != null);

                var guid = new Guid(ShellIID.IShellItem);
                IShellItem saveFolderItem;
                this.ShellLibraryInterface.GetDefaultSaveFolder(
                    DEFAULTSAVEFOLDERTYPE.DSFT_DETECT,
                    ref guid,
                    out saveFolderItem);
                return ShellFactory.FromShellItem(new ShellItem((IShellItem2)saveFolderItem)) as ShellFolder;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);

                if (!value.PathExists)
                {
                    throw new DirectoryNotFoundException(ErrorMessages.ShellLibraryDefaultSaveFolderNotFound);
                }

                this.ShellLibraryInterface.SetDefaultSaveFolder(
                    DEFAULTSAVEFOLDERTYPE.DSFT_DETECT,
                    value.ShellItem.ShellItemInterface);
                this.ShellLibraryInterface.Commit();
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Get or set a value indicating whether to pin the operation panel.
        /// </summary>
        public bool IsPinnedToNavigationPane
        {
            get
            {
                LIBRARYOPTIONFLAGS flags;
                this.ShellLibraryInterface.GetOptions(out flags);

                return (flags & LIBRARYOPTIONFLAGS.LOF_PINNEDTONAVPANE) == LIBRARYOPTIONFLAGS.LOF_PINNEDTONAVPANE;
            }
            set
            {
                var flags = LIBRARYOPTIONFLAGS.LOF_DEFAULT;
                if (value)
                {
                    flags |= LIBRARYOPTIONFLAGS.LOF_PINNEDTONAVPANE;
                }
                else
                {
                    flags &= ~LIBRARYOPTIONFLAGS.LOF_PINNEDTONAVPANE;
                }

                this.ShellLibraryInterface.SetOptions(LIBRARYOPTIONFLAGS.LOF_PINNEDTONAVPANE, flags);
                this.ShellLibraryInterface.Commit();
                RaisePropertyChanged();
            }
        }

        internal bool IsReadOnly => false;

        /// <summary>
        ///     Enumerate child folders.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ShellFolder> EnumerateFolders()
        {
            Contract.Ensures(Contract.Result<IEnumerable<ShellFolder>>() != null);

            var result = new List<ShellFolder>();

            var itemArray = default(IShellItemArray);
            try
            {
                var shellItemArrayGuid = new Guid(ShellIID.IShellItemArray);
                var hr = this.ShellLibraryInterface.GetFolders(
                    LIBRARYFOLDERFILTER.LFF_ALLITEMS,
                    ref shellItemArrayGuid,
                    out itemArray);
                if (HRESULT.Failed(hr))
                {
                    return result;
                }

                uint count;
                itemArray.GetCount(out count);

                for (uint index = 0; index < count; ++index)
                {
                    IShellItem shellItem;
                    itemArray.GetItemAt(index, out shellItem);
                    result.Add(new ShellFolder(new ShellItem((IShellItem2)shellItem)));
                }
            }
            finally
            {
                if (itemArray != null)
                {
                    Marshal.ReleaseComObject(itemArray);
                }
            }

            return result;
        }
    }
}