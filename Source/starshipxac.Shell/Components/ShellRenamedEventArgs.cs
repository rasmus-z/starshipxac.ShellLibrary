using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Components.Internal;

namespace starshipxac.Shell.Components
{
    /// <summary>
    /// シェル名前変更イベントデータを定義します。
    /// </summary>
    public class ShellRenamedEventArgs : ShellNotificationEventArgs
    {
        /// <summary>
        /// <see cref="ShellRenamedEventArgs"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="changeNotify"></param>
        internal ShellRenamedEventArgs(ShellChangeNotify changeNotify)
            : base(changeNotify)
        {
            Contract.Requires<ArgumentNullException>(changeNotify != null);
            Contract.Requires<ArgumentException>(changeNotify.ShellObject != null);
            Contract.Requires<ArgumentException>(changeNotify.ShellObject2 != null);

            this.ShellObject = changeNotify.ShellObject;
            this.NewShellObject = changeNotify.ShellObject2;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ShellObject != null);
            Contract.Invariant(this.NewShellObject != null);
        }

        /// <summary>
        /// イベントが発生した<see cref="ShellObject"/>を取得します。
        /// </summary>
        public ShellObject ShellObject { get; private set; }

        /// <summary>
        /// 変更後の<see cref="ShellObject"/>を取得します。
        /// </summary>
        public ShellObject NewShellObject { get; private set; }
    }
}