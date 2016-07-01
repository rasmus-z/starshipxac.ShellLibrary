using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Interop.KnownFolder;
using starshipxac.Shell.Properties;

namespace starshipxac.Shell
{
    /// <summary>
    ///     シェル標準フォルダーファクトリメソッドを定義します。
    /// </summary>
    public static class ShellKnownFolderFactory
    {
        /// <summary>
        ///     標準名称を指定して、<see cref="ShellKnownFolder" />クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="canonicalName">標準フォルダーの標準名称。</param>
        /// <returns>作成した標準フォルダーインターフェイス。</returns>
        /// <exception cref="ArgumentNullException">
        ///     <param name="canonicalName" />
        ///     が<c>null</c>です。
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <para>
        ///         <param name="canonicalName" />
        ///         が空文字列です。
        ///     </para>
        ///     <para>または</para>
        ///     <para>
        ///         <param name="canonicalName" />
        ///         に一致する標準フォルダーが存在しません。
        ///     </para>
        /// </exception>
        public static ShellKnownFolder FromCanonicalName(string canonicalName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(canonicalName));
            Contract.Ensures(Contract.Result<ShellKnownFolder>() != null);

            IKnownFolder knownFolderInterface;
            var knownFolderManager = new KnownFolderManager();
            knownFolderManager.GetFolderByName(canonicalName, out knownFolderInterface);

            var result = CreateKnownFolder(knownFolderInterface);
            if (result == null)
            {
                throw new ArgumentException(ErrorMessages.ShellInvalidCanonicalName, nameof(canonicalName));
            }
            return result;
        }

        /// <summary>
        ///     パスを指定して、<see cref="ShellKnownFolder" />クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="path">パス。</param>
        /// <returns>作成した標準フォルダーインターフェイス。</returns>
        public static ShellKnownFolder FromFolderPath(string path)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(path));

            return FromParsingName(path);
        }

        /// <summary>
        ///     標準フォルダー名称を指定して、<see cref="ShellKnownFolder" />クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="parsingName">標準フォルダー名称またはパス。</param>
        /// <returns>作成した標準フォルダーインターフェイス。</returns>
        /// <exception cref="ArgumentException">指定した標準フォルダー名称またはパスに一致する標準フォルダーは作成できませんでした。</exception>
        public static ShellKnownFolder FromParsingName(string parsingName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(parsingName));

            var pidl = PIDL.Null;
            var pidl2 = PIDL.Null;
            try
            {
                pidl = PIDL.FromParsingName(parsingName);
                if (pidl.IsNull)
                {
                    throw new ArgumentException(ErrorMessages.KnownFolderParsingName, nameof(parsingName));
                }

                var knownFolderInterface = FromPIDL(pidl);
                if (knownFolderInterface != null)
                {
                    var kf = CreateKnownFolder(knownFolderInterface);
                    if (kf == null)
                    {
                        throw new ArgumentException(ErrorMessages.KnownFolderParsingName, nameof(parsingName));
                    }
                    return kf;
                }

                pidl2 = PIDL.FromParsingName(parsingName.PadRight(1, '\0'));
                if (pidl2.IsNull)
                {
                    throw new ArgumentException(ErrorMessages.KnownFolderParsingName, nameof(parsingName));
                }

                var knownFolder = FromPIDL(pidl2);
                if (knownFolder != null)
                {
                    var result = CreateKnownFolder(knownFolder);
                    if (result == null)
                    {
                        throw new ArgumentException(ErrorMessages.KnownFolderParsingName, nameof(parsingName));
                    }
                    return result;
                }

                throw new ArgumentException(ErrorMessages.KnownFolderParsingName, nameof(parsingName));
            }
            finally
            {
                pidl.Free();
                pidl2.Free();
            }
        }

        /// <summary>
        ///     標準フォルダーIDを指定して、<see cref="ShellKnownFolder" />クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="knownFolderId">標準フォルダーの GUID。</param>
        /// <returns>作成した標準フォルダーインターフェイス。</returns>
        /// <exception cref="ArgumentException">指定した GUIDに一致する標準フォルダーは作成できませんでした。</exception>
        public static ShellKnownFolder FromKnownFolderId(Guid knownFolderId)
        {
            IKnownFolder knownFolderNative;
            var knownFolderManager = new KnownFolderManager();
            var hr = knownFolderManager.GetFolder(knownFolderId, out knownFolderNative);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            var result = CreateKnownFolder(knownFolderNative);
            if (result == null)
            {
                throw new ArgumentException(ErrorMessages.KnownFolderInvalidGuid, nameof(knownFolderId));
            }
            return result;
        }

        /// <summary>
        ///     すべての標準フォルダーを取得します。
        /// </summary>
        /// <returns>取得した標準フォルダーのコレクション。</returns>
        public static IReadOnlyList<ShellKnownFolder> GetAllFolders()
        {
            Contract.Ensures(Contract.Result<IReadOnlyList<ShellKnownFolder>>() != null);

            var result = new List<ShellKnownFolder>();

            var folders = IntPtr.Zero;
            try
            {
                // 標準フォルダーIDのコレクションを取得
                uint count;
                var knownFolderManager = new KnownFolderManager();
                knownFolderManager.GetFolderIds(out folders, out count);

                if (count > 0 && folders != IntPtr.Zero)
                {
                    for (var index = 0; index < count; ++index)
                    {
                        var current = new IntPtr(folders.ToInt64() + (Marshal.SizeOf(typeof(Guid))*index));

                        var knownFolderId = (Guid)Marshal.PtrToStructure(current, typeof(Guid));
                        var knownFolder = FromKnownFolderIdInternal(knownFolderId);
                        if (knownFolder != null)
                        {
                            result.Add(knownFolder);
                        }
                    }
                }
            }
            finally
            {
                if (folders != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(folders);
                }
            }

            return result;
        }

        #region Internal Methods

        /// <summary>
        ///     PIDLを指定して、<see cref="IKnownFolder" />を作成します。
        /// </summary>
        /// <param name="pidl">PIDL。</param>
        /// <returns>作成した標準フォルダーインターフェイス。</returns>
        internal static IKnownFolder FromPIDL(PIDL pidl)
        {
            IKnownFolder knownFolder;
            IKnownFolderManager knownFolderManager = new KnownFolderManager();
            var hr = knownFolderManager.FindFolderFromIDList(pidl, out knownFolder);
            if (HRESULT.Failed(hr))
            {
                return null;
            }
            return knownFolder;
        }

        /// <summary>
        ///     標準フォルダーIDを指定して、標準フォルダーを作成します。
        /// </summary>
        /// <param name="knownFolderId">標準フォルダーの GUID。</param>
        /// <returns>作成した標準フォルダーインターフェイス。</returns>
        internal static ShellKnownFolder FromKnownFolderIdInternal(Guid knownFolderId)
        {
            IKnownFolder knownFolderNative;
            IKnownFolderManager knownFolderManager = new KnownFolderManager();
            var hr = knownFolderManager.GetFolder(knownFolderId, out knownFolderNative);
            if (HRESULT.Failed(hr))
            {
                return null;
            }
            return CreateKnownFolder(knownFolderNative);
        }

        /// <summary>
        ///     <see cref="IKnownFolder" />を指定して、標準フォルダーを作成します。
        /// </summary>
        /// <param name="knownFolderInterface"><see cref="IKnownFolder" />。</param>
        /// <returns>作成した標準フォルダーインターフェイス。</returns>
        private static ShellKnownFolder CreateKnownFolder(IKnownFolder knownFolderInterface)
        {
            Contract.Requires<ArgumentNullException>(knownFolderInterface != null);

            // IKnownFolderから IShellItem2を取得する。
            IShellItem2 shellItemNative;
            var guid = new Guid(ShellIID.IShellItem2);
            var hr = knownFolderInterface.GetShellItem(0, ref guid, out shellItemNative);
            if (HRESULT.Failed(hr))
            {
                return null;
            }

            var shellItem = new ShellItem(shellItemNative);
            return new ShellKnownFolder(shellItem, knownFolderInterface);
        }

        #endregion
    }
}