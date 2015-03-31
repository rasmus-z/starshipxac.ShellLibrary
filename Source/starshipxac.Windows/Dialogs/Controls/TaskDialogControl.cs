using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    /// �^�X�N�_�C�A���O�R���g���[�����N���X���`���܂��B
    /// </summary>
    public abstract class TaskDialogControl : DialogControl
    {
        /// <summary>
        /// �R���g���[�������w�肵�āA<see cref="TaskDialogControl"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="name">�R���g���[�����B</param>
        protected TaskDialogControl(string name)
            : base(name)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(name));
        }

        /// <summary>
        /// �R���g���[��ID����уR���g���[�������w�肵�āA<see cref="TaskDialogControl"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="id">�R���g���[��ID�B</param>
        /// <param name="name">�R���g���[�����B</param>
        protected TaskDialogControl(int id, string name)
            : base(id, name)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(name));
        }

        public TaskDialogBase Dialog { get; private set; }

        internal virtual void Attach(TaskDialogBase dialog)
        {
            Contract.Requires<ArgumentNullException>(dialog != null);

            this.Dialog = dialog;
        }

        internal virtual void Detach()
        {
            Contract.Requires<InvalidOperationException>(this.Dialog != null);

            this.Dialog = null;
        }

        protected void ThrowIfNotInitialized()
        {
            if (this.Dialog == null)
            {
                throw new InvalidOperationException();
            }
        }
    }
}