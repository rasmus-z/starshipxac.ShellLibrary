using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Components.Internal;

namespace starshipxac.Shell.Components
{
    /// <summary>
    /// システムイメージ変更イベントデータを定義します。
    /// </summary>
    public class SystemImageUpdatedEventArgs : ShellNotificationEventArgs
    {
        /// <summary>
        /// <see cref="SystemImageUpdatedEventArgs"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="changeNotify">シェル変更通知情報。</param>
        internal SystemImageUpdatedEventArgs(ShellChangeNotify changeNotify)
            : base(changeNotify)
        {
            Contract.Requires<ArgumentNullException>(changeNotify != null);

            ImageIndex = changeNotify.ImageIndex;
        }

        /// <summary>
        /// 更新されたシステムイメージのインデックスを取得します。
        /// </summary>
        public int ImageIndex { get; private set; }
    }
}