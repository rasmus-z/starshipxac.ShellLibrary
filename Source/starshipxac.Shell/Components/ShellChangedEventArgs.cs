using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Components.Internal;

namespace starshipxac.Shell.Components
{
    /// <summary>
    /// �V�F���ύX�C�x���g�f�[�^���`���܂��B
    /// </summary>
    public class ShellChangedEventArgs : ShellNotificationEventArgs
    {
        /// <summary>
        /// <see cref="ShellChangedEventArgs"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="changeNotify">�V�F���ύX�ʒm���B</param>
        internal ShellChangedEventArgs(ShellChangeNotify changeNotify)
            : base(changeNotify)
        {
            Contract.Requires<ArgumentNullException>(changeNotify != null);
            Contract.Requires<ArgumentException>(changeNotify.ShellObject != null);

            this.ShellObject = changeNotify.ShellObject;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ShellObject != null);
        }

        /// <summary>
        /// �C�x���g����������<see cref="ShellObject"/>���擾���܂��B
        /// </summary>
        public ShellObject ShellObject { get; private set; }
    }
}