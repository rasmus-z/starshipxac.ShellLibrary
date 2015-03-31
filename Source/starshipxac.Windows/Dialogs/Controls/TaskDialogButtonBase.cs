using System;
using System.Diagnostics.Contracts;
using System.Threading;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    /// �^�X�N�_�C�A���O�{�^�����N���X���`���܂��B
    /// </summary>
    public abstract class TaskDialogButtonBase : TaskDialogControl
    {
        private bool enabled = true;
        private string text = String.Empty;
        private bool defaultControl;
        private bool useElevationIcon;

        /// <summary>
        /// <see cref="TaskDialogButtonBase"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="id">�W���{�^��ID�B</param>
        /// <param name="name"></param>
        protected TaskDialogButtonBase(TaskDialogCommonButtonId id, string name)
            : base((int)id, name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.DialogClosable = true;
        }

        /// <summary>
        /// <see cref="TaskDialogButtonBase"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="id">�{�^��ID�B</param>
        /// <param name="name">�{�^�����B</param>
        /// <param name="dialogClosable">�_�C�A���O����邩�ǂ����������l�B</param>
        protected TaskDialogButtonBase(int id, string name, bool dialogClosable = false)
            : base(id, name)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.DialogClosable = dialogClosable;
        }

        /// <summary>
        /// <see cref="TaskDialogButtonBase"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="id">�W���{�^��ID�B</param>
        /// <param name="name">�{�^�����B</param>
        /// <param name="text">�{�^���e�L�X�g�B</param>
        protected TaskDialogButtonBase(TaskDialogCommonButtonId id, string name, string text)
            : base((int)id, name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.Text = text;
            this.DialogClosable = true;
        }

        /// <summary>
        /// <see cref="TaskDialogButtonBase"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="id">�{�^��ID�B</param>
        /// <param name="name">�{�^�����B</param>
        /// <param name="text">�{�^���e�L�X�g�B</param>
        /// <param name="dialogClosable">�_�C�A���O����邩�ǂ����������l�B</param>
        protected TaskDialogButtonBase(int id, string name, string text, bool dialogClosable = false)
            : base(id, name)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(name));

            this.text = text;
            this.DialogClosable = dialogClosable;
        }

        /// <summary>
        /// �{�^���e�L�X�g���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                //using (BeginChangeProperty())
                //{
                //    this.text = value;
                //}
                this.text = value;
            }
        }

        /// <summary>
        /// �{�^�����L�����ǂ����𔻒肷��l���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                //using (BeginChangeProperty())
                //{
                //    this.enabled = value;
                //}
                this.enabled = value;
                this.Dialog.SetButtonEnabled(this, this.enabled);
            }
        }

        /// <summary>
        /// �f�t�H���g�{�^�����ǂ����𔻒肷��l���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public bool Default
        {
            get
            {
                return this.defaultControl;
            }
            set
            {
                //using (BeginChangeProperty())
                //{
                //    this.defaultControl = value;
                //}
                this.defaultControl = value;
            }
        }

        /// <summary>
        /// ���̃{�^�����N���b�N�����ꍇ�ɁA�_�C�A���O����邩�ǂ����𔻒肷��l���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public bool DialogClosable { get; set; }

        /// <summary>
        /// �Ǘ��ҏ��i�A�C�R�����g�p���邩�ǂ����𔻒肷��l���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public bool UseElevationIcon
        {
            get
            {
                return this.useElevationIcon;
            }
            set
            {
                //using (BeginChangeProperty("ShowElevationIcon"))
                //{
                //    this.useElevationIcon = value;
                //}
                this.useElevationIcon = value;
                this.Dialog.SetButtonElevationRequiredState(this, this.useElevationIcon);
            }
        }

        #region Click Event

        /// <summary>
        /// �{�^�����N���b�N����Ɣ������܂��B
        /// </summary>
        public event EventHandler Click;

        protected virtual void OnClick(EventArgs args)
        {
            var handler = Interlocked.CompareExchange(ref this.Click, null, null);
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        internal void RaiseClickEvent()
        {
            if (!this.enabled)
            {
                return;
            }

            OnClick(EventArgs.Empty);
        }

        /// <summary>
        /// �{�^���̃e�L�X�g���擾���܂��B
        /// </summary>
        /// <returns>�{�^���̃e�L�X�g�B</returns>
        public abstract string GetButtonText();
    }
}