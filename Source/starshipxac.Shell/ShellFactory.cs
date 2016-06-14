using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using starshipxac.Shell.Internal;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Interop.KnownFolder;
using starshipxac.Shell.Properties;

namespace starshipxac.Shell
{
    /// <summary>
    ///     <see cref="ShellObject" />を作成するメソッドを定義します。
    /// </summary>
    [ContractClass(typeof(ShellFactoryContract))]
    public abstract class ShellFactory
    {
        private static ShellFactory factory;

        static ShellFactory()
        {
            factory = DefaultShellFactory.Default;
        }

        /// <summary>
        ///     ファクトリクラスのインスタンスを設定します。
        /// </summary>
        /// <param name="shellFactory"></param>
        public static void SetFactory(ShellFactory shellFactory)
        {
            Contract.Requires<ArgumentNullException>(shellFactory != null);

            factory = shellFactory;
        }

        /// <summary>
        ///     ファクトリを基底のファクトリークラスに再設定します。
        /// </summary>
        public static void ResetFactory()
        {
            factory = DefaultShellFactory.Default;
        }

        /// <summary>
        ///     解析名(<c>ParsingName</c>)を指定して、<see cref="ShellObject" />クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="parsingName"><c>ParsingName</c>。</param>
        /// <returns>作成した<see cref="ShellObject" />。</returns>
        public static ShellObject FromParsingName(string parsingName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(parsingName));
            Contract.Ensures(Contract.Result<ShellObject>() != null);

            return factory.Create(ShellItem.FromParsingName(parsingName));
        }

        /// <summary>
        ///     ファイルのパスを指定して、<see cref="ShellFile" />クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="path">ファイルのパス。</param>
        /// <returns></returns>
        public static ShellFile FromFilePath(string path)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(path));
            Contract.Ensures(Contract.Result<ShellFile>() != null);

            var absPath = ShellItem.GetAbsolutePath(path);
            if (!File.Exists(absPath))
            {
                throw new FileNotFoundException(String.Format(CultureInfo.CurrentUICulture,
                    ErrorMessages.FilePathNotExist, path), path);
            }

            return new ShellFile(ShellItem.FromParsingName(absPath));
        }

        /// <summary>
        ///     フォルダーのパスを指定して、<see cref="ShellFolder" />クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="path">フォルダーのパス。</param>
        /// <returns></returns>
        public static ShellFolder FromFolderPath(string path)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(path));
            Contract.Ensures(Contract.Result<ShellFolder>() != null);

            var absPath = ShellItem.GetAbsolutePath(path);
            if (!Directory.Exists(absPath))
            {
                throw new DirectoryNotFoundException(String.Format(CultureInfo.InvariantCulture,
                    ErrorMessages.FilePathNotExist, path));
            }

            return new ShellFolder(ShellItem.FromParsingName(absPath));
        }

        /// <summary>
        ///     <see cref="ShellItem" />を指定して、
        ///     <see cref="ShellObject" />クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />。</param>
        /// <returns></returns>
        public static ShellObject FromShellItem(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Ensures(Contract.Result<ShellObject>() != null);

            return factory.Create(shellItem);
        }

        /// <summary>
        ///     指定した<see cref="ShellItem" />の性質に最も一致する<see cref="ShellObject" />派生クラスのインスタンスを作成します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />。</param>
        /// <returns>作成した<see cref="ShellObject" />派生クラスのインスタンス。</returns>
        public ShellObject Create(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Ensures(Contract.Result<ShellObject>() != null);

            if (shellItem.IsLink)
            {
                // リンク(ショートカットファイル)
                return new ShellLink(shellItem);
            }

            if (shellItem.IsFolder)
            {
                // フォルダー
                return CreateFolder(shellItem);
            }

            if (shellItem.IsFileSystem)
            {
                // ファイル
                return CreateShellFile(shellItem);
            }

            if (shellItem.IsStream)
            {
                return CreateShellFile(shellItem);
            }

            // ファイルシステム外のアイテム
            return new ShellNonFileSystemItem(shellItem);
        }

        /// <summary>
        ///     指定した<see cref="ShellItem" />の性質に最も一致する<see cref="ShellFolder" />派生クラスのインスタンスを作成します。
        /// </summary>
        /// <param name="shellItem"></param>
        /// <returns></returns>
        public ShellFolder CreateFolder(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);

            // フォルダー
            var shellLibrary = ShellLibraryFactory.FromShellItem(shellItem, true);
            if (SameItemType(shellItem.ItemType, ShellLibraryFactory.FileExtension) && (shellLibrary != null))
            {
                // ライブラリ
                return shellLibrary;
            }

            if (SameItemType(shellItem, ShellSearchConnector.FileExtension))
            {
                // 検索条件
                return new ShellSearchConnector(shellItem);
            }

            if (SameItemType(shellItem, ShellSavedSearchCollection.FileExtension))
            {
                // 保存された検索条件
                return new ShellSavedSearchCollection(shellItem);
            }

            var knownFolderNative = GetKnownFolderNative(shellItem);
            if (knownFolderNative != null)
            {
                // 標準フォルダー
                return new ShellKnownFolder(shellItem, knownFolderNative);
            }

            // フォルダー
            return CreateShellFolder(shellItem);
        }

        /// <summary>
        ///     指定した<see cref="ShellItem" />の性質に最も一致する<see cref="ShellFolder" />派生クラスのインスタンスを作成します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />。</param>
        /// <returns>作成した<see cref="ShellFolder" />派生クラスのインスタンス。</returns>
        public abstract ShellFolder CreateShellFolder(ShellItem shellItem);

        /// <summary>
        ///     指定した<see cref="ShellItem" />の性質に最も一致する<see cref="ShellFile" />派生クラスのインスタンスを作成します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />。</param>
        /// <returns>作成した<see cref="ShellFile" />派生クラスのインスタンス。</returns>
        public abstract ShellFile CreateShellFile(ShellItem shellItem);

        /// <summary>
        ///     2つのアイテム種別を示す文字列が、同じかどうかを判定します。
        /// </summary>
        /// <param name="itemType">判定する 1つめのアイテム種別。</param>
        /// <param name="extension">判定する 2つめのアイテム種別。</param>
        /// <returns>
        ///     1つめのアイテム種別と 2つめのアイテム種別が同じ場合は<c>true</c>。
        ///     それ以外の場合は<c>false</c>。
        /// </returns>
        public static bool SameItemType(string itemType, string extension)
        {
            return String.Equals(itemType, extension, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     2つのアイテム種別が同じかどうかを判定します。
        /// </summary>
        /// <param name="shellItem">判定する 1つめのアイテム種別を保持する<see cref="ShellItem" />。</param>
        /// <param name="extension">判定する 2つめのアイテム種別。</param>
        /// <returns>
        ///     <paramref name="shellItem" />のアイテム種別と<paramref name="extension" />が同じ場合は<c>true</c>。
        ///     それ以外の場合は<c>false</c>。
        /// </returns>
        private static bool SameItemType(ShellItem shellItem, string extension)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);

            return SameItemType(shellItem.ItemType, extension);
        }

        /// <summary>
        ///     <see cref="IShellItem" />が標準フォルダーの場合、<see cref="IKnownFolder" />を取得します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />。</param>
        /// <returns>標準フォルダーの場合は<see cref="IKnownFolder" />。それ以外の場合は<c>null</c>。</returns>
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
    }

    [ContractClassFor(typeof(ShellFactory))]
    internal abstract class ShellFactoryContract : ShellFactory
    {
        public override ShellFolder CreateShellFolder(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Requires<ArgumentException>(shellItem.IsFolder);

            throw new NotImplementedException();
        }

        public override ShellFile CreateShellFile(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Requires<ArgumentException>(!shellItem.IsFolder);
            Contract.Ensures(Contract.Result<ShellFile>() != null);

            throw new NotImplementedException();
        }
    }
}