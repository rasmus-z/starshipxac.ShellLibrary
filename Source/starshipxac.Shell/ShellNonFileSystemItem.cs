using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Shell
{
    /// <summary>
    ///     ファイルシステム外シェルアイテムを定義します。
    /// </summary>
    public class ShellNonFileSystemItem : ShellObject
    {
        internal ShellNonFileSystemItem(ShellItem shellItem)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
        }
    }
}