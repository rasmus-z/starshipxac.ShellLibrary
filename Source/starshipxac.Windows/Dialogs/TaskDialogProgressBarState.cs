using System;
using starshipxac.Windows.Dialogs.Interop;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// �v���O���X�o�[�̏�Ԃ��`���܂��B
    /// </summary>        
    public enum TaskDialogProgressBarState : uint
    {
        /// <summary>
        /// �ʏ�̏�ԁB
        /// </summary>
        Normal = ProgressBarStates.PBST_NORMAL,

        /// <summary>
        /// �G���[��ԁB
        /// </summary>
        Error = ProgressBarStates.PBST_ERROR,

        /// <summary>
        /// �ꎞ��~��ԁB
        /// </summary>
        Paused = ProgressBarStates.PBST_PAUSED,

        /// <summary>
        /// �}�[�L�[�X�^�C���B
        /// </summary>
        Marquee
    }
}