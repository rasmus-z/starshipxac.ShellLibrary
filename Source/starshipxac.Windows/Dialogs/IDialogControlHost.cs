using System;
using System.Diagnostics.Contracts;
using starshipxac.Windows.Dialogs.Controls;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// ダイアログコントロールホストのインターフェイスを定義します。
    /// </summary>
    [ContractClass(typeof(DialogControlHostContract))]
    public interface IDialogControlHost
    {
        /// <summary>
        /// コレクションの変更が許可されているかどうかを判定します。
        /// </summary>
        /// <returns>コレクションの変更が許可されている場合は<c>true</c>。</returns>
        bool IsCollectionChangeAllowed();

        /// <summary>
        /// コレクションの変更を適用します。
        /// </summary>
        void ApplyCollectionChanged();

        /// <summary>
        /// プロパティ値の変更が許可されているかどうかを判定します。
        /// </summary>
        /// <param name="propertyName">判定するプロパティ名。</param>
        /// <param name="control">判定するコントロール。</param>
        /// <returns>プロパティ値の変更が許可されている場合は<c>true</c>。</returns>
        bool IsControlPropertyChangeAllowed(string propertyName, DialogControl control);

        /// <summary>
        /// プロパティ値の変更を適用します。
        /// </summary>
        /// <param name="propertyName">プロパティ名。</param>
        /// <param name="control">コントロール。</param>
        void ApplyControlPropertyChange(string propertyName, DialogControl control);
    }

    [ContractClassFor(typeof(IDialogControlHost))]
    public abstract class DialogControlHostContract : IDialogControlHost
    {
        public abstract bool IsCollectionChangeAllowed();

        public abstract void ApplyCollectionChanged();

        public abstract bool IsControlPropertyChangeAllowed(string propertyName, DialogControl control);

        public void ApplyControlPropertyChange(string propertyName, DialogControl control)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(propertyName));
            Contract.Requires<ArgumentNullException>(control != null);
        }
    }
}