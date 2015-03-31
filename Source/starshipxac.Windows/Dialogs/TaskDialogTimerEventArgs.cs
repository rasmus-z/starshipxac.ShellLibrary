using System;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// <see cref="TaskDialog.Timer"/>�C�x���g�f�[�^���`���܂��B
    /// </summary>
    public class TaskDialogTimerEventArgs : EventArgs
    {
        /// <summary>
        /// <see cref="TaskDialogTimerEventArgs"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="ticks">�R���g���[�����L���ɂȂ��Ă���̎��Ԑ��B</param>
        public TaskDialogTimerEventArgs(int ticks)
        {
            this.Ticks = ticks;
        }

        /// <summary>
        /// ���Ԑ����擾���܂��B
        /// </summary>
        public int Ticks { get; private set; }
    }
}