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
    /// シェル標準フォルダーを定義します。
    /// </summary>
    public class ShellKnownFolder : ShellFolder
    {
        /// <summary>
        /// <see cref="ShellItem"/>、<see cref="IKnownFolder"/>および<see cref="FolderProperties"/>を指定して、
        /// <see cref="ShellKnownFolder"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem"/>。</param>
        /// <param name="knownFolderInterface"><see cref="IKnownFolder"/>。</param>
        /// <exception cref="ArgumentNullException">
        /// <para>
        /// <paramref name="=shellItem"/>が<c>null</c>参照です。
        /// </para>
        /// または
        /// <para>
        /// <paramref name="knownFolderInterface"/>が<c>null</c>参照です。
        /// </para>
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
        /// <see cref="ShellKnownFolder"/>によって使用されているすべてのリソースを解放し、
        /// オプションでマネージリソースも解放します。
        /// </summary>
        /// <param name="disposing">
        /// マネージリソースとアンマネージリソースの両方を解放する場合は<c>true</c>。
        /// アンマネージリソースだけを解放する場合は<c>false</c>。
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
                if (this.KnownFolderInterface != null)
                {
                    Marshal.ReleaseComObject(this.KnownFolderInterface);
                }
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

        internal IKnownFolder KnownFolderInterface { get; private set; }

        internal FolderProperties KnownFolderProperties { get; private set; }

        /// <summary>
        /// 標準フォルダーのファイルシステム上のパスを取得します。
        /// </summary>
        public override string Path
        {
            get
            {
                return this.KnownFolderProperties.Path;
            }
        }

        /// <summary>
        /// ファイルシステム上のパスが存在するかどうかを判定する値を取得します。
        /// </summary>
        public override bool PathExists
        {
            get
            {
                return this.KnownFolderProperties.PathExists;
            }
        }

        /// <summary>
        /// フォルダーカテゴリーを取得します。
        /// </summary>
        public KnownFolderCategories Category
        {
            get
            {
                return this.KnownFolderProperties.Category;
            }
        }

        /// <summary>
        /// 標準名を取得します。
        /// </summary>
        public string CanonicalName
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return this.KnownFolderProperties.CanonicalName;
            }
        }

        /// <summary>
        /// フォルダーの説明を取得します。
        /// </summary>
        public string Description
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return this.KnownFolderProperties.Description;
            }
        }

        /// <summary>
        /// 親フォルダーの<see cref="Guid"/>を取得します。
        /// </summary>
        public Guid ParentId
        {
            get
            {
                return this.KnownFolderProperties.ParentId;
            }
        }

        /// <summary>
        /// 相対パスを取得します。
        /// </summary>
        public string RelativePath
        {
            get
            {
                return this.KnownFolderProperties.RelativePath;
            }
        }

        /// <summary>
        /// ツールチップテキストを取得します。
        /// </summary>
        public string ToolTip
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return this.KnownFolderProperties.ToolTip;
            }
        }

        public StringReference ToolTipReference
        {
            get
            {
                Contract.Ensures(Contract.Result<StringReference>() != null);
                return this.KnownFolderProperties.ToolTipResource;
            }
        }

        /// <summary>
        /// ローカライズされた名前を取得します。
        /// </summary>
        public string LocalizedName
        {
            get
            {
                return this.KnownFolderProperties.LocalizedName;
            }
        }

        /// <summary>
        /// セキュリティ情報を取得します。
        /// </summary>
        public string Security
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return this.KnownFolderProperties.Security;
            }
        }

        /// <summary>
        /// 属性を取得します。
        /// </summary>
        public FileAttributes Attributes
        {
            get
            {
                return this.KnownFolderProperties.FileAttributes;
            }
        }

        /// <summary>
        /// 標準フォルダーの動作を取得します。
        /// </summary>
        public FolderDefinitionFlags FolderDefinitionFlag
        {
            get
            {
                return this.KnownFolderProperties.FolderDefinitionFlags;
            }
        }

        /// <summary>
        /// フォルダー種別IDを取得します。
        /// </summary>
        internal Guid FolderTypeId
        {
            get
            {
                return this.KnownFolderProperties.FolderTypeId;
            }
        }

        /// <summary>
        /// フォルダー種別を取得します。
        /// </summary>
        public string FolderType
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return this.KnownFolderProperties.FolderType;
            }
        }

        /// <summary>
        /// フォルダーIDを取得します。
        /// </summary>
        internal Guid FolderId
        {
            get
            {
                return this.KnownFolderProperties.FolderId;
            }
        }

        /// <summary>
        /// <see cref="RedirectionCapability"/>を取得します。
        /// </summary>
        public RedirectionCapability Redirection
        {
            get
            {
                return this.KnownFolderProperties.Redirection;
            }
        }
    }
}