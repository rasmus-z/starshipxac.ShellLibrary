using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    ///     コマンドリンクボタンを定義します。
    /// </summary>
    public class TaskDialogCommandLink : TaskDialogButtonBase
    {
        /// <summary>
        ///     <see cref="TaskDialogCommandLink" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">コントロール名。</param>
        /// <param name="text">コントロールテキスト。</param>
        /// <param name="dialogClosable">ダイアログを閉じることができるかどうかを示す値。</param>
        public TaskDialogCommandLink(string name, string text, bool dialogClosable = false)
            : this(name, text, null, dialogClosable)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        /// <summary>
        ///     <see cref="TaskDialogCommandLink" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">コントロール名。</param>
        /// <param name="text">コントロールテキスト。</param>
        /// <param name="instructionText">説明テキスト。</param>
        /// <param name="dialogClosable">ダイアログを閉じるかどうかを判定する値。</param>
        public TaskDialogCommandLink(string name, string text, string instructionText, bool dialogClosable = false)
            : base(name, text, dialogClosable)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.InstructionText = instructionText ?? String.Empty;
        }

        /// <summary>
        ///     コマンドリンクボタンの説明テキストを取得または設定します。
        /// </summary>
        public string InstructionText { get; set; }

        /// <summary>
        ///     ボタンテキストを取得します。
        /// </summary>
        /// <returns>ボタンテキスト。</returns>
        public override string GetButtonText()
        {
            var result = new StringBuilder();

            if (!String.IsNullOrWhiteSpace(this.Text))
            {
                result.Append(this.Text);
            }
            if (!String.IsNullOrWhiteSpace(this.Text) &&
                !String.IsNullOrWhiteSpace(this.InstructionText))
            {
                result.Append(Environment.NewLine);
            }
            if (!String.IsNullOrWhiteSpace(this.InstructionText))
            {
                result.Append(this.InstructionText);
            }

            return result.ToString();
        }

        /// <summary>
        ///     <see cref="TaskDialogCommandLink" />クラスの文字列表現を取得します。
        /// </summary>
        /// <returns><see cref="TaskDialogCommandLink" />クラスの文字列表現。</returns>
        public override string ToString()
        {
            return $"{this.GetType().Name}{{Id={this.Id}, Name={this.Name}, Text={this.Text}, InstructionText={this.InstructionText}}}";
        }
    }
}