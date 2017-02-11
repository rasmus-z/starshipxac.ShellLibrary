using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Interop.Library;
using starshipxac.Shell.Properties;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define shell library folder factory methods.
    /// </summary>
    public static class ShellLibraryFactory
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        internal static readonly Guid FOLDERID_Libraries = new Guid("1B3EA5DC-B587-4786-B4EF-BD1DC332AEAE");

        internal static readonly string FileExtension = ".library-ms";

        /// <summary>
        ///     Create new shell library.
        /// </summary>
        /// <param name="libraryName">Name of the library to be created.</param>
        /// <param name="overwrite">A value indicating whether to overwrite an existing library.</param>
        /// <returns>Created <see cref="ShellLibrary" />.</returns>
        public static ShellLibrary CreateLibrary(string libraryName, bool overwrite)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));
            Contract.Ensures(Contract.Result<ShellLibrary>() != null);

            var shellLibraryInterface = CreateShellLibraryNativeInterface();

            var guid = FOLDERID_Libraries;
            var flags = GetLibrarySaveOptions(overwrite);
            IShellItem2 shellItem2;
            shellLibraryInterface.SaveInKnownFolder(ref guid, libraryName, flags, out shellItem2);

            return new ShellLibrary(new ShellItem(shellItem2), shellLibraryInterface, libraryName);
        }

        /// <summary>
        ///     Create new shell library.
        /// </summary>
        /// <param name="libraryName">Name of the library to be created.</param>
        /// <param name="sourceKnownFolder">Known folder of library.</param>
        /// <param name="overwrite">A value indicating whether to overwrite an existing library.</param>
        /// <returns>Created <see cref="ShellLibrary" />.</returns>
        public static ShellLibrary Create(string libraryName, ShellKnownFolder sourceKnownFolder, bool overwrite)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));
            Contract.Requires<ArgumentNullException>(sourceKnownFolder != null);
            Contract.Ensures(Contract.Result<ShellLibrary>() != null);

            var shellLibraryInterface = CreateShellLibraryNativeInterface();

            var guid = sourceKnownFolder.FolderId;
            var flags = GetLibrarySaveOptions(overwrite);
            IShellItem2 shellItem2;
            shellLibraryInterface.SaveInKnownFolder(ref guid, libraryName, flags, out shellItem2);

            return new ShellLibrary(new ShellItem(shellItem2), shellLibraryInterface, libraryName);
        }

        /// <summary>
        ///     Create new shell library.
        /// </summary>
        /// <param name="libraryName">Name of the library to be created.</param>
        /// <param name="sourcePath">Library path.</param>
        /// <param name="overwrite">A value indicating whether to overwrite an existing library.</param>
        /// <returns>Created <see cref="ShellLibrary" />.</returns>
        public static ShellLibrary Create(string libraryName, string sourcePath, bool overwrite)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(sourcePath));
            Contract.Ensures(Contract.Result<ShellLibrary>() != null);

            if (!Directory.Exists(sourcePath))
            {
                throw new DirectoryNotFoundException(ErrorMessages.ShellLibraryFolderNotFound);
            }

            var guid = new Guid(ShellIID.IShellItem);
            var flags = GetLibrarySaveOptions(overwrite);
            IShellItem shellItemIn;
            ShellNativeMethods.SHCreateItemFromParsingName(sourcePath, IntPtr.Zero, ref guid, out shellItemIn);

            var shellLibraryInterface = CreateShellLibraryNativeInterface();

            IShellItem2 shellItem2;
            shellLibraryInterface.Save(shellItemIn, libraryName, flags, out shellItem2);

            return new ShellLibrary(new ShellItem(shellItem2), shellLibraryInterface, libraryName);
        }

        /// <summary>
        ///     Create a new instance of the <see cref="ShellLibrary" /> class
        ///     to the specified library name.
        /// </summary>
        /// <param name="libraryName">Name of the library to be created.</param>
        /// <param name="isReadOnly">A value that indicates whether the library is readonly.</param>
        /// <returns>Created <see cref="ShellLibrary" />.</returns>
        public static ShellLibrary Load(string libraryName, bool isReadOnly = true)
        {
            Contract.Requires<ArgumentNullException>(libraryName != null);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));
            Contract.Ensures(Contract.Result<ShellLibrary>() != null);

            var shellItemPath = Path.Combine(ShellKnownFolders.Libraries.Path, libraryName + FileExtension);

            IShellItem shellItem;
            var guid = new Guid(ShellIID.IShellItem);
            var hr = ShellNativeMethods.SHCreateItemFromParsingName(shellItemPath, IntPtr.Zero, ref guid, out shellItem);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            var shellLibraryInterface = CreateShellLibraryNativeInterface();

            var flags = isReadOnly ? STGM.STGM_READ : STGM.STGM_READWRITE;
            shellLibraryInterface.LoadLibraryFromItem(shellItem, flags);

            return new ShellLibrary(new ShellItem((IShellItem2)shellItem), shellLibraryInterface, libraryName);
        }

        /// <summary>
        ///     Create a new instance of the <see cref="ShellLibrary" /> class
        ///     to the specified known folder.
        /// </summary>
        /// <param name="sourceKnownFolder">Shell known folder.</param>
        /// <param name="isReadOnly">A value that indicates whether the library is readonly.</param>
        /// <returns>Created <see cref="ShellLibrary" />.</returns>
        public static ShellLibrary Load(ShellKnownFolder sourceKnownFolder, bool isReadOnly = true)
        {
            Contract.Requires<ArgumentNullException>(sourceKnownFolder != null);
            Contract.Ensures(Contract.Result<ShellLibrary>() != null);

            var shellItem = sourceKnownFolder.ShellItem.ShellItemInterface;
            var shellLibraryInterface = CreateShellLibraryNativeInterface();
            try
            {
                var guid = sourceKnownFolder.FolderId;
                var flags = isReadOnly ? STGM.STGM_READ : STGM.STGM_READWRITE;
                shellLibraryInterface.LoadLibraryFromKnownFolder(ref guid, flags);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ErrorMessages.ShellLibraryInvalidLibrary, ex);
            }

            var libraryShellItem = new ShellItem(shellItem);
            var libraryName = libraryShellItem.GetDisplayName();
            return new ShellLibrary(libraryShellItem, shellLibraryInterface, libraryName);
        }

        /// <summary>
        ///     Create a new instance of the <see cref="ShellLibrary" /> class
        ///     to the specified library name and folder path.
        /// </summary>
        /// <param name="libraryName">Name of the library to be created.</param>
        /// <param name="sourcePath">Folder path.</param>
        /// <param name="isReadOnly">A value that indicates whether the library is readonly.</param>
        /// <returns>Created <see cref="ShellLibrary" />.</returns>
        public static ShellLibrary Load(string libraryName, string sourcePath, bool isReadOnly = true)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(sourcePath));
            Contract.Ensures(Contract.Result<ShellLibrary>() != null);

            var shellItemPath = Path.Combine(sourcePath, libraryName + FileExtension);

            var shellLibraryInterface = CreateShellLibraryNativeInterface();

            var item = ShellFactory.FromFilePath(shellItemPath);
            var shellItem = item.ShellItem.ShellItemInterface;
            var flags = isReadOnly ? STGM.STGM_READ : STGM.STGM_READWRITE;
            shellLibraryInterface.LoadLibraryFromItem(shellItem, flags);

            return new ShellLibrary(new ShellItem(shellItem), shellLibraryInterface, libraryName);
        }

        /// <summary>
        ///     Create a new instance of the <see cref="ShellLibrary" /> class
        ///     to the specified <see cref="ShellItem" />.
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />.</param>
        /// <param name="isReadOnly">A value that indicates whether the library is readonly.</param>
        /// <returns>Created <see cref="ShellLibrary" />.</returns>
        internal static ShellLibrary FromShellItem(ShellItem shellItem, bool isReadOnly)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Ensures(Contract.Result<ShellLibrary>() != null);

            var shellLibraryInterface = CreateShellLibraryNativeInterface();
            var flags = isReadOnly ? STGM.STGM_READ : STGM.STGM_READWRITE;
            shellLibraryInterface.LoadLibraryFromItem(shellItem.ShellItemInterface, flags);

            var libraryName = shellItem.GetDisplayName();
            return new ShellLibrary(shellItem, shellLibraryInterface, libraryName);
        }

        /// <summary>
        ///     Create a new instance of the <see cref="IShellLibraryNative" /> interface.
        /// </summary>
        /// <returns></returns>
        private static IShellLibraryNative CreateShellLibraryNativeInterface()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return (IShellLibraryNative)new ShellLibraryCoClass();
        }

        /// <summary>
        ///     Get a <see cref="LIBRARYSAVEFLAGS" />.
        /// </summary>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        private static LIBRARYSAVEFLAGS GetLibrarySaveOptions(bool overwrite)
        {
            return overwrite ? LIBRARYSAVEFLAGS.LSF_OVERRIDEEXISTING : LIBRARYSAVEFLAGS.LSF_FAILIFTHERE;
        }
    }
}