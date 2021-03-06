﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Internal
{
    /// <summary>
    ///     フォルダー内のアイテムを反復処理します。
    /// </summary>
    internal class ShellFolderEnumerator : IEnumerator<ShellObject>
    {
        private IEnumIDList enumIdList;

        /// <summary>
        ///     <see cref="ShellFolderEnumerator" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="parentFolder">親フォルダー。</param>
        /// <param name="options">フォルダー反復処理オプション。 </param>
        /// <remarks>
        ///     <para>
        ///         <see cref="System.IO.FileNotFoundException" />が発生する場合は、
        ///         プロジェクトのプロパティ -> ビルドの「32ビットを優先」のチェックを外す。
        ///     </para>
        /// </remarks>
        internal ShellFolderEnumerator(ShellFolder parentFolder, SHCONTF options)
        {
            Contract.Requires<ArgumentNullException>(parentFolder != null);

            this.Parent = parentFolder;

            this.enumIdList = this.Parent.ShellFolderItem.EnumObjects(options);
        }

        /// <summary>
        ///     <see cref="ShellFolderEnumerator" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="parentFolder">親フォルダー。</param>
        internal ShellFolderEnumerator(ShellFolder parentFolder)
            : this(parentFolder, SHCONTF.SHCONTF_FOLDERS | SHCONTF.SHCONTF_NONFOLDERS)
        {
            Contract.Requires<ArgumentNullException>(parentFolder != null);
        }

        public void Dispose()
        {
            if (this.enumIdList != null)
            {
                Marshal.ReleaseComObject(this.enumIdList);
                this.enumIdList = null;
            }
        }

        /// <summary>
        ///     親フォルダーを取得します。
        /// </summary>
        public ShellFolder Parent { get; }

        public ShellObject Current { get; private set; }

        object IEnumerator.Current => this.Current;

        public bool MoveNext()
        {
            if (this.enumIdList == null)
            {
                return false;
            }

            const uint itemsRequested = 1;
            IntPtr item;
            uint numItemsReturned;
            var hr = this.enumIdList.Next(itemsRequested, out item, out numItemsReturned);
            if (HRESULT.Failed(hr) || numItemsReturned < itemsRequested)
            {
                return false;
            }

            this.Current = ShellFactory.FromShellItem(ShellItem.FromIdList(item, this.Parent.ShellFolderItem));
            return true;
        }

        public void Reset()
        {
            this.enumIdList?.Reset();
        }
    }
}