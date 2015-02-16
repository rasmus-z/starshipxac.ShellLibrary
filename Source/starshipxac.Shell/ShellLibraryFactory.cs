using System;
using System.Diagnostics.Contracts;
using System.IO;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Interop.Library;
using starshipxac.Shell.Properties;

namespace starshipxac.Shell
{
    public static class ShellLibraryFactory
    {
        internal static readonly Guid FOLDERID_Libraries = new Guid("1B3EA5DC-B587-4786-B4EF-BD1DC332AEAE");
        internal static readonly string FileExtension = ".library-ms";

        /// <summary>
        /// 新しいライブラリを作成します。
        /// </summary>
        /// <param name="libraryName">作成するライブラリ名称。</param>
        /// <param name="overwrite">既存のライブラリに上書きするかどうかを示す値。</param>
        /// <returns>作成したライブラリ。</returns>
        public static ShellLibrary CreateLibrary(string libraryName, bool overwrite)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));
            Contract.Ensures(Contract.Result<ShellLibrary>() != null);

            var shellLibraryInterface = (IShellLibraryNative)new ShellLibraryCoClass();

            var guid = FOLDERID_Libraries;
            var flags = GetLibrarySaveOptions(overwrite);
            IShellItem2 shellItem2;
            shellLibraryInterface.SaveInKnownFolder(ref guid, libraryName, flags, out shellItem2);

            return new ShellLibrary(new ShellItem(shellItem2), shellLibraryInterface, libraryName);
        }

        /// <summary>
        /// 新しいライブラリを作成します。
        /// </summary>
        /// <param name="libraryName">作成するライブラリ名称。</param>
        /// <param name="sourceKnownFolder">ライブラリの標準フォルダー。</param>
        /// <param name="overwrite">既存のライブラリに上書きするかどうかを示す値。</param>
        /// <returns>作成したライブラリ。</returns>
        public static ShellLibrary Create(string libraryName, ShellKnownFolder sourceKnownFolder, bool overwrite)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));
            Contract.Requires<ArgumentNullException>(sourceKnownFolder != null);
            Contract.Ensures(Contract.Result<ShellLibrary>() != null);

            var shellLibraryInterface = (IShellLibraryNative)new ShellLibraryCoClass();

            var guid = sourceKnownFolder.FolderId;
            var flags = GetLibrarySaveOptions(overwrite);
            IShellItem2 shellItem2;
            shellLibraryInterface.SaveInKnownFolder(ref guid, libraryName, flags, out shellItem2);

            return new ShellLibrary(new ShellItem(shellItem2), shellLibraryInterface, libraryName);
        }

        /// <summary>
        /// 新しいライブラリを作成します。
        /// </summary>
        /// <param name="libraryName">作成するライブラリ名称。</param>
        /// <param name="sourcePath">ライブラリのパス。</param>
        /// <param name="overwrite">既存のライブラリに上書きするかどうかを示す値。</param>
        /// <returns>作成したライブラリ。</returns>
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

            var shellLibraryInterface = (IShellLibraryNative)new ShellLibraryCoClass();

            IShellItem2 shellItem2;
            shellLibraryInterface.Save(shellItemIn, libraryName, flags, out shellItem2);

            return new ShellLibrary(new ShellItem(shellItem2), shellLibraryInterface, libraryName);
        }

        /// <summary>
        /// ライブラリ名称を指定して、
        /// <see cref="ShellLibrary"/>クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="libraryName">ライブラリ名称。</param>
        /// <param name="isReadOnly">ライブラリが読み込み専用かどうかを示す値。</param>
        /// <returns>作成したライブラリ。</returns>
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

            var shellLibraryInterface = (IShellLibraryNative)new ShellLibraryCoClass();

            var flags = isReadOnly ? STGM.STGM_READ : STGM.STGM_READWRITE;
            shellLibraryInterface.LoadLibraryFromItem(shellItem, flags);

            return new ShellLibrary(new ShellItem((IShellItem2)shellItem), shellLibraryInterface, libraryName);
        }

        /// <summary>
        /// 標準フォルダーを指定して、
        /// <see cref="ShellLibrary"/>クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="sourceKnownFolder">標準フォルダー情報。</param>
        /// <param name="isReadOnly">ライブラリが読み込み専用かどうかを示す値。</param>
        /// <returns>作成したライブラリ。</returns>
        public static ShellLibrary Load(ShellKnownFolder sourceKnownFolder, bool isReadOnly = true)
        {
            Contract.Requires<ArgumentNullException>(sourceKnownFolder != null);
            Contract.Ensures(Contract.Result<ShellLibrary>() != null);

            var shellItem = sourceKnownFolder.ShellItem.ShellItemInterface;
            var shellLibraryInterface = (IShellLibraryNative)new ShellLibraryCoClass();
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
        /// ライブラリ名称とフォルダーのパスを指定して、
        /// <see cref="ShellLibrary"/>クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="libraryName">ライブラリ名称。</param>
        /// <param name="sourcePath">フォルダーのパス。</param>
        /// <param name="isReadOnly">ライブラリが読み込み専用かどうかを示す値。</param>
        /// <returns>作成したライブラリ。</returns>
        public static ShellLibrary Load(string libraryName, string sourcePath, bool isReadOnly = true)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(sourcePath));
            Contract.Ensures(Contract.Result<ShellLibrary>() != null);

            var shellItemPath = Path.Combine(sourcePath, libraryName + FileExtension);

            var shellLibraryInterface = (IShellLibraryNative)new ShellLibraryCoClass();

            var item = ShellFactory.FromFilePath(shellItemPath);
            var shellItem = item.ShellItem.ShellItemInterface;
            var flags = isReadOnly ? STGM.STGM_READ : STGM.STGM_READWRITE;
            shellLibraryInterface.LoadLibraryFromItem(shellItem, flags);

            return new ShellLibrary(new ShellItem(shellItem), shellLibraryInterface, libraryName);
        }

        /// <summary>
        /// <see cref="ShellItem"/>を指定して、
        /// <see cref="ShellLibrary"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem"/>。</param>
        /// <param name="isReadOnly">ライブラリが読み込み専用かどうかを示す値。</param>
        /// <returns>作成した<see cref="ShellLibrary"/>。</returns>
        internal static ShellLibrary FromShellItem(ShellItem shellItem, bool isReadOnly)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Ensures(Contract.Result<ShellLibrary>() != null);

            var shellLibraryInterface = (IShellLibraryNative)new ShellLibraryCoClass();
            var flags = isReadOnly ? STGM.STGM_READ : STGM.STGM_READWRITE;
            shellLibraryInterface.LoadLibraryFromItem(shellItem.ShellItemInterface, flags);

            var libraryName = shellItem.GetDisplayName();
            return new ShellLibrary(shellItem, shellLibraryInterface, libraryName);
        }

        /// <summary>
        /// <see cref="LIBRARYSAVEFLAGS"/>を取得します。
        /// </summary>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        private static LIBRARYSAVEFLAGS GetLibrarySaveOptions(bool overwrite)
        {
            return overwrite ? LIBRARYSAVEFLAGS.LSF_OVERRIDEEXISTING : LIBRARYSAVEFLAGS.LSF_FAILIFTHERE;
        }
    }
}