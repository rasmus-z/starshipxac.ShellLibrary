using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Components.Internal;

namespace starshipxac.Shell.Components
{
    /// <summary>
    /// �V�X�e���C���[�W�ύX�C�x���g�f�[�^���`���܂��B
    /// </summary>
    public class SystemImageUpdatedEventArgs : ShellNotificationEventArgs
    {
        /// <summary>
        /// <see cref="SystemImageUpdatedEventArgs"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="changeNotify">�V�F���ύX�ʒm���B</param>
        internal SystemImageUpdatedEventArgs(ShellChangeNotify changeNotify)
            : base(changeNotify)
        {
            Contract.Requires<ArgumentNullException>(changeNotify != null);

            ImageIndex = changeNotify.ImageIndex;
        }

        /// <summary>
        /// �X�V���ꂽ�V�X�e���C���[�W�̃C���f�b�N�X���擾���܂��B
        /// </summary>
        public int ImageIndex { get; private set; }
    }
}