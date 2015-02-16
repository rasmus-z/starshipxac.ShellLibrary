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
    /// <see cref="ShellObject"/>を作成するメソッドを定義します。
    /// </summary>
    public class ShellFactory
    {
        private static readonly object syncObj = new object();
        private static ShellFactory instance;

        /// <summary>
        /// <c>Shell</c>ファクトリークラスのインスタンスを取得または設定します。
        /// </summary>
        public static ShellFactory Instance
        {
            get
            {
                Contract.Ensures(Contract.Result<ShellFactory>() != null);
                if (instance == null)
                {
                    lock(syncObj)
                    {
                        if (instance == null)
                        {
                            instance = new ShellFactory();
                        }
                    }
                }
                return instance;
            }
            set
            {
                Contract.Ensures(instance != null);
                lock(syncObj)
                {
                    if (value == null)
                    {
                        instance = new ShellFactory();
                    }
                    else
                    {
                        instance = value;
                    }
                }
            }
        }

        /// <summary>
        /// 解析名(<c>ParsingName</c>)を指定して、<see cref="ShellObject"/>クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="parsingName"><c>ParsingName</c>。</param>
        /// <returns>作成した<see cref="ShellObject"/>。</returns>
        public static ShellObject FromParsingName(string parsingName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(parsingName));
            Contract.Ensures(Contract.Result<ShellObject>() != null);

            return Instance.Create(ShellItem.FromParsingName(parsingName));
        }

        /// <summary>
        /// ファイルのパスを指定して、<see cref="ShellFile"/>クラスの新しいインスタンスを作成します。
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
        /// フォルダーのパスを指定して、<see cref="ShellFolder"/>クラスの新しいインスタンスを作成します。
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
                throw new DirectoryNotFoundException(
                    String.Format(CultureInfo.InvariantCulture, ErrorMessages.FilePathNotExist, path));
            }

            return new ShellFolder(ShellItem.FromParsingName(absPath));
        }

        /// <summary>
        /// 指定した<see cref="ShellItem"/>から、<see cref="ShellObject"/>派生クラスのインスタンスを作成します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem"/>。</param>
        /// <returns>作成した<see cref="ShellObject"/>。</returns>
        public virtual ShellObject Create(ShellItem shellItem)
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
                return CreateFile(shellItem);
            }

            if (shellItem.IsStream)
            {
                return CreateStreamFile(shellItem);
            }

            // ファイルシステム外のアイテム
            return new ShellNonFileSystemItem(shellItem);
        }

        /// <summary>
        /// 指定した<c>ParsingName</c>から、<see cref="ShellFolder"/>を作成します。
        /// </summary>
        /// <param name="parsingName"><c>ParsingName</c>。</param>
        /// <returns>
        /// 作成した<see cref="ShellFolder"/>。
        /// <param name="parsingName"/>がフォルダではない場合は<c>null</c>。
        /// </returns>
        public ShellFolder CreateFolder(string parsingName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(parsingName));

            var shellItem = ShellItem.FromParsingName(parsingName);
            if (!shellItem.IsFolder)
            {
                return null;
            }

            return Instance.CreateFolder(shellItem);
        }

        /// <summary>
        /// 指定した<see cref="ShellItem"/>から、<see cref="ShellFolder"/>を作成します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem"/>。</param>
        /// <returns>作成した<see cref="ShellFolder"/>。</returns>
        public virtual ShellFolder CreateFolder(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Requires<ArgumentException>(shellItem.IsFolder);
            Contract.Ensures(Contract.Result<ShellFolder>() != null);

            var shellLibrary = ShellLibraryFactory.FromShellItem(shellItem, true);
            if (SameItemType(shellItem.ItemType, ShellLibraryFactory.FileExtension) && (shellLibrary != null))
            {
                // ライブラリー
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
                return new ShellKnownFolder(shellItem, knownFolderNative);
            }

            // フォルダー
            return new ShellFolder(shellItem);
        }

        /// <summary>
        /// 指定した<see cref="ShellItem"/>から、<see cref="ShellFile"/>を作成します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem"/>。</param>
        /// <returns>作成した<see cref="ShellFile"/>。</returns>
        public virtual ShellFile CreateFile(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Requires<ArgumentException>(shellItem.IsFileSystem);
            Contract.Ensures(Contract.Result<ShellFile>() != null);

            return new ShellFile(shellItem);
        }

        public virtual ShellFile CreateStreamFile(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Requires<ArgumentException>(shellItem.IsStream);
            Contract.Ensures(Contract.Result<ShellFile>() != null);

            return new ShellFile(shellItem);
        }

        /// <summary>
        /// 指定した<see cref="IShellItem"/>から、<see cref="ShellObject"/>を作成します。
        /// </summary>
        /// <param name="shellItem"><see cref="IShellItem"/>。</param>
        /// <returns>作成した<see cref="ShellObject"/>。</returns>
        internal static ShellObject Create(IShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Ensures(Contract.Result<ShellObject>() != null);

            return Instance.Create(new ShellItem((IShellItem2)shellItem));
        }

        /// <summary>
        /// 指定した<see cref="ShellItem"/>から、<see cref="ShellFolder"/>を作成します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem"/>。</param>
        /// <returns>作成した<see cref="ShellFolder"/>。</returns>
        internal static ShellFolder CreateFolder(IShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Ensures(Contract.Result<ShellFolder>() != null);

            return Instance.CreateFolder(new ShellItem((IShellItem2)shellItem));
        }

        /// <summary>
        /// <see cref="PIDL"/>から、<see cref="ShellObject"/>を作成します。
        /// </summary>
        /// <param name="pidl"><see cref="PIDL"/>。</param>
        /// <returns>作成した<see cref="ShellObject"/>。</returns>
        internal static ShellObject Create(PIDL pidl)
        {
            Contract.Requires<ArgumentException>(!pidl.IsNull);
            return Instance.Create(ShellItem.FromPIDL(pidl));
        }

        /// <summary>
        /// 2つのアイテム種別が同じかどうかを判定します。
        /// </summary>
        /// <param name="shellItem">判定する 1つめのアイテム種別を保持する<see cref="ShellItem"/>。</param>
        /// <param name="extension">判定する 2つめのアイテム種別。</param>
        /// <returns>
        /// <paramref name="shellItem"/>のアイテム種別と<paramref name="extension"/>が同じ場合は<c>true</c>。
        /// それ以外の場合は<c>false</c>。
        /// </returns>
        private static bool SameItemType(ShellItem shellItem, string extension)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);

            return SameItemType(shellItem.ItemType, extension);
        }

        /// <summary>
        /// 2つのアイテム種別が同じかどうかを判定します。
        /// </summary>
        /// <param name="itemType">判定する 1つめのアイテム種別。</param>
        /// <param name="extension">判定する 2つめのアイテム種別。</param>
        /// <returns>
        /// 1つめのアイテム種別と 2つめのアイテム種別が同じ場合は<c>true</c>。
        /// それ以外の場合は<c>false</c>。
        /// </returns>
        public static bool SameItemType(string itemType, string extension)
        {
            return String.Equals(itemType, extension, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// <see cref="IShellItem2"/>が標準フォルダーかどうかを判定します。
        /// </summary>
        /// <param name="shellItem">判定する<see cref="ShellItem"/>。</param>
        /// <returns>標準フォルダーの場合は<c>true</c>を返します。それ以外の場合は<c>false</c>を返します。</returns>
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
}