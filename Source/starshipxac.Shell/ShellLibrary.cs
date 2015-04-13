using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Interop.Library;
using starshipxac.Shell.Properties;
using starshipxac.Shell.Resources;

namespace starshipxac.Shell
{
    /// <summary>
    /// シェルライブラリフォルダーを定義します。
    /// </summary>
    public sealed class ShellLibrary : ShellFolder
    {
        private readonly string displayName;

        /// <summary>
        /// <see cref="ShellItem"/>およびライブラリインターフェイスを指定して、
        /// <see cref="ShellLibrary"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem"/>。</param>
        /// <param name="shellLibraryInterface">ライブラリインターフェイス。</param>
        /// <param name="libraryName">ライブラリ名</param>
        internal ShellLibrary(ShellItem shellItem, IShellLibraryNative shellLibraryInterface, string libraryName)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Requires<ArgumentNullException>(shellLibraryInterface != null);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));

            this.ShellLibraryInterface = shellLibraryInterface;
            this.displayName = libraryName;
        }

        [ContractInvariantMethod]
        private void ObjectInvaliant()
        {
            Contract.Invariant(this.ShellLibraryInterface != null);
            Contract.Invariant(this.displayName != null);
        }

        internal IShellLibraryNative ShellLibraryInterface { get; set; }

        public override string Path
        {
            get
            {
                return String.Empty;
            }
        }

        public override bool PathExists
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// ライブラリ表示名を取得します。
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return this.displayName;
            }
        }

        /// <summary>
        /// アイコンリソース情報を取得または設定します。
        /// </summary>
        public IconReference IconReference
        {
            get
            {
                Contract.Ensures(Contract.Result<IconReference>() != null);

                string iconRef;
                this.ShellLibraryInterface.GetIcon(out iconRef);
                return new IconReference(iconRef);
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);

                this.ShellLibraryInterface.SetIcon(value.ReferencePath);
                this.ShellLibraryInterface.Commit();
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// ライブラリフォルダー種別IDを取得または設定します。
        /// </summary>
        public Guid LibraryType
        {
            get
            {
                Guid folderTypeGuid;
                this.ShellLibraryInterface.GetFolderType(out folderTypeGuid);
                return folderTypeGuid;
            }
            set
            {
                var guid = value;
                this.ShellLibraryInterface.SetFolderType(ref guid);
                this.ShellLibraryInterface.Commit();
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 規定の保存フォルダーを取得または設定します。
        /// </summary>
        public ShellFolder DefaultSaveFolder
        {
            get
            {
                Contract.Ensures(Contract.Result<ShellFolder>() != null);

                var guid = new Guid(ShellIID.IShellItem);
                IShellItem saveFolderItem;
                this.ShellLibraryInterface.GetDefaultSaveFolder(
                    DEFAULTSAVEFOLDERTYPE.DSFT_DETECT,
                    ref guid,
                    out saveFolderItem);
                return ShellFactory.FromShellItem(new ShellItem((IShellItem2)saveFolderItem)) as ShellFolder;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);

                if (!value.PathExists)
                {
                    throw new DirectoryNotFoundException(ErrorMessages.ShellLibraryDefaultSaveFolderNotFound);
                }

                this.ShellLibraryInterface.SetDefaultSaveFolder(
                    DEFAULTSAVEFOLDERTYPE.DSFT_DETECT,
                    value.ShellItem.ShellItemInterface);
                this.ShellLibraryInterface.Commit();
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 操作パネルにピン留めするかどうかを示す値を取得または設定します。
        /// </summary>
        public bool IsPinnedToNavigationPane
        {
            get
            {
                var flags = LIBRARYOPTIONFLAGS.LOF_PINNEDTONAVPANE;
                this.ShellLibraryInterface.GetOptions(out flags);

                return (flags & LIBRARYOPTIONFLAGS.LOF_PINNEDTONAVPANE) == LIBRARYOPTIONFLAGS.LOF_PINNEDTONAVPANE;
            }
            set
            {
                var flags = LIBRARYOPTIONFLAGS.LOF_DEFAULT;
                if (value)
                {
                    flags |= LIBRARYOPTIONFLAGS.LOF_PINNEDTONAVPANE;
                }
                else
                {
                    flags &= ~LIBRARYOPTIONFLAGS.LOF_PINNEDTONAVPANE;
                }

                this.ShellLibraryInterface.SetOptions(LIBRARYOPTIONFLAGS.LOF_PINNEDTONAVPANE, flags);
                this.ShellLibraryInterface.Commit();
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// ライブラリが読み込み専用かどうかを示す値を取得します。
        /// </summary>
        internal bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Close()
        {
            this.Dispose();
        }

        public override IEnumerable<ShellFolder> EnumerateFolders()
        {
            Contract.Ensures(Contract.Result<IEnumerable<ShellFolder>>() != null);

            var result = new List<ShellFolder>();
            IShellItemArray itemArray;

            var shellItemArrayGuid = new Guid(ShellIID.IShellItemArray);
            var hr = this.ShellLibraryInterface.GetFolders(LIBRARYFOLDERFILTER.LFF_ALLITEMS,
                ref shellItemArrayGuid, out itemArray);
            if (HRESULT.Failed(hr))
            {
                return result;
            }

            uint count;
            itemArray.GetCount(out count);

            for (uint index = 0; index < count; ++index)
            {
                IShellItem shellItem;
                itemArray.GetItemAt(index, out shellItem);
                result.Add(new ShellFolder(new ShellItem((IShellItem2)shellItem)));
            }

            Marshal.ReleaseComObject(itemArray);

            return result;
        }
    }
}