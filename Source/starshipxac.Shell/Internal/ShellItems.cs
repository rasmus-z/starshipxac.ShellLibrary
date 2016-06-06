using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace starshipxac.Shell.Internal
{
    /// <summary>
    ///     <see cref="ShellObject" />のコレクションに対する列挙子を公開します。
    /// </summary>
    internal class ShellItems : IEnumerable<ShellObject>
    {
        private readonly ShellFolderEnumerator folderEnumerator;

        /// <summary>
        ///     <see cref="ShellFolderEnumerator" />を指定して、
        ///     <see cref="ShellItems" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="folderEnumerator"></param>
        public ShellItems(ShellFolderEnumerator folderEnumerator)
        {
            Contract.Requires<ArgumentNullException>(folderEnumerator != null);

            this.folderEnumerator = folderEnumerator;
        }

        /// <summary>
        ///     親コンテナーを取得します。
        /// </summary>
        public ShellFolder Parent => this.folderEnumerator.Parent;

        public IEnumerator<ShellObject> GetEnumerator()
        {
            return this.folderEnumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}