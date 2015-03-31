using System;
using System.Diagnostics.Contracts;
using starshipxac.Windows.Properties;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    /// �^�X�N�_�C�A���O�̃v���O���X�o�[���`���܂��B
    /// </summary>
    public class TaskDialogProgressBar : TaskDialogControl
    {
        private TaskDialogProgressBarState state;
        private int minimum = 0;
        private int maximum = 100;
        private int value;

        /// <summary>
        /// �R���g���[�������w�肵�āA
        /// <see cref="TaskDialogProgressBar"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="name">�R���g���[�����B</param>
        public TaskDialogProgressBar(string name)
            : base(name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        /// <summary>
        /// �v���O���X�o�[�̏�Ԃ��擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public TaskDialogProgressBarState State
        {
            get
            {
                return this.state;
            }
            set
            {
                //using (BeginChangeProperty())
                //{
                //    this.state = value;
                //}
                this.state = value;
                this.Dialog.SetProgressBarState(this, this.state);
            }
        }

        /// <summary>
        /// �v���O���X�o�[�̍ŏ��l���擾�܂��͐ݒ肵�܂��B
        /// </summary>                
        public int Minimum
        {
            get
            {
                return this.minimum;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(0 <= value,
                    DialogErrorMessages.TaskDialogProgressBarMinValueGreaterThanZero);
                Contract.Requires<ArgumentOutOfRangeException>(value < this.Maximum,
                    DialogErrorMessages.TaskDialogProgressBarMinValueLessThanMax);

                //using (BeginChangeProperty())
                //{
                //    this.minimum = value;
                //}
                this.minimum = value;
                this.Dialog.SetProgressBarRange(this, this.minimum, this.maximum);
            }
        }

        /// <summary>
        /// �v���O���X�o�[�̍ő�l���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public int Maximum
        {
            get
            {
                return this.maximum;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(this.Minimum <= value,
                    DialogErrorMessages.TaskDialogProgressBarMaxValueGreaterThanMin);

                //using (BeginChangeProperty())
                //{
                //    this.maximum = value;
                //}
                this.maximum = value;
                this.Dialog.SetProgressBarRange(this, this.minimum, this.maximum);
            }
        }

        /// <summary>
        /// �v���O���X�o�[�̌��݂̒l���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(this.Minimum <= value && value <= this.Maximum,
                    DialogErrorMessages.TaskDialogProgressBarValueInRange);

                //using (BeginChangeProperty())
                //{
                //    this.value = value;
                //}
                this.value = value;
                this.Dialog.SetProgressBarPosition(this, this.value);
            }
        }

        /// <summary>
        /// �v���O���X�o�[�̒l���ŏ��l�ƍő�l�̊Ԃɂ��邩�ǂ��������؂��܂��B
        /// </summary>
        internal bool HasValidValues
        {
            get
            {
                return this.minimum <= this.value && this.value <= this.maximum;
            }
        }

        /// <summary>
        /// �v���O���X�o�[�̒l���ŏ��l�ɐݒ肵�܂��B
        /// </summary>
        protected internal void Reset()
        {
            this.state = TaskDialogProgressBarState.Normal;
            this.value = this.minimum;
        }
    }
}