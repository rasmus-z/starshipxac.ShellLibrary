using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace starshipxac.Shell.Internal
{
    /// <summary>
    /// 指定したフォルダー内のフォルダーを反復処理します。
    /// </summary>
    internal class ShellFolders : IEnumerable<ShellFolder>
    {
        private readonly ShellFolderEnumerator enumerator;

        /// <summary>
        /// <see cref="ShellFolders"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="enumerator"></param>
        public ShellFolders(ShellFolderEnumerator enumerator)
        {
            Contract.Requires<ArgumentNullException>(enumerator != null);

            this.enumerator = enumerator;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.enumerator != null);
        }

        /// <summary>
        /// 親コンテナーを取得します。
        /// </summary>
        public ShellFolder Parent
        {
            get
            {
                return this.enumerator.Parent;
            }
        }

        public IEnumerator<ShellFolder> GetEnumerator()
        {
            return new ShellItems(this.enumerator).OfType<ShellFolder>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}