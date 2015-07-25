using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Components.Internal;

namespace starshipxac.Shell.Components
{
    /// <summary>
    /// シェル変更イベントデータを定義します。
    /// </summary>
    public class ShellChangedEventArgs : ShellNotificationEventArgs
    {
        /// <summary>
        /// <see cref="ShellChangedEventArgs"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="changeNotify">シェル変更通知情報。</param>
        internal ShellChangedEventArgs(ShellChangeNotify changeNotify)
            : base(changeNotify)
        {
            Contract.Requires<ArgumentNullException>(changeNotify != null);
            Contract.Requires<ArgumentException>(changeNotify.ShellObject != null);

            this.ShellObject = changeNotify.ShellObject;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ShellObject != null);
        }

        /// <summary>
        /// イベントが発生した<see cref="ShellObject"/>を取得します。
        /// </summary>
        public ShellObject ShellObject { get; }
    }
}