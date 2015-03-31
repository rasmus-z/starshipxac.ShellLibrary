using System;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// <see cref="TaskDialog.HyperlinkClicked"/>�C�x���g�f�[�^���`���܂��B
    /// </summary>
    public class TaskDialogHyperlinkClickedEventArgs : EventArgs
    {
        /// <summary>
        /// �n�C�p�[�����N�̃e�L�X�g���w�肵�āA
        /// <see cref="TaskDialogHyperlinkClickedEventArgs"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="linkText"></param>
        public TaskDialogHyperlinkClickedEventArgs(string linkText)
        {
            this.LinkText = linkText;
        }

        /// <summary>
        /// �N���b�N�����n�C�p�[�����N�̃e�L�X�g���擾���܂��B
        /// </summary>
        public string LinkText { get; private set; }
    }
}