using System;
using System.Diagnostics.Contracts;
using System.Threading;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    /// タスクダイアログボタン基底クラスを定義します。
    /// </summary>
    public abstract class TaskDialogButtonBase : TaskDialogControl
    {
        private bool enabled = true;
        private string text = String.Empty;
        private bool defaultControl;
        private bool useElevationIcon;

        /// <summary>
        /// <see cref="TaskDialogButtonBase"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="id">標準ボタンID。</param>
        /// <param name="name"></param>
        protected TaskDialogButtonBase(TaskDialogCommonButtonId id, string name)
            : base((int)id, name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.DialogClosable = true;
        }

        /// <summary>
        /// <see cref="TaskDialogButtonBase"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="id">ボタンID。</param>
        /// <param name="name">ボタン名。</param>
        /// <param name="dialogClosable">ダイアログを閉じるかどうかを示す値。</param>
        protected TaskDialogButtonBase(int id, string name, bool dialogClosable = false)
            : base(id, name)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.DialogClosable = dialogClosable;
        }

        /// <summary>
        /// <see cref="TaskDialogButtonBase"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="id">標準ボタンID。</param>
        /// <param name="name">ボタン名。</param>
        /// <param name="text">ボタンテキスト。</param>
        protected TaskDialogButtonBase(TaskDialogCommonButtonId id, string name, string text)
            : base((int)id, name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.Text = text;
            this.DialogClosable = true;
        }

        /// <summary>
        /// <see cref="TaskDialogButtonBase"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="id">ボタンID。</param>
        /// <param name="name">ボタン名。</param>
        /// <param name="text">ボタンテキスト。</param>
        /// <param name="dialogClosable">ダイアログを閉じるかどうかを示す値。</param>
        protected TaskDialogButtonBase(int id, string name, string text, bool dialogClosable = false)
            : base(id, name)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(name));

            this.text = text;
            this.DialogClosable = dialogClosable;
        }

        /// <summary>
        /// ボタンテキストを取得または設定します。
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                //using (BeginChangeProperty())
                //{
                //    this.text = value;
                //}
                this.text = value;
            }
        }

        /// <summary>
        /// ボタンが有効かどうかを判定する値を取得または設定します。
        /// </summary>
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                //using (BeginChangeProperty())
                //{
                //    this.enabled = value;
                //}
                this.enabled = value;
                this.Dialog.SetButtonEnabled(this, this.enabled);
            }
        }

        /// <summary>
        /// デフォルトボタンかどうかを判定する値を取得または設定します。
        /// </summary>
        public bool Default
        {
            get
            {
                return this.defaultControl;
            }
            set
            {
                //using (BeginChangeProperty())
                //{
                //    this.defaultControl = value;
                //}
                this.defaultControl = value;
            }
        }

        /// <summary>
        /// このボタンをクリックした場合に、ダイアログを閉じるかどうかを判定する値を取得または設定します。
        /// </summary>
        public bool DialogClosable { get; set; }

        /// <summary>
        /// 管理者昇格アイコンを使用するかどうかを判定する値を取得または設定します。
        /// </summary>
        public bool UseElevationIcon
        {
            get
            {
                return this.useElevationIcon;
            }
            set
            {
                //using (BeginChangeProperty("ShowElevationIcon"))
                //{
                //    this.useElevationIcon = value;
                //}
                this.useElevationIcon = value;
                this.Dialog.SetButtonElevationRequiredState(this, this.useElevationIcon);
            }
        }

        #region Click Event

        /// <summary>
        /// ボタンをクリックすると発生します。
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
        /// ボタンのテキストを取得します。
        /// </summary>
        /// <returns>ボタンのテキスト。</returns>
        public abstract string GetButtonText();
    }
}