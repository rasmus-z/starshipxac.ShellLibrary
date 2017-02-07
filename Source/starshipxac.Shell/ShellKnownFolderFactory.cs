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
        ///     Create a new instance of the <see cref="ShellKnownFolder" /> class
        ///     to the specified canonical name.
        /// </summary>
        /// <param name="canonicalName">Canonical name of known folder.</param>
        /// <returns><see cref="ShellKnownFolder" />.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="canonicalName" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <para>
        ///         <paramref name="canonicalName" /> is empty string.
        ///     </para>
        ///     or
        ///     <para>
        ///         There is no standard folder matching <paramref name="canonicalName" />.
        ///     </para>
        /// </exception>
        public static ShellKnownFolder FromCanonicalName(string canonicalName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(canonicalName));

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
        ///     Create a new instance of the <see cref="ShellKnownFolder" /> class
        ///     to the specified path.
        /// </summary>
        /// <param name="path">Path.</param>
        /// <returns><see cref="ShellKnownFolder" />.</returns>
        public static ShellKnownFolder FromFolderPath(string path)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(path));

            return FromParsingName(path);
        }

        /// <summary>
        ///     Create a new instance of the <see cref="ShellKnownFolder" /> class
        ///     to the specified parsing name.
        /// </summary>
        /// <param name="parsingName">Parsing name or path.</param>
        /// <returns><see cref="ShellKnownFolder" />.</returns>
        /// <exception cref="ArgumentException">
        ///     A known folder matching the specified standard folder name or path could not be
        ///     created.
        /// </exception>
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
        ///     Create a new instance of the <see cref="ShellKnownFolder" /> class
        ///     to the specified known folder ID.
        /// </summary>
        /// <param name="knownFolderId">Known folder GUID.</param>
        /// <returns>
        ///     <see cref="ShellKnownFolder" />
        /// </returns>
        /// <exception cref="ArgumentException">A known folder matching the specified GUID could not be created.</exception>
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
        ///     Get all the standard folders.
        /// </summary>
        /// <returns>Known folder collection.</returns>
        public static IReadOnlyList<ShellKnownFolder> GetAllFolders()
        {
            var result = new List<ShellKnownFolder>();

            var folders = IntPtr.Zero;
            try
            {
                uint count;
                var knownFolderManager = new KnownFolderManager();
                knownFolderManager.GetFolderIds(out folders, out count);

                if (count > 0 && folders != IntPtr.Zero)
                {
                    for (var index = 0; index < count; ++index)
                    {
                        var current = new IntPtr(folders.ToInt64() + (Marshal.SizeOf(typeof(Guid)) * index));

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
        ///     Create a new instance of the <see cref="IKnownFolder" /> instance
        ///     to the specified PIDL.
        /// </summary>
        /// <param name="pidl">PIDL.</param>
        /// <returns><see cref="IKnownFolder" />.</returns>
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
        ///     Create a new instance of the <see cref="ShellKnownFolder" /> class
        ///     to the specified known folder GUID.
        /// </summary>
        /// <param name="knownFolderId">Known folder GUID.</param>
        /// <returns><see cref="ShellKnownFolder" />.</returns>
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
        ///     Create a new instance of the <see cref="ShellKnownFolder" /> class
        ///     to the specified <see cref="IKnownFolder" />.
        /// </summary>
        /// <param name="knownFolderInterface"><see cref="IKnownFolder" />.</param>
        /// <returns><see cref="ShellKnownFolder" />.</returns>
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