using System;
using System.Diagnostics.Contracts;
using System.Threading;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    ///     タスクダイアログボタン基底クラスを定義します。
    /// </summary>
    public abstract class TaskDialogButtonBase : TaskDialogControl
    {
        private bool enabled = true;
        private bool useElevationIcon;

        /// <summary>
        ///     <see cref="TaskDialogButtonBase" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">ボタン名。</param>
        /// <param name="text">ボタンテキスト。</param>
        /// <param name="dialogClosable">ダイアログを閉じるかどうかを示す値。</param>
        protected TaskDialogButtonBase(string name, string text, bool dialogClosable = false)
            : base(name)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(name));

            this.Text = text ?? String.Empty;
            this.DialogClosable = dialogClosable;
        }

        /// <summary>
        ///     ボタンテキストを取得または設定します。
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        ///     ボタンが有効かどうかを判定する値を取得または設定します。
        /// </summary>
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
                this.Dialog.SetButtonEnabled(this, this.enabled);
            }
        }

        /// <summary>
        ///     デフォルトボタンかどうかを判定する値を取得または設定します。
        /// </summary>
        public bool Default { get; set; }

        /// <summary>
        ///     このボタンをクリックした場合に、ダイアログを閉じるかどうかを判定する値を取得または設定します。
        /// </summary>
        public bool DialogClosable { get; private set; }

        /// <summary>
        ///     管理者昇格アイコンを使用するかどうかを判定する値を取得または設定します。
        /// </summary>
        public bool UseElevationIcon
        {
            get
            {
                return this.useElevationIcon;
            }
            set
            {
                this.useElevationIcon = value;
                this.Dialog.SetButtonElevationRequiredState(this, this.useElevationIcon);
            }
        }

        #region Click Event

        /// <summary>
        ///     ボタンをクリックすると発生します。
        /// </summary>
        public event EventHandler Click;

        protected virtual void OnClick(EventArgs args)
        {
            var handler = Interlocked.CompareExchange(ref this.Click, null, null);
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        internal void RaiseClickEvent()
        {
            if (!this.enabled)
            {
                return;
            }

            OnClick(EventArgs.Empty);
        }

        /// <summary>
        ///     ボタンのテキストを取得します。
        /// </summary>
        /// <returns>ボタンのテキスト。</returns>
        public abstract string GetButtonText();
    }
}