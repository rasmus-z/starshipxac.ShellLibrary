using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    /// �^�X�N�_�C�A���O���W�I�{�^�����`���܂��B
    /// </summary>
    public class TaskDialogRadioButton : TaskDialogButtonBase
    {
        /// <summary>
        /// <see cref="TaskDialogRadioButton"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="id">�R���g���[��ID�B</param>
        /// <param name="name">�R���g���[�����B</param>
        /// <param name="text">�e�L�X�g�B</param>
        public TaskDialogRadioButton(int id, string name, string text)
            : base(id, name, text)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        /// <summary>
        /// ���W�I�{�^���̕\���e�L�X�g���擾���܂��B
        /// </summary>
        /// <returns>���W�I�{�^���̕\���e�L�X�g�B</returns>
        public override string GetButtonText()
        {
            return this.Text;
        }
    }
}