using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Interop.KnownFolder;
using starshipxac.Shell.Properties;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define a method to create a <see cref="ShellObject" />.
    /// </summary>
    public static class ShellFactory
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellObject" /> class
        ///     to the specified parsing name.
        /// </summary>
        /// <param name="parsingName"><c>ParsingName</c>.</param>
        /// <returns>Created <see cref="ShellObject" />.</returns>
        public static ShellObject FromParsingName(string parsingName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(parsingName));
            Contract.Ensures(Contract.Result<ShellObject>() != null);

            return Create(ShellItem.FromParsingName(parsingName));
        }

        /// <summary>
        ///     Create a new instance of the <see cref="ShellFile" /> class
        ///     to the specified file path.
        /// </summary>
        /// <param name="path">File path.</param>
        /// <returns>Created <see cref="ShellFile" />.</returns>
        public static ShellFile FromFilePath(string path)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(path));
            Contract.Ensures(Contract.Result<ShellFile>() != null);

            var absPath = GetAbsolutePath(path);
            if (!File.Exists(absPath))
            {
                throw new FileNotFoundException(String.Format(CultureInfo.CurrentUICulture,
                    ErrorMessages.FilePathNotExist, path), path);
            }

            return new ShellFile(ShellItem.FromParsingName(absPath));
        }

        /// <summary>
        ///     Create a new instance of the <see cref="ShellFolder" /> class
        ///     to the specified folder path.
        /// </summary>
        /// <param name="path">Folder path.</param>
        /// <returns>Created <see cref="ShellFolder" />.</returns>
        public static ShellFolder FromFolderPath(string path)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(path));
            Contract.Ensures(Contract.Result<ShellFolder>() != null);

            var absPath = GetAbsolutePath(path);
            if (!Directory.Exists(absPath))
            {
                throw new DirectoryNotFoundException(String.Format(CultureInfo.InvariantCulture,
                    ErrorMessages.FilePathNotExist, path));
            }

            return new ShellFolder(ShellItem.FromParsingName(absPath));
        }

        /// <summary>
        ///     Create a new instance of the <see cref="ShellObject" /> class
        ///     to the specified <see cref="ShellItem" />.
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />.</param>
        /// <returns>Created <see cref="ShellObject" />.</returns>
        public static ShellObject FromShellItem(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Ensures(Contract.Result<ShellObject>() != null);

            return Create(shellItem);
        }

        /// <summary>
        ///     Create a new instance of the <see cref="ShellFolder" /> class
        ///     to the specified <see cref="ShellItem" />.
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />.</param>
        /// <returns>
        ///     Created <see cref="ShellFolder" />.
        ///     If <paramref name="shellItem" /> is not a folder, it returns <c>null</c>.
        /// </returns>
        public static ShellFolder FromShellFolderItem(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);

            if (!shellItem.IsFolder)
            {
                return null;
            }

            return CreateFolder(shellItem);
        }

        /// <summary>
        ///     <para>
        ///         Create a new instance of a <see cref="ShellObject" /> derived class
        ///         that best matches the nature of the specified <see cref="ShellItem" />.
        ///     </para>
        ///     <para>
        ///         指定した<see cref="ShellItem" />の性質に最も一致する<see cref="ShellObject" />派生クラスのインスタンスを作成します。
        ///     </para>
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />.</param>
        /// <returns>Created a new instance of <see cref="ShellObject" /> derived class.</returns>
        private static ShellObject Create(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Ensures(Contract.Result<ShellObject>() != null);

            if (shellItem.IsLink)
            {
                // Link(Shor cut file)
                return new ShellLink(shellItem);
            }
            else if (shellItem.IsFolder)
            {
                // Folder
                return CreateFolder(shellItem);
            }
            else if (shellItem.IsFileSystem)
            {
                // File
                return new ShellFile(shellItem);
            }
            else if (shellItem.IsStream)
            {
                return new ShellFile(shellItem);
            }

            // Non file system item
            return new ShellNonFileSystemItem(shellItem);
        }

        /// <summary>
        ///     Create a new instance of a <see cref="ShellFolder" /> derived class
        ///     that best matches the nature of the specified <see cref="ShellItem" />.
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />.</param>
        /// <returns>Created a new instance of <see cref="ShellFolder" /> derived class.</returns>
        private static ShellFolder CreateFolder(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);

            var shellLibrary = ShellLibraryFactory.FromShellItem(shellItem, true);
            if (SameItemType(shellItem.GetItemType(), ShellLibraryFactory.FileExtension) && (shellLibrary != null))
            {
                // Library
                return shellLibrary;
            }
            else if (SameItemType(shellItem, ShellSearchConnector.FileExtension))
            {
                // Search connector
                return new ShellSearchConnector(shellItem);
            }
            else if (SameItemType(shellItem, ShellSavedSearchCollection.FileExtension))
            {
                // Saved search collection
                return new ShellSavedSearchCollection(shellItem);
            }
            else
            {
                var knownFolderNative = GetKnownFolderNative(shellItem);
                if (knownFolderNative != null)
                {
                    // Known folder
                    return new ShellKnownFolder(shellItem, knownFolderNative);
                }
            }

            // Folder
            return new ShellFolder(shellItem);
        }

        /// <summary>
        ///     Determines whether specified item type string and extension string have same value.
        /// </summary>
        /// <param name="itemType">The item type string to compare, or <c>null</c>.</param>
        /// <param name="extension">The extension string to compare, or <c>null</c>.</param>
        /// <returns>
        ///     <c>true</c> if the value of <paramref name="itemType" /> is the same as the value of <paramref name="extension" />;
        ///     otherwise <c>false</c>.
        ///     If <paramref name="itemType" /> and <paramref name="extension" /> are <c>null</c>, the method returns <c>true</c>.
        /// </returns>
        public static bool SameItemType(string itemType, string extension)
        {
            return String.Equals(itemType, extension, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Determines wheter specified <see cref="ShellItem" /> item type and extension string have same item type.
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />.</param>
        /// <param name="extension">The extensionstring to compare, or <c>null</c>.</param>
        /// <returns>
        ///     <c>true</c> if the item type of <see cref="ShellItem" /> is the same as the value of <paramref name="extension" />;
        ///     otherwise <c>false</c>.
        /// </returns>
        private static bool SameItemType(ShellItem shellItem, string extension)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);

            return SameItemType(shellItem.GetItemType(), extension);
        }

        /// <summary>
        ///     If <paramref name="shellItem" /> is known folder, create a new instance of the <see cref="IKnownFolder" /> derived
        ///     class.
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />.</param>
        /// <returns>Created a new instance of <see cref="IKnownFolder" /> derived class.</returns>
        private static IKnownFolder GetKnownFolderNative(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);

            var pidl = PIDL.FromShellItem(shellItem.ShellItemInterface);
            if (pidl.IsNull)
            {
                return null;
            }

            return ShellKnownFolderFactory.FromPIDL(pidl);
        }

        /// <summary>
        ///     Returns the absolute path for the specified path string.
        /// </summary>
        /// <param name="path">Path string.</param>
        /// <returns>Absolute path string.</returns>
        /// <exception cref="ArgumentException"><paramref name="path" /> is <c>null</c> or empty string.</exception>
        private static string GetAbsolutePath(string path)
        {
            Contract.Requires(!String.IsNullOrWhiteSpace(path));

            if (Uri.IsWellFormedUriString(path, UriKind.Absolute))
            {
                return path;
            }
            return Path.GetFullPath(path);
        }
    }
}