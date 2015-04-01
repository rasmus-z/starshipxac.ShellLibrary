using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    /// コマンドリンクボタンを定義します。
    /// </summary>
    public class TaskDialogCommandLink : TaskDialogButtonBase
    {
        /// <summary>
        /// <see cref="TaskDialogCommandLink"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="id">標準ボタンID。</param>
        /// <param name="name">コントロール名。</param>
        /// <param name="text">コントロールテキスト。</param>
        public TaskDialogCommandLink(TaskDialogCommonButtonId id, string name, string text)
            : base((int)id, name, text)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        /// <summary>
        /// <see cref="TaskDialogCommandLink"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="id">コントロールID。</param>
        /// <param name="name">コントロール名。</param>
        /// <param name="text">コントロールテキスト。</param>
        /// <param name="dialogClosable">ダイアログを閉じることができるかどうかを示す値。</param>
        public TaskDialogCommandLink(int id, string name, string text, bool dialogClosable = false)
            : base(id, name, text, dialogClosable)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        /// <summary>
        /// <see cref="TaskDialogCommandLink"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="id">標準ボタンID。</param>
        /// <param name="name">コントロール名。</param>
        /// <param name="text">コントロールテキスト。</param>
        /// <param name="instruction">説明テキスト。</param>
        public TaskDialogCommandLink(TaskDialogCommonButtonId id, string name, string text, string instruction)
            : base((int)id, name, text)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.Instruction = instruction;
        }

        /// <summary>
        /// <see cref="TaskDialogCommandLink"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="id">コントロールID。</param>
        /// <param name="name">コントロール名。</param>
        /// <param name="text">コントロールテキスト。</param>
        /// <param name="instruction">説明テキスト。</param>
        /// <param name="dialogClosable">ダイアログを閉じるかどうかを判定する値。</param>
        public TaskDialogCommandLink(int id, string name, string text, string instruction, bool dialogClosable = false)
            : base(id, name, text, dialogClosable)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.Instruction = instruction;
        }

        /// <summary>
        /// コマンドリンクボタンの説明テキストを取得または設定します。
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// ボタンテキストを取得します。
        /// </summary>
        /// <returns>ボタンテキスト。</returns>
        public override string GetButtonText()
        {
            return String.Format("{0}{1}{2}",
                this.Text ?? String.Empty,
                (!String.IsNullOrEmpty(this.Text) && !String.IsNullOrEmpty(this.Instruction)) ? Environment.NewLine : String.Empty,
                this.Instruction ?? String.Empty);
        }

        /// <summary>
        /// <see cref="TaskDialogCommandLink"/>クラスの文字列表現を取得します。
        /// </summary>
        /// <returns><see cref="TaskDialogCommandLink"/>クラスの文字列表現。</returns>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("TaskDialogCommandLink[");
            result.AppendFormat("Id={0}", this.Id);
            result.AppendFormat(", Name={0}", this.Name);
            result.AppendFormat(", Text={0}", this.Text);
            result.AppendFormat(", Instruction={0}", this.Instruction);
            result.Append("]");
            return result.ToString();
        }
    }
}