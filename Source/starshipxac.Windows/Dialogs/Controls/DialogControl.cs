using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    /// �_�C�A���O�R���g���[���̊��N���X���`���܂��B
    /// </summary>
    public abstract class DialogControl : IEquatable<DialogControl>
    {
        public static readonly int MinDialogControlId = 16;

        private static int nextId = MinDialogControlId;

        /// <summary>
        /// �R���g���[�������w�肵�āA
        /// <see cref="DialogControl"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="name">�R���g���[�����B</param>
        protected DialogControl(string name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.Id = GetNextId();
            this.Name = name;
        }

        protected DialogControl(int id, string name)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// �R���g���[�������擾���܂��B
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// �R���g���[��ID���擾���܂��B
        /// </summary>
        public int Id { get; private set; }

        ///// <summary>
        ///// ����<see cref="DialogControl"/>��ێ����Ă���<see cref="IDialogControlHost"/>���擾�܂��͐ݒ肵�܂��B
        ///// </summary>
        //public IDialogControlHost Dialog { get; set; }

        ///// <summary>
        ///// �v���p�e�B�̕ύX���J�n���܂��B
        ///// </summary>
        ///// <param name="propertyName"></param>
        //protected ChangePropertyTransaction BeginChangeProperty([CallerMemberName] string propertyName = "")
        //{
        //    return new ChangePropertyTransaction(this, propertyName);
        //}

        ///// <summary>
        ///// �v���p�e�B�̕ύX��ʒm���܂��B
        ///// </summary>
        ///// <param name="propertyName">�ʒm����v���p�e�B���B</param>
        //protected void ApplyPropertyChange([CallerMemberName] string propertyName = "")
        //{
        //    Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(propertyName));

        //    this.Dialog.ApplyControlPropertyChange(propertyName, this);
        //}

        /// <summary>
        /// ���̃R���g���[��ID���擾���܂��B
        /// </summary>
        /// <returns>�R���g���[��ID�B</returns>
        private static int GetNextId()
        {
            var result = nextId;
            if (nextId == Int32.MaxValue)
            {
                nextId = Int32.MinValue;
            }
            else
            {
                nextId += 1;
            }
            return result;
        }

        public static bool operator ==(DialogControl left, DialogControl right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DialogControl left, DialogControl right)
        {
            return !Equals(left, right);
        }

        public bool Equals(DialogControl other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return this.Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((DialogControl)obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("DialogControl{{id: {0}, name: {1}}}", this.Id, this.Name);
        }

        //protected class ChangePropertyTransaction : IDisposable
        //{
        //    public ChangePropertyTransaction(DialogControl control, [CallerMemberName] string propertyName = "")
        //    {
        //        Contract.Requires<ArgumentNullException>(control != null);

        //        this.Control = control;
        //        this.PropertyName = propertyName;

        //        if (this.Control.Dialog != null)
        //        {
        //            this.Control.Dialog.IsControlPropertyChangeAllowed(this.PropertyName, this.Control);
        //        }
        //    }

        //    ~ChangePropertyTransaction()
        //    {
        //        Dispose();
        //    }

        //    public void Dispose()
        //    {
        //        if (this.Control.Dialog != null)
        //        {
        //            this.Control.Dialog.ApplyControlPropertyChange(this.PropertyName, this.Control);
        //        }
        //    }

        //    public DialogControl Control { get; private set; }
        //    public string PropertyName { get; private set; }
        //}
    }
}