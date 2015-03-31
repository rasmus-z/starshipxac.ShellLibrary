using System;
using System.Diagnostics.Contracts;
using starshipxac.Windows.Properties;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    /// タスクダイアログのプログレスバーを定義します。
    /// </summary>
    public class TaskDialogProgressBar : TaskDialogControl
    {
        private TaskDialogProgressBarState state;
        private int minimum = 0;
        private int maximum = 100;
        private int value;

        /// <summary>
        /// コントロール名を指定して、
        /// <see cref="TaskDialogProgressBar"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">コントロール名。</param>
        public TaskDialogProgressBar(string name)
            : base(name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        /// <summary>
        /// プログレスバーの状態を取得または設定します。
        /// </summary>
        public TaskDialogProgressBarState State
        {
            get
            {
                return this.state;
            }
            set
            {
                //using (BeginChangeProperty())
                //{
                //    this.state = value;
                //}
                this.state = value;
                this.Dialog.SetProgressBarState(this, this.state);
            }
        }

        /// <summary>
        /// プログレスバーの最小値を取得または設定します。
        /// </summary>                
        public int Minimum
        {
            get
            {
                return this.minimum;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(0 <= value,
                    DialogErrorMessages.TaskDialogProgressBarMinValueGreaterThanZero);
                Contract.Requires<ArgumentOutOfRangeException>(value < this.Maximum,
                    DialogErrorMessages.TaskDialogProgressBarMinValueLessThanMax);

                //using (BeginChangeProperty())
                //{
                //    this.minimum = value;
                //}
                this.minimum = value;
                this.Dialog.SetProgressBarRange(this, this.minimum, this.maximum);
            }
        }

        /// <summary>
        /// プログレスバーの最大値を取得または設定します。
        /// </summary>
        public int Maximum
        {
            get
            {
                return this.maximum;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(this.Minimum <= value,
                    DialogErrorMessages.TaskDialogProgressBarMaxValueGreaterThanMin);

                //using (BeginChangeProperty())
                //{
                //    this.maximum = value;
                //}
                this.maximum = value;
                this.Dialog.SetProgressBarRange(this, this.minimum, this.maximum);
            }
        }

        /// <summary>
        /// プログレスバーの現在の値を取得または設定します。
        /// </summary>
        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(this.Minimum <= value && value <= this.Maximum,
                    DialogErrorMessages.TaskDialogProgressBarValueInRange);

                //using (BeginChangeProperty())
                //{
                //    this.value = value;
                //}
                this.value = value;
                this.Dialog.SetProgressBarPosition(this, this.value);
            }
        }

        /// <summary>
        /// プログレスバーの値が最小値と最大値の間にあるかどうかを検証します。
        /// </summary>
        internal bool HasValidValues
        {
            get
            {
                return this.minimum <= this.value && this.value <= this.maximum;
            }
        }

        /// <summary>
        /// プログレスバーの値を最小値に設定します。
        /// </summary>
        protected internal void Reset()
        {
            this.state = TaskDialogProgressBarState.Normal;
            this.value = this.minimum;
        }
    }
}