using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    /// �R�}���h�����N�{�^�����`���܂��B
    /// </summary>
    public class TaskDialogCommandLink : TaskDialogButtonBase
    {
        /// <summary>
        /// <see cref="TaskDialogCommandLink"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="id">�W���{�^��ID�B</param>
        /// <param name="name">�R���g���[�����B</param>
        /// <param name="text">�R���g���[���e�L�X�g�B</param>
        public TaskDialogCommandLink(TaskDialogCommonButtonId id, string name, string text)
            : base((int)id, name, text)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        /// <summary>
        /// <see cref="TaskDialogCommandLink"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="id">�R���g���[��ID�B</param>
        /// <param name="name">�R���g���[�����B</param>
        /// <param name="text">�R���g���[���e�L�X�g�B</param>
        /// <param name="dialogClosable">�_�C�A���O����邱�Ƃ��ł��邩�ǂ����������l�B</param>
        public TaskDialogCommandLink(int id, string name, string text, bool dialogClosable = false)
            : base(id, name, text, dialogClosable)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        /// <summary>
        /// <see cref="TaskDialogCommandLink"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="id">�W���{�^��ID�B</param>
        /// <param name="name">�R���g���[�����B</param>
        /// <param name="text">�R���g���[���e�L�X�g�B</param>
        /// <param name="instruction">�����e�L�X�g�B</param>
        public TaskDialogCommandLink(TaskDialogCommonButtonId id, string name, string text, string instruction)
            : base((int)id, name, text)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.Instruction = instruction;
        }

        /// <summary>
        /// <see cref="TaskDialogCommandLink"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="id">�R���g���[��ID�B</param>
        /// <param name="name">�R���g���[�����B</param>
        /// <param name="text">�R���g���[���e�L�X�g�B</param>
        /// <param name="instruction">�����e�L�X�g�B</param>
        /// <param name="dialogClosable">�_�C�A���O����邩�ǂ����𔻒肷��l�B</param>
        public TaskDialogCommandLink(int id, string name, string text, string instruction, bool dialogClosable = false)
            : base(id, name, text, dialogClosable)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.Instruction = instruction;
        }

        /// <summary>
        /// �R�}���h�����N�{�^���̐����e�L�X�g���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// �{�^���e�L�X�g���擾���܂��B
        /// </summary>
        /// <returns>�{�^���e�L�X�g�B</returns>
        public override string GetButtonText()
        {
            return String.Format("{0}{1}{2}",
                this.Text ?? String.Empty,
                (!String.IsNullOrEmpty(this.Text) && !String.IsNullOrEmpty(this.Instruction)) ? Environment.NewLine : String.Empty,
                this.Instruction ?? String.Empty);
        }

        /// <summary>
        /// <see cref="TaskDialogCommandLink"/>�N���X�̕�����\�����擾���܂��B
        /// </summary>
        /// <returns><see cref="TaskDialogCommandLink"/>�N���X�̕�����\���B</returns>
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