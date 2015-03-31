using System;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// �^�X�N�_�C�A���O�̎��s���ʂ��`���܂��B
    /// </summary>        
    public enum TaskDialogSelectedButton
    {
        /// <summary>
        /// ��`�O�̃{�^���N���b�N�B
        /// </summary>
        None = 0x0000,

        /// <summary>
        /// OK�{�^���N���b�N�B
        /// </summary>
        Ok = 0x0001,

        /// <summary>
        /// �u�͂��v�{�^���N���b�N�B
        /// </summary>
        Yes = 0x0002,

        /// <summary>
        /// �u�������v�{�^���N���b�N�B
        /// </summary>
        No = 0x0004,

        /// <summary>
        /// �L�����Z���{�^���N���b�N�B
        /// </summary>
        Cancel = 0x0008,

        /// <summary>
        /// �u�Ď��s�v�{�^���N���b�N�B
        /// </summary>
        Retry = 0x0010,

        /// <summary>
        /// ����{�^���N���b�N�B
        /// </summary>
        Close = 0x0020,

        /// <summary>
        /// �J�X�^���{�^���N���b�N�B
        /// </summary>
        CustomButtonClicked = 0x0100,
    }
}