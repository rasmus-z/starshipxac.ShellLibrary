using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;
using starshipxac.Shell.Internal;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    ///     シェルフォルダー情報を定義します。
    /// </summary>
    public class ShellFolder : ShellObject
    {
        private ShellFolder parent;

        /// <summary>
        ///     <see cref="ShellItem" />を指定して、<see cref="ShellFolder" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />。</param>
        /// <remarks>
        ///     <para>
        ///         <see cref="ShellItem.IsFolder" />は、<c>true</c>であるとは限りません。
        ///         一部の<c>KnownFolder</c>は、<see cref="ShellItem.IsFolder" />が<c>false</c>の場合があります。
        ///     </para>
        /// </remarks>
        internal ShellFolder(ShellItem shellItem)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);

            this.ShellFolderInterface = shellItem.GetShellFolder();
        }

        /// <summary>
        ///     <see cref="ShellFolder" />によって使用されているすべてのリソースを解放し、
        ///     オプションでマネージリソースも解放します。
        /// </summary>
        /// <param name="disposing">
        ///     マネージリソースとアンマネージリソースの両方を解放する場合は<c>true</c>。
        ///     アンマネージリソースだけを解放する場合は<c>false</c>。
        /// </param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                // アンマネージリソース解放
                Marshal.ReleaseComObject(this.ShellFolderInterface);
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ShellFolderInterface != null);
        }

        /// <summary>
        ///     <see cref="IShellFolder" />を取得します。
        /// </summary>
        internal IShellFolder ShellFolderInterface { get; }

        /// <summary>
        ///     ファイルシステム上のパスを取得します。
        /// </summary>
        public virtual string Path => this.ParsingName;

        /// <summary>
        ///     ファイルシステム上のパスが存在するかどうかを判定する値を取得します。
        /// </summary>
        public virtual bool PathExists => this.IsFileSystem && Directory.Exists(this.Path);

        /// <summary>
        ///     親フォルダーのインスタンスを取得します。
        /// </summary>
        public ShellFolder Parent
        {
            get
            {
                if (this.parent == null)
                {
                    this.parent = GetFolder();
                }
                return this.parent;
            }
        }

        /// <summary>
        ///     <see cref="ShellFolder" />に存在する<see cref="ShellObject" />のコレクションを取得します。
        /// </summary>
        /// <returns><see cref="ShellObject" />のコレクション。</returns>
        public virtual IEnumerable<ShellObject> EnumerateObjects()
        {
            Contract.Ensures(Contract.Result<IEnumerable<ShellObject>>() != null);
            return new ShellItems(new ShellFolderEnumerator(this));
        }

        /// <summary>
        ///     <see cref="ShellFolder" />に存在する<see cref="ShellFile" />のコレクションを取得します。
        /// </summary>
        /// <returns><see cref="ShellFile" />のコレクション。</returns>
        public virtual IEnumerable<ShellObject> EnumerateFiles()
        {
            Contract.Ensures(Contract.Result<IEnumerable<ShellObject>>() != null);
            return new ShellItems(new ShellFolderEnumerator(this, SHCONTF.SHCONTF_NONFOLDERS));
        }

        /// <summary>
        ///     <see cref="ShellFolder" />に存在する<see cref="ShellFolder" />のコレクションを取得します。
        /// </summary>
        /// <returns><see cref="ShellFolder" />のコレクション。</returns>
        public virtual IEnumerable<ShellFolder> EnumerateFolders()
        {
            Contract.Ensures(Contract.Result<IEnumerable<ShellFolder>>() != null);
            return new ShellFolders(new ShellFolderEnumerator(this, SHCONTF.SHCONTF_FOLDERS));
        }
    }
}