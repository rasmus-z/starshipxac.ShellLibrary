using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Components.Internal;

namespace starshipxac.Shell.Components
{
    /// <summary>
    ///     シェル通知イベントデータを定義します。
    /// </summary>
    public class ShellNotificationEventArgs : EventArgs
    {
        /// <summary>
        ///     <see cref="ShellNotificationEventArgs" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="changeNotify">シェル変更通知情報。</param>
        internal ShellNotificationEventArgs(ShellChangeNotify changeNotify)
        {
            Contract.Requires<ArgumentNullException>(changeNotify != null);

            ChangeType = changeNotify.ChangeType;
            FromSystemInterrupt = changeNotify.FromSystemInterrupt;
        }

        /// <summary>
        ///     <see cref="ShellObject" />で発生したイベント種別を取得します。
        /// </summary>
        public ShellChangeTypes ChangeType { get; }

        /// <summary>
        ///     発生したイベントがシステムイベントかどうかを判定する値を取得します。
        /// </summary>
        public bool FromSystemInterrupt { get; }
    }
}