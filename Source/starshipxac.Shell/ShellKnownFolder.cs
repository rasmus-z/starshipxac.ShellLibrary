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
    ///     シェル標準フォルダーを定義します。
    /// </summary>
    public sealed class ShellKnownFolder : ShellFolder
    {
        /// <summary>
        ///     <see cref="ShellItem" />、<see cref="IKnownFolder" />および<see cref="FolderProperties" />を指定して、
        ///     <see cref="ShellKnownFolder" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />。</param>
        /// <param name="knownFolderInterface"><see cref="IKnownFolder" />。</param>
        /// <exception cref="ArgumentNullException">
        ///     <para>
        ///         <paramref name="=shellItem" />が<c>null</c>参照です。
        ///     </para>
        ///     または
        ///     <para>
        ///         <paramref name="knownFolderInterface" />が<c>null</c>参照です。
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
        ///     <see cref="ShellKnownFolder" />によって使用されているすべてのリソースを解放し、
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
                if (disposing)
                {
                    // マネージリソース解放
                }

                // アンマネージリソース解放
                Marshal.ReleaseComObject(this.KnownFolderInterface);
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.KnownFolderInterface != null);
            Contract.Invariant(this.KnownFolderProperties != null);
        }

        internal IKnownFolder KnownFolderInterface { get; }

        internal FolderProperties KnownFolderProperties { get; }

        /// <summary>
        ///     標準フォルダーのファイルシステム上のパスを取得します。
        /// </summary>
        public override string Path => this.KnownFolderProperties.Path;

        /// <summary>
        ///     ファイルシステム上のパスが存在するかどうかを判定する値を取得します。
        /// </summary>
        public override bool PathExists => this.KnownFolderProperties.PathExists;

        /// <summary>
        ///     フォルダーカテゴリーを取得します。
        /// </summary>
        public KnownFolderCategories Category => this.KnownFolderProperties.Category;

        /// <summary>
        ///     標準名を取得します。
        /// </summary>
        public string CanonicalName => this.KnownFolderProperties.CanonicalName;

        /// <summary>
        ///     フォルダーの説明を取得します。
        /// </summary>
        public string Description => this.KnownFolderProperties.Description;

        /// <summary>
        ///     親フォルダーの<see cref="Guid" />を取得します。
        /// </summary>
        public Guid ParentId => this.KnownFolderProperties.ParentId;

        /// <summary>
        ///     相対パスを取得します。
        /// </summary>
        public string RelativePath => this.KnownFolderProperties.RelativePath;

        /// <summary>
        ///     ツールチップテキストを取得します。
        /// </summary>
        public string ToolTip => this.KnownFolderProperties.ToolTip;

        public StringReference ToolTipReference => this.KnownFolderProperties.ToolTipResource;

        /// <summary>
        ///     ローカライズされた名前を取得します。
        /// </summary>
        public string LocalizedName => this.KnownFolderProperties.LocalizedName;

        /// <summary>
        ///     セキュリティ情報を取得します。
        /// </summary>
        public string Security => this.KnownFolderProperties.Security;

        /// <summary>
        ///     属性を取得します。
        /// </summary>
        public FileAttributes Attributes => this.KnownFolderProperties.FileAttributes;

        /// <summary>
        ///     標準フォルダーの動作を取得します。
        /// </summary>
        public FolderDefinitionFlags FolderDefinitionFlag => this.KnownFolderProperties.FolderDefinitionFlags;

        /// <summary>
        ///     フォルダー種別IDを取得します。
        /// </summary>
        internal Guid FolderTypeId => this.KnownFolderProperties.FolderTypeId;

        /// <summary>
        ///     フォルダー種別を取得します。
        /// </summary>
        public string FolderType => this.KnownFolderProperties.FolderType;

        /// <summary>
        ///     フォルダーIDを取得します。
        /// </summary>
        internal Guid FolderId => this.KnownFolderProperties.FolderId;

        /// <summary>
        ///     <see cref="RedirectionCapability" />を取得します。
        /// </summary>
        public RedirectionCapability Redirection => this.KnownFolderProperties.Redirection;
    }
}