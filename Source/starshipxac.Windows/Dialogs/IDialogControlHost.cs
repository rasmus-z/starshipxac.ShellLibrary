using System;
using System.Diagnostics.Contracts;
using starshipxac.Windows.Dialogs.Controls;

namespace starshipxac.Windows.Dialogs
{
    /// <summary>
    /// �_�C�A���O�R���g���[���z�X�g�̃C���^�[�t�F�C�X���`���܂��B
    /// </summary>
    [ContractClass(typeof(DialogControlHostContract))]
    public interface IDialogControlHost
    {
        /// <summary>
        /// �R���N�V�����̕ύX��������Ă��邩�ǂ����𔻒肵�܂��B
        /// </summary>
        /// <returns>�R���N�V�����̕ύX��������Ă���ꍇ��<c>true</c>�B</returns>
        bool IsCollectionChangeAllowed();

        /// <summary>
        /// �R���N�V�����̕ύX��K�p���܂��B
        /// </summary>
        void ApplyCollectionChanged();

        /// <summary>
        /// �v���p�e�B�l�̕ύX��������Ă��邩�ǂ����𔻒肵�܂��B
        /// </summary>
        /// <param name="propertyName">���肷��v���p�e�B���B</param>
        /// <param name="control">���肷��R���g���[���B</param>
        /// <returns>�v���p�e�B�l�̕ύX��������Ă���ꍇ��<c>true</c>�B</returns>
        bool IsControlPropertyChangeAllowed(string propertyName, DialogControl control);

        /// <summary>
        /// �v���p�e�B�l�̕ύX��K�p���܂��B
        /// </summary>
        /// <param name="propertyName">�v���p�e�B���B</param>
        /// <param name="control">�R���g���[���B</param>
        void ApplyControlPropertyChange(string propertyName, DialogControl control);
    }

    [ContractClassFor(typeof(IDialogControlHost))]
    public abstract class DialogControlHostContract : IDialogControlHost
    {
        public abstract bool IsCollectionChangeAllowed();

        public abstract void ApplyCollectionChanged();

        public abstract bool IsControlPropertyChangeAllowed(string propertyName, DialogControl control);

        public void ApplyControlPropertyChange(string propertyName, DialogControl control)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(propertyName));
            Contract.Requires<ArgumentNullException>(control != null);
        }
    }
}